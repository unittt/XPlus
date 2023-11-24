using System;
using Cysharp.Threading.Tasks;
using GridMap;
using HT.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScript.RunTime.Procedure
{
    public class ProcedureGame : ProcedureBase
    {
        public static event Action<float> OnLoadingGameScene; 

        public override void OnEnter(ProcedureBase lastProcedure)
        {
            base.OnEnter(lastProcedure);
      
            // Main.m_UI.OpenUI<UILoading>();
            // SceneInfo sceneInfo = new SceneInfo("scene", "Assets/GameRes/Scene/Game.unity", "Game");
            // Main.m_Resource.LoadScene(sceneInfo,OnLoading, OnLoadDone);
            InitAsync().Forget();
        }


        private async UniTask InitAsync()
        {
            await Main.m_Resource.LoadSceneAsync("Game",null,LoadSceneMode.Additive);
            //等待加载地图

            var textAsset = await Main.m_Resource.LoadAsset<TextAsset>("mapdata_1010");
            MapData mapData = MapData.Deserialize(textAsset.text);
            var mapManager = GameObject.Find("GridMapManager").GetComponent<MapManager>();
            mapManager.SetMapData(mapData,false);
            //等待加载角色
        }
        
        private void OnLoading(float arg)
        {
            OnLoadingGameScene?.Invoke(arg);
        }
   
        private void OnLoadDone()
        {
            // Main.m_UI.DestroyUI<UILoading>();
            // Main.m_UI.OpenUI<UIMain>();
        }
   
        public override void OnLeave(ProcedureBase nextProcedure)
        {
            base.OnLeave(nextProcedure);
        }
    }
}