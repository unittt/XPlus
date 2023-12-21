using UnityEngine;

namespace GameScripts.RunTime.Buff
{
    public class BuffInfo
    {
        public BuffData buffData;
        public GameObject creator;
        public GameObject target;
        public float durationTimer;
        public float tickTimer;
        /// <summary>
        /// 默认初始层数为1
        /// </summary>
        public int curStack = 1;
    }
}