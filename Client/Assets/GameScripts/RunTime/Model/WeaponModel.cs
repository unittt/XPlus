using UnityEngine;

namespace  GameScripts.RunTime.Model
{
    /// <summary>
    /// 武器模型
    /// </summary>
    public class WeaponModel : ModelBase
    {
        public override Transform GetParent()
        {
            var mainModel = ActorEntity.GetModel<MainModel>();
            return mainModel.IsLoadDone ? mainModel.WeaponContainer : base.GetParent();
        }

        protected override string GetLocation()
        {
            return $"weapon{Info.shape.ToString()}_{Info.weapon.ToString()}";
        }
    }
}