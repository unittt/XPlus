using System;
using HT.Framework;
using UnityEditor;

namespace GridMap
{
    /// <summary>
    /// 网格地图全局配置
    /// </summary>
    [Serializable]
    public sealed class GridMapGlobalConfig : DataSetBase
    {
        private const string PATH = "Assets/StandardAssets/GridMap/GridMapGlobalConfig.asset";
        
        public string EditorScenePath;
        public string RawDataPath;
        public string NavDataRootPath;
        public string ConfigRootPath;


        private static GridMapGlobalConfig _instance;
        public static GridMapGlobalConfig Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = AssetDatabase.LoadAssetAtPath<GridMapGlobalConfig>(PATH);
                if (_instance != null) return _instance;
                _instance = CreateInstance<GridMapGlobalConfig>();
                AssetDatabase.CreateAsset(_instance, PATH);
                return _instance;
            }
        }

        public void SaveAsset()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }
    }
}