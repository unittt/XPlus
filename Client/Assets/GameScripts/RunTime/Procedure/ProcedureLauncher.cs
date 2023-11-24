using GameScript.RunTime.Network;
using UnityEngine;
using HT.Framework;

namespace GameScript.RunTime.Procedure
{
    /// <summary>
    /// 启动流程
    /// </summary>
    public class ProcedureLauncher : ProcedureBase
    {
        /// <summary>
        /// 流程初始化
        /// </summary>
        public override void OnInit()
        {
            //设置目标帧率
            Application.targetFrameRate = 60;
            //关闭多点触碰
            Input.multiTouchEnabled = false;
            //开启后台运行
            Application.runInBackground = true;
            //设置语言环境
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture =
                new System.Globalization.CultureInfo("en-US");
            //连接服务器
            GameNetManager.ConnectServer();
        }

        /// <summary>
        /// 流程帧刷新
        /// </summary>
        public override void OnUpdate()
        {
            if (Main.m_Resource.IsInitialized)
            {
                // Main.m_Procedure.SwitchProcedure<ProcedureLogin>();
                Main.m_Procedure.SwitchProcedure<ProcedureGame>();
            }
        }
    }
}