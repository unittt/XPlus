using UnityEngine;

namespace GameScripts.RunTime.Buff
{
    public class SpawnGOBM: BaseBuffModule
    {
        public GameObject prefab;
        public Vector3 localPosition;

        public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
        {
            var obj = GameObject.Instantiate(prefab);
            obj.transform.position = localPosition;
        }
    }
}