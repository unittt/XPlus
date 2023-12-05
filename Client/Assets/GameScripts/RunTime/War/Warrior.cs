using System;
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
                _actor.CrossFade(AnimationClipCode.IDLE_WAR ,0.1f);
                
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

        private void LookAtPos(Vector3 localPos)
        {
            
        }
        
        private void LookAtPos(Vector3 localPos, float time)
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
            var angle = GetDefalutRotateAngle();
            if (LocalEulerAngles != angle)
            {
                Entity.transform.DOLocalRotate(angle, 0.1f);
            }
        }

        private Vector3 GetDefalutRotateAngle()
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

        public bool IsBusy(string passivereborn)
        {
            return false;
        }
    }
    
    
}