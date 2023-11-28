using System;
using Cysharp.Threading.Tasks;
using GameScripts.RunTime.Model;
using GridMap;
using HT.Framework;
using Pb.Mmo.Common;
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
            mapManager.SetMapData(mapData);
            
            //加载角色
            var roleEntity = await Main.m_Entity.CreateEntity<RoleEntity>();
            ModelInfo modelInfo = new ModelInfo();
            modelInfo.shape = 1110;
            modelInfo.weapon = 9;
            roleEntity.Fill(modelInfo);
            var mapTouchController = GameObject.Find("Touch").GetComponent<MapTouchController>();
            mapTouchController._walker = roleEntity.Walker;
            MapManager.Instance.SetFollow(roleEntity.Entity.transform);
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