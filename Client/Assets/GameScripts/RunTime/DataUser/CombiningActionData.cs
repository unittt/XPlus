using DG.Tweening;
using HT.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameScripts.RunTime.DataUser
{

    /// <summary>
    /// 组合动作信息
    /// </summary>
    public struct CActionInfo
    {
        public string action;
        public int start_frame;
        public int hit_frame;
        public int end_frame;
        public int speed;
    }
    
    /// <summary>
    /// 组合动作文件
    /// </summary>
    public static class CombiningActionData
    {

        public static Dictionary<int, Dictionary<string, CActionInfo[]>> Data =
            new()
            {
                {
                    101, new Dictionary<string, CActionInfo[]>()
                    {
                         {
                              "attack1001", new CActionInfo[]
                              {
                                 new(){action = "attack3",end_frame = 7,hit_frame = 10,start_frame = 0},
                                 new(){action = "attack3",end_frame = 11,hit_frame = 10,start_frame = 0},
                                 new(){action = "attack3",end_frame = 11,hit_frame = 10,start_frame = 0},
                                 new(){action = "attack3",end_frame = 29,hit_frame = 0,start_frame = 0}
                              }
                    }
                }}
            };
    }
}

