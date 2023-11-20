using System;
using GameScript.RunTime.UI;
using HT.Framework;

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