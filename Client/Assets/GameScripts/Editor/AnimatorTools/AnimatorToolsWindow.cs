using System.Collections.Generic;
using GameScripts.RunTime.Utility.Selector;
using HT.Framework;
using UnityEditor;
using UnityEngine;

namespace GameScripts.Editor.AnimatorTools
{
    public class AnimatorToolsWindow : HTFEditorWindow
    {
        private List<string> _characterList = new List<string>();

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
            //获得所有模型
            // var guids = AssetDatabase.FindAssets(Filter, new[] { Path });
            // foreach (var guid in guids)
            // {
            //     var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            //     var assetName = System.IO.Path.GetFileNameWithoutExtension(assetPath);
            //     Terms.Add(assetName);
            // }
            
            
            SelectorManager.GetTerms<SelectorHandler_Character>(_characterList);
            foreach (var shape in _characterList)
            {
                
            }
        }
    }
}