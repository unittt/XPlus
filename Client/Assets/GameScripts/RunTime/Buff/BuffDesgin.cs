using System;

namespace GameScripts.RunTime.Buff
{
    public enum BuffTimeUpdateEnum
    {
        Add,
        Replace,
        Keep
    }

    public enum BuffRemoveStackUpdateEnum
    {
        Clear,
        Reduce
    }

    [Serializable]
    public class Property
    {
        public float hp;
        public float atk;
    }
}