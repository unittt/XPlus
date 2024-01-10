using GameScripts.Editor.AnimatorTools;
using HT.Framework;
using UnityEditor;
using UnityEngine;

namespace GameScripts.Editor
{
    
    /// <summary>
    /// 游戏编辑器全局工具
    /// </summary>
    [InitializeOnLoad]
    public static class GameEditorGlobalTools
    {
        #region 构造函数
        static GameEditorGlobalTools()
        {
      
        }
        #endregion
        
  
        #region About 【优先级0】
        /// <summary>
        /// About
        /// </summary>
        // [MenuItem("游戏工具/动画", false, 0)]
        // private static void About()
        // {
        //     AnimatorToolsWindow cb = EditorWindow.GetWindow<AnimatorToolsWindow>();
        //     cb.titleContent.image = EditorGUIUtility.IconContent("d_editicon.sml").image;
        //     cb.titleContent.text = "动画片段信息导出";
        //     cb.position = new Rect(200, 200, 300, 165);
        //     cb.Show();
        // }
        #endregion
        
        
        /// <summary>
        /// 【验证函数】新建HotfixObject类
        /// </summary>
        [MenuItem("Game/HTFramework Settings...", false, 10002)]
        private static void CreateHotfixProcedure()
        {
            // EditorPrefs.SetString(EditorPrefsTable.Script_HotfixProcedure_Folder, "/Hotfix");
            EditorGlobalTools.CreateScriptFormTemplate("", "HotfixProcedure","AnimClipDataTemplate");
            // EditorGlobalTools.CreateScriptFormTemplate("", "HotfixProcedure", "Assets/GameScripts/Editor/Utility/Template/","AnimClipDataTemplate",OnHander);
        }

        private static string OnHander(string arg)
        {
            return "";
        }
    }
}