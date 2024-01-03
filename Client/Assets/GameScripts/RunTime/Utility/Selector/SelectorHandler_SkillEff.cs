using UnityEditor;

namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// 技能特效选择器
    /// </summary>
    public class SelectorHandler_SkillEff : SelectorHandler
    {
#if UNITY_EDITOR
        private const string PATH = "Assets/GameRes/Effect/Magic";
        public SelectorHandler_SkillEff()
        {
            var guids = AssetDatabase.FindAssets("t:GameObject", new[] { PATH });
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var assetName = System.IO.Path.GetFileNameWithoutExtension(assetPath);
                Terms.Add(assetName);
            }
        }
#endif
        internal override object GetTermValue(string value)
        {
            return value;
        }
    }
}