using UnityEngine;

namespace GameScripts.RunTime.Buff
{
    public class Character : MonoBehaviour
    {
        public Property property;

        public bool IsCanBekill(DamageInfo damageInfo)
        {
            return false;
        }
    }
}