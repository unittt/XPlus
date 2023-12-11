using GameScripts.RunTime.Utility.Variable;
using HT.Framework;
using UnityEngine;


namespace GameScripts.RunTime.EditorMagic
{
    public class ComplexArgBoxEntity: ArgBoxEntityBase
    {
        public Transform Container { get; private set; }
        
        public override void Fill(GameObject entity, VarFieldInfo varFieldInfo)
        {
            base.Fill(entity, varFieldInfo);
            Container = _entity.FindChildren("Container").transform;
        }
    }
}