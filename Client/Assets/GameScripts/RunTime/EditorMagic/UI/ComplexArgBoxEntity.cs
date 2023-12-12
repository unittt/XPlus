using GameScripts.RunTime.Utility.Variable;
using HT.Framework;
using UnityEngine;


namespace GameScripts.RunTime.EditorMagic
{
    public class ComplexArgBoxEntity: ArgBoxEntityBase
    {
        public Transform Container { get; private set; }
        
        public override void Fill(GameObject entity, VarFieldInfo varFieldInfo, ArgBoxEntityBase parent)
        {
            base.Fill(entity, varFieldInfo,parent);
            Container = _entity.FindChildren("Container").transform;
        }
    }
}