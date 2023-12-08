using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Command("指令组",1001)]
    public class GroupCmd:CommandBase
    {
        [Argument("指令组类型")]
        [SelectHandler(typeof(EnumSelectorHandler<CmdGroupType>))]
        public CmdGroupType cmdGroupType;

        #region 条件判断
        [Argument("条件")]
        [SelectHandler(typeof(EnumSelectorHandler<CmdGroupCondition>))]
        public CmdGroupCondition Condition_name;
        
        [Argument("true指令组","IsShowCondition")]
        public string true_group;
        
        [Argument("false指令组","IsShowCondition")]
        public string false_group;
        #endregion

        #region 重复播放
        [Argument("选取列表","IsShowRepeat")]
        public string group_names;
        
        [Argument("选取方式","IsShowRepeat")]
        [SelectHandler(typeof(EnumSelectorHandler<GetType>))]
        public GetType get_type;

        [Argument("选取次数","IsShowRepeat")]
        public int cnt;
        #endregion
        
        [Argument("添加方式")]
        [SelectHandler(typeof(EnumSelectorHandler<CmdGroupAddType>))]
        public CmdGroupAddType add_type;

        private bool IsShowCondition()
        {
            return cmdGroupType == CmdGroupType.Condition;
        }

        private bool IsShowRepeat()
        {
            return cmdGroupType == CmdGroupType.Repeat;
        }
    }
}