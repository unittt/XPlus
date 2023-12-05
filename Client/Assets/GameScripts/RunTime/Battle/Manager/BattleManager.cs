using HT.Framework;

namespace GameScripts.RunTime.Battle.Manager
{
    public class BattleManager : SingletonBase<BattleManager>
    {

        public bool IsBattle { get; private set; }

        
        public void EnterBattleState()
        {
            IsBattle = true;
            
            //1.初始化场景
            //2.
        }

        public void ExitBattleState()
        {
            IsBattle = false;
        }
    }
}