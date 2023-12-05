using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Model
{
    /// <summary>
    /// 坐骑模型
    /// </summary>
    public class RideModel : ModelBase
    {
        /// <summary>
        /// 角色乘骑的节点
        /// </summary>
        public Transform MountRideContainer { get; private set; }

        protected override void OnEntityCreationCompleted()
        {
            var variableBehaviour = Entity.GetComponent<VariableBehaviour>();
            MountRideContainer =  variableBehaviour.Container.Get<Transform>("mount_Ride");
        }

        protected override string GetLocation()
        {
            if (Info.horse <= 0) return string.Empty;
            return "model" + Info.horse;
        }
    }
}