using GameScript.RunTime.UI;
using HT.Framework;

namespace GameScript.RunTime.Procedure
{
    
    /// <summary>
    /// 登录流程
    /// </summary>
    public class ProcedureLogin : ProcedureBase
    {
        /// <summary>
        /// 进入流程
        /// </summary>
        /// <param name="lastProcedure">上一个离开的流程</param>
        public override void OnEnter(ProcedureBase lastProcedure)
        {
            base.OnEnter(lastProcedure);
            //服务器连接成功 后打开登陆界面
            Main.m_UI.OpenUI<UILogin>();
        }
    }
}