namespace GameScripts.RunTime.DataUser
{
    public struct ClipInfo
    {
        public string Name;
        public int Frame;
        public float Length;
    }
    
    /// <summary>
    /// 动画片段数据
    /// </summary>
    // public static class AnimClipData
    // {
    // public static readonly Dictionary<int, ClipInfo[]> Data = new()
    // {
    //     {1110,  new ClipInfo[]
    //         {
    //             new ClipInfo(){Key = "attack1", Frame = 20, Length =  0.66f},
    //             new ClipInfo(){Key = "attack2", Frame = 24, Length =  0.66f},
    //             new ClipInfo(){Key = "attack3", Frame = 35, Length =  0.66f},
    //             new ClipInfo(){Key = "attack4", Frame = 29, Length =  0.66f},
    //             new ClipInfo(){Key = "attack5", Frame = 41, Length =  0.66f},
    //             new ClipInfo(){Key = "attack6", Frame = 32, Length =  0.66f},
    //             new ClipInfo(){Key = "attack7", Frame = 32, Length =  0.66f},
    //             new ClipInfo(){Key = "attack8", Frame = 43, Length =  0.66f},
    //             new ClipInfo(){Key = "attack9", Frame = 26, Length =  0.66f},
    //         } 
    //     },
    //     {1120,  new ClipInfo[]
    //         {
    //             new ClipInfo(){Key = "attack1", Frame = 20, Length =  0.66f},
    //             new ClipInfo(){Key = "attack2", Frame = 24, Length =  0.66f},
    //             new ClipInfo(){Key = "attack3", Frame = 35, Length =  0.66f},
    //             new ClipInfo(){Key = "attack4", Frame = 29, Length =  0.66f},
    //             new ClipInfo(){Key = "attack5", Frame = 41, Length =  0.66f},
    //             new ClipInfo(){Key = "attack6", Frame = 32, Length =  0.66f},
    //             new ClipInfo(){Key = "attack7", Frame = 32, Length =  0.66f},
    //             new ClipInfo(){Key = "attack8", Frame = 43, Length =  0.66f},
    //             new ClipInfo(){Key = "attack9", Frame = 26, Length =  0.66f},
    //         } 
    //     }
    // };
    //
    //     /// <summary>
    //     ///  获得动画片段信息
    //     /// </summary>
    //     /// <param name="id"></param>
    //     /// <param name="clipName"></param>
    //     /// <param name="clipInfo"></param>
    //     /// <returns></returns>
    //     public static bool TryGetAnimClipInfo(int id, string clipName, out ClipInfo clipInfo)
    //     {
    //         var result = false;
    //         clipInfo = default;
    //         if (Data.TryGetValue(id, out var clipInfos))
    //         {
    //             foreach (var info in clipInfos)
    //             {
    //                 if (info.Key != clipName) continue;
    //                 clipInfo = info;
    //                 result = true;
    //                 break;
    //             }
    //         }
    //         
    //         return result;
    //     }
    // }
}