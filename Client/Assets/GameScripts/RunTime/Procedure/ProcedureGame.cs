using System;
using Cysharp.Threading.Tasks;
using GameScript.RunTime.UI;
using GameScripts.RunTime.Model;
using GameScripts.RunTime.War;
using GridMap;
using HT.Framework;
using Pb.Mmo.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScript.RunTime.Procedure
{
    public class ProcedureGame : ProcedureBase
    {
        private MapWalker _mapWalkerEntity;
        public static event Action<float> OnLoadingGameScene; 

        
        public override void OnEnter(ProcedureBase lastProcedure)
        {
            base.OnEnter(lastProcedure);
      
            // Main.m_UI.OpenUI<UILoading>();
            // SceneInfo sceneInfo = new SceneInfo("scene", "Assets/GameRes/Scene/Game.unity", "Game");
            // Main.m_Resource.LoadScene(sceneInfo,OnLoading, OnLoadDone);

            Main.m_UI.OpenUI<UIMain>();
            InitAsync().Forget();
        }


        private async UniTask InitAsync()
        {
            await Main.m_Resource.LoadSceneAsync("Game",null,LoadSceneMode.Additive);
            
            //等待加载地图
            var textAsset = await Main.m_Resource.LoadAsset<TextAsset>("mapdata_1010");
            MapData mapData = MapData.Deserialize(textAsset.text);
            var mapManager = GameObject.Find("GridMapManager").GetComponent<MapManager>();
            mapManager.SetMapData(mapData);
            
            //加载角色
            _mapWalkerEntity = await Main.m_Entity.CreateEntity<MapWalker>();
            ModelInfo modelInfo = new ModelInfo();
            modelInfo.shape = 1110;
            modelInfo.horse = 4004;
            modelInfo.weapon = 9;
            _mapWalkerEntity.AssembleModel(modelInfo);
            var mapTouchController = GameObject.Find("Touch").GetComponent<MapTouchController>();
            mapTouchController._walker = _mapWalkerEntity.Agent;
            MapManager.Instance.SetFollow(_mapWalkerEntity.Entity.transform);
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