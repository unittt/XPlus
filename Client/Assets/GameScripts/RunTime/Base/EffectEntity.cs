using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Base
{
    public abstract class EffectEntity : EntityLogicBase
    {

        private static int EffectIndex;
        public Transform RotateNode { get; private set; }

        private Action _callBack;
        private int _index;
        private GameObject _effectObj;

        public void Fill(int layer, string path, Action callBack)
        {
            _index = Interlocked.Increment(ref EffectIndex);
            _callBack = callBack;
            LoadCloneAsync(path).Forget();
        }

        private async UniTaskVoid LoadCloneAsync(string location)
        {
            var index = _index;
            var obj = await Main.m_Resource.LoadPrefab(location, Entity.transform);
            //如果当前已经回池了 删除这个
            if (index != _index)
            {
                Main.m_Resource.UnLoadAsset(obj);
                return;
            }

            _effectObj = obj;
            _callBack?.Invoke();
        }
        
        public void SetRotateNode(Transform node)
        {
            node.name = "rotate_node";
            var v = Entity.transform.localRotation;
            node.SetParent(Entity.transform.parent, true);
            Entity.transform.SetParent(node, true);
            RotateNode = node;
        }
        
        /// <summary>
        /// 处理贴图的平铺效果
        /// </summary>
        public void SetTiling()
        {
            
        }

        public void ProcessTiling()
        {
            
        }

        public void SetLayer()
        {
            
        }

        public void SetLoop()
        {
            
        }

        public void AutoDestroy()
        {
            
        }
    }
}