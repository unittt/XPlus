using GameScript.RunTime.UI;
using GameScripts.RunTime.Hud;
using GameScripts.RunTime.Utility.Timer;
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


            if ( WarManager.Current.TryGetWarrior(wid, out var warrior))
            {
                if ( WarManager.Current.IsBossWarType && warrior.CampPos == 1 && warrior.CampID != WarManager.Current.m_AllyCamp)
                {
                    var uiWarFloat = Main.m_UI.GetOpenedUI<UIWarFloat>();
                    uiWarFloat.AddMsg(3121, content,2);
                }
                else
                {
                    HudManager.Current.ShowHud<ChatHudEntityLogic>(warrior, logic => logic.Show(content));
                }
                TimerManager.RegisterTimer(2, SetCompleted);
            }
            else
            {
                SetCompleted();
            }
        }
    }
}