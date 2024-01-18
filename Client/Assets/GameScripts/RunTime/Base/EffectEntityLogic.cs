using System.Threading;
using Cysharp.Threading.Tasks;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Base
{
    public abstract class EffectEntityLogic : EntityLogicBase
    {

        private static int EffectIndex;
        public Transform RotateNode { get; private set; }
        
        private int _index;
        private GameObject _effectGO;

        /// <summary>
        /// 填充
        /// </summary>
        /// <param name="location">定位</param>
        /// <param name="layer">层级</param>
        /// <param name="isCached">是否缓存</param>
        public virtual void Fill(string location,int layer, bool isCached)
        {
            _index = Interlocked.Increment(ref EffectIndex);
            LoadCloneAsync(location).Forget();
        }

        private async UniTaskVoid LoadCloneAsync(string location)
        {
            var index = _index;
            var obj = await Main.m_Resource.LoadPrefab(location, null);
            //如果当前已经回池了 删除这个
            if (index != _index)
            {
                Main.m_Resource.UnLoadAsset(obj);
                return;
            }

            _effectGO = obj;
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
        
        public void SetLocalEulerAngles(Vector3 localEulerAngles)
        {
            if (Entity != null)
            {
                Entity.transform.localEulerAngles = localEulerAngles;
            }
        }
        
        public void SetLocalPos(Vector3 pos)
        {
            if (Entity != null)
            {
                Entity.transform.localPosition = pos;
            }
        }
        
        public Transform Parent => Entity?.transform.parent;
        
    }
}