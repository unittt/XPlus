using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
        private static void CreateAnimClipData()
        {
            EditorGlobalTools.CreateScriptFormTemplate(GameEditorPrefsTable.Script_AnimClipData_TemplatePath,GameEditorPrefsTable.Script_AnimClipData_Path,
                arg =>
                {
                    var sb = new StringBuilder();
                    var directories = Directory.GetDirectories("Assets/GameRes/Model/Character");
                    foreach (var characterPath in directories)
                    {
                        var match = Regex.Match(characterPath, @"\d+$");
                        if (!match.Success || !int.TryParse(match.Value, out  var shape))continue;
              
                        var path = $"Assets/GameRes/Model/Character/{shape.ToString()}";
                        var guids = AssetDatabase.FindAssets( "t:AnimationClip", new[] { path });
                
                        sb.AppendLine("{");
                        sb.AppendLine($"{shape.ToString()}, new ClipInfo[]");
                        sb.AppendLine("{");
                        foreach (var guid in guids)
                        {
                            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                            var animationClip  = AssetDatabase.LoadAssetAtPath<AnimationClip>(assetPath);
                            var frame = Mathf.FloorToInt(animationClip.length / (1 / animationClip.frameRate));
                            var context = $"new ClipInfo(){{Key = {animationClip.name}, Frame = {frame}, Length = {animationClip.length.ToString("F2")}f}}";
                            sb.AppendLine(context);
                        }
                        sb.AppendLine("}");
                        sb.AppendLine("},");
                    }
                    
                    arg = arg.Replace("#CODE#", sb.ToString());
                    return arg;
                });
        }
    }
}