using UnityEngine;

namespace GameScripts.RunTime.Utility.Selector
{

    /// <summary>
    /// 战斗音效选择
    /// </summary>
    public class SelectorHandler_WarSound:SelectorHandlerAsset<AudioClip>
    {
        protected override string Path =>"Assets/GameRes/Audio/Sound/War";
// #if UNITY_EDITOR
//         // private const string PATH = "Assets/GameRes/Audio/Sound/War";
//         public SelectorHandler_WarSound()
//         {
//             var guids = AssetDatabase.FindAssets("t:AudioClip", new[] { PATH });
//             foreach (var guid in guids)
//             {
//                 var assetPath = AssetDatabase.GUIDToAssetPath(guid);
//                 var assetName = System.IO.Path.GetFileNameWithoutExtension(assetPath);
//                 Terms.Add(assetName);
//             }
//         }
// #endif
        
        // internal override object GetTermValue(string value)
        // {
        //     return value;
        // }
    }
}