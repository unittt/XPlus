using System.Collections.Generic;

namespace GameScripts.RunTime.War
{
    /// <summary>
    /// 施法指令
    /// </summary>
    public class MagicCmd : WarCmd
    {
        /// <summary>
        /// 攻击
        /// </summary>
        public int atkid;
        public List<int> vicid_list;
        public int magic_id;
        public int magic_index;


        
        protected override void OnExecute()
        {
            
            //4.处理弹幕命令：如果有与弹幕相关的特定命令（infoBulletBarrage_cmd），则执行该命令。
            //5.处理受害者 ID 和附加动作：如果没有指定受害者 ID（vicid_list），则对攻击者执行额外动作，如插入删除或生存命令。
            //6.处理说话命令：遍历所有战士，并执行与他们相关的任何说话命令（speek_cmd）。
            //7.准备魔法单元和数据：准备一个新的魔法单元（oMagicUnit）并附上相关数据，如攻击者和受害者的引用。这部分涉及设置魔法单元以执行法术或能力
            //8.确定时间和执行流程：检查各种条件，如当前受害者是否是下一个魔法命令的受害者的一部分，设置子动作的时间，并决定攻击是否应该停止击打。
            //9.管理魔法命令的执行：插入开始魔法单元的动作，等待魔法单元结束，以及检查清除任何临时变更的动作
            //10.处理返回命令：如果攻击者有返回命令，根据某些条件执行或忽略它。
            //11.处理保护机制：对每个受害者检查是否有保护机制（如一个战士跑去保护另一个），并执行相关动作。
            
            //1.获取攻击者对象
            var atkObj = WarManager.Current.GetWarrior(atkid);
            if (atkObj is null)
            {
                return;
            }

            //2.处理被动技能 检查攻击者是否有由魔法命令触发的被动技能（trigger_passive）。如果有，就执行这些被动命令。
            var dAtkVary = GetWarriorVary(atkid);
            var passiveMaxIndex = dAtkVary.trigger_passive.Count - 1;
            for (var i = passiveMaxIndex; i >= 0; i--)
            {
                var oCmd = dAtkVary.trigger_passive[i];
                //获得被动技能数据
                oCmd.TryExecute();
                //移除技能
                dAtkVary.trigger_passive.Remove(oCmd);
            }
            
            //3.检查并通知命令：调用 oCmd:CheckNotifyCmds 可能基于攻击者的状态或条件处理命令的其他方面。
            // oCmd:CheckNotifyCmds(dAtkVary, 0)
            
            
        }
    }
}