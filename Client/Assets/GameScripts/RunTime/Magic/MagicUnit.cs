using System.Collections.Generic;
using HT.Framework;

namespace GameScripts.RunTime.Magic
{
    /// <summary>
    /// 控制魔法效果的播放、管理、更新和结束
    /// </summary>
    public class MagicUnit: IReference
    {
        public int ID { get; private set; }
        public int Shape { get; private set; }
        public int MagicID { get; private set; }
        public int MagicIdx { get; private set; }
        private Dictionary<string, object> data = new(); // 用于存储攻击对象、受害对象等数据
        private List<MagicCmd> _commandList = new(); // 执行播放技能指令列表
        private float _elapsedTime; // 已执行时间
        private bool _isRunning;
        private bool _isActive;
        
        
        public void Reset()
        {
            data.Clear();
            _commandList.Clear();
        }
    }
}