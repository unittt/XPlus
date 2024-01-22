namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 伤害指令
    /// </summary>
    public sealed class WarDamageCmd:WarCmd
    {
        public int wid;
        public int type;
        /// <summary>
        /// 伤害
        /// </summary>
        public int damage;
        /// <summary>
        /// 是否暴击
        /// </summary>
        public int iscrit;

        protected override void OnExecute()
        {
            //1. miss
            //2. 防守
            if (type == 1 || type == 2)
            {
                 SetCompleted();
                return;
            }
            
            if (WarManager.Current.TryGetWarrior(wid, out  var  warrior))
            {
                warrior.ShowDamage(damage, iscrit,false);
                SetCompleted();
            }
            else
            {
                SetCompleted();
            }
        }
    }
}