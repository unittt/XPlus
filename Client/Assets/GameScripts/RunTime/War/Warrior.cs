using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameScript.RunTime.Config;
using GameScripts.RunTime.Model;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 战斗中对象
    /// </summary>
    [EntityResource("Warrior",true)]
    public class Warrior : ActorEntity
    {

        private const float DIS_THRESHOLD = 0.01f;
        public ActorEntity _actor;
        private Busy _busy;
        private Busy currentStatus;

        public int m_ID;


        private int m_arge_5703 = 0;
        private int m_arge_5704 = 0;


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

        public Vector3 OriginPos { get; private set; }

        
        
        
        
        


        #region 方向
        public Vector3 LocalUp => WarManager.Current.Root.InverseTransformDirection(RotateObj.up);
        public Vector3 LocalForward => WarManager.Current.Root.InverseTransformDirection(RotateObj.forward);
        public Vector3 LocalRight => WarManager.Current.Root.InverseTransformDirection(RotateObj.right);
        #endregion

        #region 行为
        public void GoBack()
        {
            
        }


        public void RunTo(Vector3 endPos,Vector3 endAngle,bool isRunBack,Action callBack)
        {
            
        }
        
        
        public async UniTaskVoid RunTo(Vector3 endPos,Vector3 endAngle,float speed,bool isRunBack,Action callBack)
        {
            //如果对象死亡 不处理
            //添加标签
            AddBusy(Busy.RunTo);
            var curPos = LocalPosition;
            //获得距离
            var dis = WarTools.GetHorizontalDis(curPos, endPos);

            if (dis > DIS_THRESHOLD)
            {
                var t = dis / speed;
                LookAtPos(endPos);

                //播放移动动画
                _actor.AdjustSpeedPlay(AnimationClipCode.RUN, 0.4f);
                //等待移动结束
                await Entity.transform.DOLocalMove(endPos, t).AsyncWaitForCompletion();
                
                //设置角度
                // if endAngle then
                // self.m_RotateObj:SetLocalEulerAngles(endAngle)
                // else
                // self:FaceDefault()
                
                //播放动画
                _actor.CrossFade(AnimationClipCode.IDLE_WAR ,0.1f,0,0);
                
                //如果需要回到原地
                if (isRunBack)
                {
                    SetLocalPos(/* original position */);
                }
                
                callBack?.Invoke();
                //等待一会
                await UniTask.Delay(200);
              
            }
            else
            {
                //1.设置旋转角度
                
                //2.直接回调
                callBack?.Invoke();
            }
            
            //移除标签
            RemoveBusy(Busy.RunTo);
        }
        

        private void SetLocalPos()
        {
            
        }

        public void LookAtPos(Vector3 localPos)
        {
            
        }
        
        public void LookAtPos(Vector3 localPos, float time)
        {
            var position = LocalPosition;
            var dir = WarTools.GetRootDir(null, position, localPos);
            if (dir is { x: 0, z: 0 })
            {
                return;
            }

            var dirForward = Entity.transform.InverseTransformDirection(dir);
            var dirUp = Entity.transform.InverseTransformDirection(Entity.transform.up);
            var r = Quaternion.LookRotation(dirForward, dirUp);

            //旋转朝向
            ActorContainer.DOKill();
            ActorContainer.DOLocalRotateQuaternion(r, time);
        }

        public void FaceDefault()
        {
            var angle = GetDefaultRotateAngle();
            if (LocalEulerAngles != angle)
            {
                Entity.transform.DOLocalRotate(angle, 0.1f);
            }
        }

        /// <summary>
        /// 获得默认的角度
        /// </summary>
        /// <returns></returns>
        public Vector3 GetDefaultRotateAngle()
        {
            return Vector3.zero;
        }
        #endregion


   
        
        public Vector3 LocalPosition =>  Entity.transform.localPosition;

        public Vector3 LocalEulerAngles
        {
            get => Entity.transform.localEulerAngles;
            set => Entity.transform.localEulerAngles = value;
        }


        public Transform ActorContainer { get; private set; }


        protected override async UniTask OnAssembleModelFinish()
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
        public Transform RotateObj { get; set; }
      

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

        public void SetOriginPos(Vector3 pos)
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
            var isNearOriPos = warrior.IsNearOriPos(warrior.Pos);
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
    }
}