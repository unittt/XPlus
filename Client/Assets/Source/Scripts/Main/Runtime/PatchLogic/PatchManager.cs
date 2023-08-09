using System;
using UnityEngine;
using YooAsset;
using ZEngine.Utility.State;


public static class PatchManager
    {
        private static bool _isRun;
        private static StateMachine _machine;
        
        /// <summary>
        /// 包裹的版本信息
        /// </summary>
        public static string PackageVersion { set; get; }
        /// <summary>
        /// 下载器
        /// </summary>
        public static PatchDownloaderOperation Downloader { set; get; }
        /// <summary>
        /// 监听器
        /// </summary>
        internal static IPatchListener Listener { get; private set; }

        /// <summary>
        /// 当准备完成
        /// </summary>
        public static event Action OnDone;

        public static void Run()
        {
            if (_isRun)
            {
                Debug.LogWarning("补丁更新已经正在进行中!");
                return;
            }

            _isRun = true;
            Debug.Log("开启补丁更新流程...");
            _machine = new StateMachine(null);
            _machine.AddState<FsmPatchPrepare>();
            _machine.AddState<FsmInitialize>();
            _machine.AddState<FsmUpdateVersion>();
            _machine.AddState<FsmUpdateManifest>();
            _machine.AddState<FsmCreateDownloader>();
            _machine.AddState<FsmDownloadFiles>();
            _machine.AddState<FsmDownloadOver>();
            _machine.AddState<FsmClearCache>();
            _machine.AddState<FsmPatchDone>();
            _machine.Run<FsmPatchPrepare>();
        }

        public static void SetListener(IPatchListener listener)
        {
            Listener = listener;
        }

        /// <summary>
        /// 触发结束
        /// </summary>
        internal static void ThrowDone()
        {
            OnDone?.Invoke();
        }
        
        /// <summary>
        /// 用户尝试再次初始化资源包
        /// </summary>
        public static void UserTryInitialize()
        {
            _machine.SwitchState<FsmInitialize>();
        }
        /// <summary>
        /// 用户开始下载网络文件
        /// </summary>
        public static void UserBeginDownloadWebFiles()
        {
            _machine.SwitchState<FsmDownloadFiles>();
        }
        /// <summary>
        /// 用户尝试再次更新静态版本
        /// </summary>
        public static void UserTryUpdatePackageVersion()
        {
            _machine.SwitchState<FsmUpdateVersion>();
        }
        /// <summary>
        /// 用户尝试再次更新补丁清单
        /// </summary>
        public static void UserTryUpdatePatchManifest()
        {
            _machine.SwitchState<FsmUpdateManifest>();
        }
        /// <summary>
        /// 用户尝试再次下载网络文件
        /// </summary>
        public static void UserTryDownloadWebFiles()
        {
            _machine.SwitchState<FsmCreateDownloader>();
        }
    }
