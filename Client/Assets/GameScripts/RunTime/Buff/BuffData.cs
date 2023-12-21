using UnityEngine;

namespace GameScripts.RunTime.Buff
{
    public class BuffData : ScriptableObject
    {
        //基础信息
        public int id;
        public string buffName;
        public string description;
        public string icon;
        public int priority;
        public int maxStack;
        public string tags;
        
        //时间信息
        public bool isForever;
        public float duration;
        public float tickTime;
        
        //更新方式
        public BuffTimeUpdateEnum buffUpdateTime;
        public BuffRemoveStackUpdateEnum buffRemoveStackUpdateEnum;
        
        //基础回调点
        public BaseBuffModule OnCreate;
        public BaseBuffModule OnRemove;
        public BaseBuffModule OnTick;
        
        //伤害回调点
        public BaseBuffModule OnHit;
        public BaseBuffModule OnBehurt;
        public BaseBuffModule OnKill;
        public BaseBuffModule OnBeKill;
    }
}