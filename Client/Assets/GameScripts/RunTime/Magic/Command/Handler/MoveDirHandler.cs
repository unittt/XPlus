using System.Collections.Generic;
using DG.Tweening;
using GameScripts.RunTime.War;

namespace GameScripts.RunTime.Magic.Command.Handler
{
    /// <summary>
    /// 朝方向移动
    /// </summary>
    public class MoveDirHandler : CmdHandlerBase<MoveDir>
    {
        private List<Warrior> _warriors = new();

        protected override void OnExecute()
        {
            GetExecutors(Data.executor,_warriors);

            foreach (var warrior in _warriors)
            {
                var dir = MagicTools.GetDir(warrior, Data.dir);
                var beginPos = warrior.Position;
                var endPos = beginPos + Data.move_time * Data.speed * dir;
                warrior.Entity.transform.DOLocalMove(endPos, Data.move_time);
            }
        }
        
    }
}