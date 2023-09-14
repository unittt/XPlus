using UnityEngine;

namespace GameScripts.RunTime.Map
{
    /// <summary>
    /// 2d 地图特效
    /// </summary>
    public class Map2DEffect
    {
        private GridMapEffectData effectData;
        private GameObject effectGo;
        private GameObject effectRoot = null;
        private bool isLoaded = false;

        public Map2DEffect(GameObject root, GridMapEffectData data)
        {
            this.effectRoot = root;
            this.effectData = data;
            // ResourceManager.LoadAsync(data.name, OnLoadEffect);
        }

        public void SetShow(bool isShow)
        {
            if (isShow == true && effectGo == null && !isLoaded)
            {
                isLoaded = true;
                // ResourceManager.LoadAsync(effectData.name, OnLoadEffect);
            }

            if (effectGo != null)
            {
                effectGo.SetActive(isShow);
            }
        }

        public void CheckShow(Bounds cameraBounds)
        {
            bool show = cameraBounds.Contains(effectData.pos);
            SetShow(show);
        }

        private void LoadPrefab(GameObject prefab)
        {
            if (this.effectRoot == null) //父节点已经销毁
            {
                return;
            }

            this.effectGo = GameObject.Instantiate<GameObject>(prefab);
            Transform transform = this.effectGo.transform;
            transform.parent = this.effectRoot.transform;
            transform.rotation = Quaternion.Euler(effectData.rotation);
            transform.localScale = effectData.scale;
            transform.localPosition = new Vector3(effectData.pos.x, effectData.pos.y, 0);
            // ResourceManager.AddAssetBundleRef(effectData.name);
        }

//         private void OnLoadEffect(object asset, LoadErrorCode error)
//         {
//             if (asset != null)
//             {
//                 GameObject prefab = (GameObject)asset;
//                 LoadPrefab(prefab);
//             }
//             else
//             {
// #if UNITY_EDITOR
//                 Debug.LogError("OnLoadEffect Error! " + effectData.name);
// #endif
//             }
//         }

        public void Release()
        {
            if (effectGo != null)
            {
                GameObject.Destroy(effectGo);
            }
            // ResourceManager.DelAssetBundleRef(effectData.name);
            this.effectGo = null;
            this.effectRoot = null;
        }
    }
}