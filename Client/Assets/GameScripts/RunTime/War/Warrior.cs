using System;
using System.Collections.Generic;
using cfg.WarModule;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameScript.RunTime.Config;
using GameScripts.RunTime.Model;
using HT.Framework;
using Pb.Mmo.Common;
using UnityEngine;

namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 战斗中对象
    /// </summary>
    [EntityResource("Warrior",true)]
    public sealed class Warrior : ActorEntity
    {

        private const float DIS_THRESHOLD = 0.01f;
        private Busy _busy;
        private Busy currentStatus;

        public int m_ID;


        private int m_arge_5703 = 0;
        private int m_arge_5704 = 0;


        /// <summary>
        /// 阵营
        /// </summary>
        public ECamp Camp { get; private set; }

        /// <summary>
        /// 阵营编号
        /// </summary>
        public int CampID { get; private set; }

        /// <summary>
        /// 阵营中的坐标
        /// </summary>
        public int CampPos { get; private set; }

        /// <summary>
        /// 是否为盟友
        /// </summary>
        public bool IsAlly
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 在战场中的初始坐标
        /// </summary>
        public Vector3 OriginPos { get; private set; }

        public Transform RotateTransform { get; private set; }
        #region 方向
        public Vector3 LocalUp => WarManager.Current.Root.InverseTransformDirection(RotateTransform.up);
        public Vector3 LocalForward => WarManager.Current.Root.InverseTransformDirection(RotateTransform.forward);
        public Vector3 LocalRight => WarManager.Current.Root.InverseTransformDirection(RotateTransform.right);
        #endregion

        
        public override void OnInit()
        {
            base.OnInit();
            RotateTransform = VbArray.Get<Transform>("rotateNode");
        }

        public void Fill(ModelInfo modelInfo, ECamp camp, int campPos)
        {
            Fill(modelInfo);
            Camp = camp;
            OriginPos = WarTools.GetPositionByCampAndIndex(camp, campPos);
            Position = OriginPos;
            LocalEulerAngles = WarTools.GetDefalutRotateAngle(camp);
        }

        #region 移动
        public void GoBack(float speed)
        {
            var angle = GetDefaultRotateAngle();
            RunTo(OriginPos, angle,speed, false);
        }

        
        
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="endPos">目标点</param>
        /// <param name="endAngle">结束点</param>
        /// <param name="speed">速度</param>
        /// <param name="callBack"></param>
        public void RunTo(Vector3 endPos,Vector3 endAngle,float speed,bool isRunBack, HTFAction callBack = null)
        {
            if(!Entity)return;
            
            //获得距离
            var dis = WarTools.GetHorizontalDis(LocalPosition, endPos);

            if (dis > DIS_THRESHOLD)
            {
                //时间
                var t = dis / speed;
                //朝向坐标
                LookAtPos(endPos);
                //播放移动动画
                AdjustSpeedPlay(AnimationClipCode.RUN, 0.4f,callBack);
                transform.DOLocalMove(endPos, t).OnComplete(() =>
                {
                    RemoveBusy(Busy.RunTo);
                    callBack?.Invoke();

                    if (endAngle != Vector3.zero)
                    {
                        RotateTransform.localEulerAngles = endAngle;
                    }
                    else
                    {
                        FaceDefault();
                    }
                    
                    //如果需要回到原地
                    if (isRunBack)
                    {
                        LocalPosition = OriginPos;
                    }
                });
                //播放动画
                CrossFade(AnimationClipCode.IDLE_WAR ,0.1f,0,0, null);
            }
            else
            {
                //移除标签
                RemoveBusy(Busy.RunTo);
                //1.设置旋转角度
                RotateTransform.localEulerAngles = endAngle;
                //2.直接回调
                callBack?.Invoke();
            }
            
           
        }
        
        /// <summary>
        /// 朝向目标
        /// </summary>
        /// <param name="localPos"></param>
        /// <param name="time"></param>
        public void LookAtPos(Vector3 localPos, float time = 0)
        {
            var position = LocalPosition;
            var dir = WarTools.GetRootDir(position, localPos);
            if (dir is { x: 0, z: 0 })
            {
                return;
            }

            var dirForward = transform.InverseTransformDirection(dir);
            var dirUp = transform.InverseTransformDirection(transform.up);
            var r = Quaternion.LookRotation(dirForward, dirUp);

            if (time > 0)
            {
                //旋转朝向
                RotateTransform.DOKill();
                RotateTransform.DOLocalRotateQuaternion(r, time);
            }
            else
            {
                RotateTransform.rotation = r;
            }
        }

        public void FaceDefault()
        {
            var defaultRotateAngle = GetDefaultRotateAngle();
            var  angles= RotateTransform.localEulerAngles;
            if (angles != defaultRotateAngle)
            {
                RotateTransform.DOLocalRotate(defaultRotateAngle, 0.1f);
            }
        }

        /// <summary>
        /// 获得默认的角度
        /// </summary>
        /// <returns></returns>
        public Vector3 GetDefaultRotateAngle()
        {
             return Camp == ECamp.A ? new Vector3(0, -50, 0) : new Vector3(0, 130, 0);
        }
        #endregion
        

        protected override async UniTask OnAllModelLoadDone()
        {
            var animatorControllerLocation = $"Animator{Shape}_2";
            var animatorOverrideController = await Main.m_Resource.LoadAsset<AnimatorOverrideController>(animatorControllerLocation);
            GetModel<MainModel>().SetAnimatorController(animatorOverrideController);
        }

        #region Busy
        
        private void AddBusy(Busy other)
        {
            _busy |= other;
        }

        private void RemoveBusy(Busy other)
        {
            _busy &= ~other;
        }

        private bool CheckingBusy(Busy other)
        {
            return (_busy & other) == other;
        }

        private bool IsOnlyNone =>_busy == Busy.None;
      
        
        private void ResetBusy()
        {
            _busy = Busy.None;
        }
        
        #endregion

        public override int Layer => LayerConfig.War;

        public int MagicID;

        public bool IsBusy(string passivereborn)
        {
            return false;
        }

        /// <summary>
        /// 设置当前施法的编号
        /// </summary>
        /// <param name="magicID"></param>
        public void SetPlayMagicID(int magicID)
        {
            
        }
        
        public Vector3 GetOriginPos()
        {
            return Vector3.zero;
        }

        public void TriggerPassiveSkill(int pfid, Dictionary<string, int> keyList)
        {

            var needTip = true;

            if (pfid == 5703)
            {
                //狂暴
                needTip = m_arge_5703 == 0;
                m_arge_5703 = needTip ? 1 : 0;
                if (needTip)
                {
                    AddBindObj("warrior_statu_5703");
                }
                else
                {
                    DelBindObj("warrior_statu_5703");
                }
            }
            else if (pfid == 5704)
            {
                //好战
            }

            if (needTip)
            {
                //获得被动技能数据
                // var passiveData = DataTools.GetMaigcPassiveData(pfid);
                
            }

            if (keyList != null)
            {
                foreach (var pair in keyList)
                {
                    if (pair.Key == "ghost")
                    {
                        RefeshStatusGohst(pfid, pair.Key, pair.Value);
                    }
                }
            }
        }

        private void DelBindObj(string warriorStatu)
        {
            
        }

        private void AddBindObj(string warriorStatu)
        {
           
        }

        /// <summary>
        /// 魄实现
        /// </summary>
        /// <param name="pfid"></param>
        /// <param name="pairKey"></param>
        /// <param name="pairValue"></param>
        private void RefeshStatusGohst(int pfid, string pairKey, int pairValue)
        {
           
        }

        public void UpdateAutoMagic()
        {
           
        }
        
        /// <summary>
        /// 用于存储与战斗相关的一些浮动（动态变化的）信息。具体来说，m_FloatHitInfo 似乎用于追踪和管理角色在战斗中的某些特定状态或事件
        /// down_timer、restore_timer：这些可能是与计时相关的字段，用于追踪某些状态或效果的持续时间。
        /// first_atkid、last_atk_id：这些字段可能用于存储首次攻击者和最后一次攻击者的标识符（ID），这在处理连击、反击或特殊事件时可能非常有用。
        /// atkids：这个字段可能是一个包含攻击者ID的列表或集合，用于追踪战斗中涉及的所有攻击者
        /// record：这个布尔类型的字段可能用于指示是否应该记录或激活某些功能或状态
        /// </summary>
        private object m_FloatHitInfo; 

        /// <summary>
        /// 回合开始
        /// </summary>
        public void Bout()
        {
          
            //1.刷新回合的所有参数
            //2.处理buff 的回合开始
            //3.处理施法cd
            //4.清理战斗相关的浮动信息
            // m_FloatHitInfo.first_atkid = 0;
            // m_FloatHitInfo.atkids = null;
            // m_FloatHitInfo.record = false;
            // m_FloatHitInfo.laset_atk_id = 0;
            //5.检查错误
        }

        public void SetLocalEulerAngles(Vector3 eulerAngles)
        {
            
        }

        private bool IsSameCamp(Warrior warrior)
        {
            return CampID == warrior.CampID;
        }
        

        /// <summary>
        /// 是否相邻
        /// </summary>
        /// <param name="warrior"></param>
        private bool IsNeighbor(Warrior warrior)
        {
            if (IsSameCamp(warrior))
            {
                return false;
            }

            var iCampPos = warrior.CampPos;
            //判断是否为邻居
            //暂时不做处理
            
            return false;
        }
        
        public Vector3 GetNormalAttackPos(Warrior warrior)
        {
            var isNearOriPos = warrior.IsNearOriPos(warrior.Position);
            var isBossCamp = WarManager.Current.IsBossWarType && !warrior.IsAlly;
            var isNeighbor = IsNeighbor(warrior);
            //不属于boss阵营 并且不属于邻居 并且目标在目标原点附近
            if (!isBossCamp && ! isNeighbor && isNearOriPos)
            {
                return warrior.GetOriginPos();
            }

            return Vector3.zero;
        }

   

        /// <summary>
        /// 在原始坐标附近
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool IsNearOriPos(Vector3 pos)
        {
            return WarManager.Current.GetHorizontalDis(pos, OriginPos) < 0.05f;
        }

        #region Hud
        /// <summary>
        /// 显示伤害
        /// </summary>
        /// <param name="damage">伤害</param>
        /// <param name="isCrit">是否暴击</param>
        public void ShowDamage(int damage, int isCrit, bool hitTrick)
        {
            //加血
            if (damage > 0)
            {
                
            }
            else if (hitTrick)
            {
                //播放被打击音效
            }
            
         
        }
        #endregion
    }
}