using System;
using System.Collections.Generic;
using GameScripts.RunTime.Magic.Command;
using GameScripts.RunTime.Utility;

namespace GameScripts.RunTime.Magic
{
    /// <summary>
    /// 法术数据
    /// </summary>
    [Serializable]
    public sealed class MagicData
    {
        
        public List<CommandBase> Commands  = new();
        
        // 当需要序列化时调用这个方法
        public byte[] SerializeCommands()
        {
            return SerializationHelper.SerializeToBytes(this);
        }
        
        public static MagicData Deserialize(byte[] bytes)
        {
            var magicData = SerializationHelper.DeserializeFromBytes<MagicData>(bytes);
            return magicData;
        } 
    }
}