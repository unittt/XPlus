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
        
        protected override void OnFill(MoveDir commandData)
        {
            GetExecutors(commandData.executor,_warriors);

            foreach (var warrior in _warriors)
            {
                var dir = MagicTools.GetDir(warrior, commandData.dir);
                var beginPos = warrior.Pos;
                var endPos = beginPos + commandData.move_time * commandData.speed * dir;
                warrior.Entity.transform.DOLocalMove(endPos, commandData.move_time);
            }
        }
    }
}