using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using HT.Framework;
using UnityEditor;
using UnityEngine;

namespace GameScripts.Editor.AnimatorTools
{
    public class AnimatorToolsWindow : HTFEditorWindow
    {
     
        
        protected override void OnEnable()
        {
            base.OnEnable();
        }
        
        protected override void OnBodyGUI()
        {
            GUILayout.Label("更新动作时间:",EditorStyles.boldLabel);
            if (GUILayout.Button("导出", EditorStyles.miniButton))
            {
                GenAnimTimeData();
            }
        }

        private void GenAnimTimeData()
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
        }
    }
}