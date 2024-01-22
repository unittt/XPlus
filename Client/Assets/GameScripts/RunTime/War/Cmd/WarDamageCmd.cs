namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 伤害指令
    /// </summary>
    public sealed class WarDamageCmd:WarCmd
    {
        public int wid;
        public int type;
        public int damage;
        public int iscrit;

        protected override void OnExecute()
        {
            SetCompleted();
        }
    }
}