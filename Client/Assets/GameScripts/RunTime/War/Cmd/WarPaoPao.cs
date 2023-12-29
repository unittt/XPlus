using HT.Framework;

namespace GameScripts.RunTime.War
{
    public class WarPaoPao : WarCmd
    {
        public int wid;
        public string content;

        protected override void OnExecute()
        {
            //显示一个对话框
            //等待几秒关闭 不阻塞进程
            
            //如果时Boss站 并且阵营为敌方 并且campPos = 1;
            //显示Boss的 oView:AddMsg(oWarrior.m_Actor.m_Shape, v.content, 2)
            //等待几秒关闭 不阻塞进程
            
        }
    }
}