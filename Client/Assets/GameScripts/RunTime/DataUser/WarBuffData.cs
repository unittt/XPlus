using System.Collections.Generic;
using HT.Framework;

namespace GameScripts.RunTime.DataUser
{
    public class WarBuffData : DataSetBase
    {
        public Dictionary<int, BuffEffectData> Datas = new();
    }

    public class BuffEffectData
    {
        public int AddCnt { get; set; }
        public int BuffId { get; set; }
        public float Height { get; set; }
        public string Path { get; set; }
        public string Pos { get; set; }
    }
}