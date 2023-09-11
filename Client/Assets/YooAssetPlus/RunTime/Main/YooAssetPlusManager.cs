using HT.Framework;
using UnityEngine;

namespace YooAssetPlus
{
    /// <summary>
    /// YooAsset扩展
    /// </summary>
    [LockTransform]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-10)]
    public sealed partial class YooAssetPlusManager : SingletonBehaviourBase<YooAssetPlusManager>
    {
        protected override void Awake()
        {
            base.Awake();
            OnAwake();
        }

        private void Start()
        {
            OnStart();
        }

        private void Update()
        {
            OnUpdate();
        }

        partial void OnAwake();
        partial void OnStart();
        partial void OnUpdate();
    }
}
