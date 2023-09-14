using System;
using HT.Framework;

namespace GameScripts.RunTime.GridMapEditor
{
    /// <summary>
    /// 地图生成配置
    /// </summary>
    [Serializable]
    internal sealed class GridMapGeneratorConfig : DataSetBase
    {
        //编辑场景的路径
        public string EditorScenePath;

        public string RawDataPath;
        //寻路数据根目录
        public string NavDataRoot;
        //
        public string ConfigRoot;
    }
}