using UnityEngine;

namespace GameScripts.RunTime.Model
{
    /// <summary>
    /// 翅膀模型
    /// </summary>
    public class WingModel : ModelBase
    {
        protected override string GetLocation()
        {
            if (Info.show_wing <= 0) return string.Empty;
            return "";
        }
        
        public override Transform GetParent()
        {
            var mainModel = ActorEntity.GetModel<MainModel>();
            return mainModel.IsLoadDone ? mainModel.WingContainer : base.GetParent();
        }
    }
}