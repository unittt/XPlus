using System.Collections.Generic;

namespace GameScripts.RunTime.DataUser
{
    public struct ClipInfo
    {
        public string Key;
        public int Frame;
        public float Length;
    }
    
	/// <summary>
    /// 动画片段数据
	/// </summary>
    public static class AnimClipData
    {
        public static readonly Dictionary<int, ClipInfo[]> Data = new()
        {
          {
1110, new ClipInfo[]
{
new(){Key = "attack1", Frame = 20, Length = 0.67f},
new(){Key = "attack2", Frame = 24, Length = 0.80f},
new(){Key = "attack3", Frame = 35, Length = 1.17f},
new(){Key = "attack4", Frame = 29, Length = 0.97f},
new(){Key = "attack5", Frame = 41, Length = 1.37f},
new(){Key = "attack6", Frame = 32, Length = 1.07f},
new(){Key = "attack7", Frame = 50, Length = 1.67f},
new(){Key = "attack8", Frame = 43, Length = 1.43f},
new(){Key = "attack9", Frame = 26, Length = 0.87f},
new(){Key = "dance", Frame = 150, Length = 5.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 26, Length = 0.87f},
new(){Key = "hit1", Frame = 7, Length = 0.23f},
new(){Key = "hit2", Frame = 6, Length = 0.20f},
new(){Key = "hitCity", Frame = 30, Length = 1.00f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 33, Length = 1.10f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "runBack", Frame = 20, Length = 0.67f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 14, Length = 0.47f},
new(){Key = "show", Frame = 120, Length = 4.00f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "rolecreate1", Frame = 86, Length = 2.87f},
new(){Key = "rolecreate2", Frame = 40, Length = 1.33f},
new(){Key = "rolecreate3", Frame = 110, Length = 3.67f},
}
},
{
1120, new ClipInfo[]
{
new(){Key = "attack1", Frame = 21, Length = 0.70f},
new(){Key = "attack2", Frame = 38, Length = 1.27f},
new(){Key = "attack3", Frame = 36, Length = 1.20f},
new(){Key = "attack4", Frame = 47, Length = 1.57f},
new(){Key = "attack5", Frame = 22, Length = 0.73f},
new(){Key = "attack6", Frame = 22, Length = 0.73f},
new(){Key = "attack7", Frame = 33, Length = 1.10f},
new(){Key = "attack8", Frame = 37, Length = 1.23f},
new(){Key = "dance", Frame = 130, Length = 4.33f},
new(){Key = "defend", Frame = 21, Length = 0.70f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 38, Length = 1.27f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runBack", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 12, Length = 0.40f},
new(){Key = "show", Frame = 137, Length = 4.57f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "rolecreate1", Frame = 100, Length = 3.33f},
new(){Key = "rolecreate2", Frame = 40, Length = 1.33f},
new(){Key = "rolecreate3", Frame = 137, Length = 4.57f},
}
},
{
1130, new ClipInfo[]
{
new(){Key = "attack1", Frame = 21, Length = 0.70f},
new(){Key = "attack2", Frame = 24, Length = 0.80f},
new(){Key = "attack3", Frame = 35, Length = 1.17f},
new(){Key = "attack4", Frame = 29, Length = 0.97f},
new(){Key = "attack5", Frame = 41, Length = 1.37f},
new(){Key = "attack6", Frame = 32, Length = 1.07f},
new(){Key = "attack7", Frame = 50, Length = 1.67f},
new(){Key = "attack8", Frame = 43, Length = 1.43f},
new(){Key = "attack9", Frame = 26, Length = 0.87f},
new(){Key = "dance", Frame = 150, Length = 5.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 23, Length = 0.77f},
new(){Key = "hit1", Frame = 6, Length = 0.20f},
new(){Key = "hit2", Frame = 6, Length = 0.20f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 33, Length = 1.10f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "runBack", Frame = 20, Length = 0.67f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 14, Length = 0.47f},
new(){Key = "show", Frame = 120, Length = 4.00f},
new(){Key = "walk", Frame = 33, Length = 1.10f},
}
},
{
1131, new ClipInfo[]
{
new(){Key = "attack1", Frame = 20, Length = 0.67f},
new(){Key = "attack2", Frame = 24, Length = 0.80f},
new(){Key = "attack3", Frame = 35, Length = 1.17f},
new(){Key = "attack4", Frame = 29, Length = 0.97f},
new(){Key = "attack5", Frame = 41, Length = 1.37f},
new(){Key = "attack6", Frame = 32, Length = 1.07f},
new(){Key = "attack7", Frame = 50, Length = 1.67f},
new(){Key = "attack8", Frame = 43, Length = 1.43f},
new(){Key = "attack9", Frame = 26, Length = 0.87f},
new(){Key = "dance", Frame = 150, Length = 5.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 23, Length = 0.77f},
new(){Key = "hit1", Frame = 7, Length = 0.23f},
new(){Key = "hit2", Frame = 7, Length = 0.23f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 33, Length = 1.10f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "runBack", Frame = 20, Length = 0.67f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 14, Length = 0.47f},
new(){Key = "show", Frame = 120, Length = 4.00f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "marryBaitang", Frame = 50, Length = 1.67f},
new(){Key = "marryHug", Frame = 80, Length = 2.67f},
new(){Key = "marryKiss", Frame = 70, Length = 2.33f},
}
},
{
1132, new ClipInfo[]
{
new(){Key = "attack1", Frame = 20, Length = 0.67f},
new(){Key = "attack2", Frame = 24, Length = 0.80f},
new(){Key = "attack3", Frame = 35, Length = 1.17f},
new(){Key = "attack4", Frame = 29, Length = 0.97f},
new(){Key = "attack5", Frame = 29, Length = 0.97f},
new(){Key = "attack6", Frame = 32, Length = 1.07f},
new(){Key = "attack7", Frame = 50, Length = 1.67f},
new(){Key = "attack8", Frame = 43, Length = 1.43f},
new(){Key = "attack9", Frame = 26, Length = 0.87f},
new(){Key = "dance", Frame = 150, Length = 5.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 23, Length = 0.77f},
new(){Key = "hit1", Frame = 7, Length = 0.23f},
new(){Key = "hit2", Frame = 7, Length = 0.23f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 33, Length = 1.10f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 14, Length = 0.47f},
new(){Key = "show", Frame = 120, Length = 4.00f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "marryBaitang", Frame = 50, Length = 1.67f},
new(){Key = "marryHug", Frame = 80, Length = 2.67f},
new(){Key = "marryKiss", Frame = 70, Length = 2.33f},
}
},
{
1170, new ClipInfo[]
{
new(){Key = "attack1", Frame = 21, Length = 0.70f},
new(){Key = "attack2", Frame = 38, Length = 1.27f},
new(){Key = "attack3", Frame = 36, Length = 1.20f},
new(){Key = "attack4", Frame = 44, Length = 1.47f},
new(){Key = "attack5", Frame = 35, Length = 1.17f},
new(){Key = "attack6", Frame = 22, Length = 0.73f},
new(){Key = "attack7", Frame = 33, Length = 1.10f},
new(){Key = "attack8", Frame = 37, Length = 1.23f},
new(){Key = "dance", Frame = 130, Length = 4.33f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 21, Length = 0.70f},
new(){Key = "hit1", Frame = 7, Length = 0.23f},
new(){Key = "hit2", Frame = 7, Length = 0.23f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 38, Length = 1.27f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 12, Length = 0.40f},
new(){Key = "show", Frame = 137, Length = 4.57f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
1171, new ClipInfo[]
{
new(){Key = "attack1", Frame = 21, Length = 0.70f},
new(){Key = "attack2", Frame = 38, Length = 1.27f},
new(){Key = "attack3", Frame = 36, Length = 1.20f},
new(){Key = "attack4", Frame = 47, Length = 1.57f},
new(){Key = "attack5", Frame = 35, Length = 1.17f},
new(){Key = "attack6", Frame = 22, Length = 0.73f},
new(){Key = "attack7", Frame = 33, Length = 1.10f},
new(){Key = "attack8", Frame = 37, Length = 1.23f},
new(){Key = "dance", Frame = 130, Length = 4.33f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 38, Length = 1.27f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 12, Length = 0.40f},
new(){Key = "show", Frame = 137, Length = 4.57f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "marryBaitang", Frame = 50, Length = 1.67f},
new(){Key = "marryHug", Frame = 80, Length = 2.67f},
new(){Key = "marryKiss", Frame = 70, Length = 2.33f},
}
},
{
1172, new ClipInfo[]
{
new(){Key = "attack1", Frame = 21, Length = 0.70f},
new(){Key = "attack2", Frame = 38, Length = 1.27f},
new(){Key = "attack3", Frame = 38, Length = 1.27f},
new(){Key = "attack4", Frame = 47, Length = 1.57f},
new(){Key = "attack5", Frame = 35, Length = 1.17f},
new(){Key = "attack6", Frame = 22, Length = 0.73f},
new(){Key = "attack7", Frame = 33, Length = 1.10f},
new(){Key = "attack8", Frame = 37, Length = 1.23f},
new(){Key = "dance", Frame = 130, Length = 4.33f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 7, Length = 0.23f},
new(){Key = "hit2", Frame = 7, Length = 0.23f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 38, Length = 1.27f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 12, Length = 0.40f},
new(){Key = "show", Frame = 137, Length = 4.57f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "marryBaitang", Frame = 50, Length = 1.67f},
new(){Key = "marryHug", Frame = 80, Length = 2.67f},
new(){Key = "marryKiss", Frame = 70, Length = 2.33f},
}
},
{
1210, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "dance", Frame = 135, Length = 4.50f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 6, Length = 0.20f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 38, Length = 1.27f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 110, Length = 3.67f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "rolecreate1", Frame = 100, Length = 3.33f},
new(){Key = "rolecreate2", Frame = 40, Length = 1.33f},
new(){Key = "rolecreate3", Frame = 90, Length = 3.00f},
}
},
{
1220, new ClipInfo[]
{
new(){Key = "attack1", Frame = 33, Length = 1.10f},
new(){Key = "dance", Frame = 160, Length = 5.33f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 6, Length = 0.20f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 77, Length = 2.57f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "rolecreate1", Frame = 100, Length = 3.33f},
new(){Key = "rolecreate2", Frame = 40, Length = 1.33f},
new(){Key = "rolecreate3", Frame = 73, Length = 2.43f},
}
},
{
1230, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "dance", Frame = 135, Length = 4.50f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 38, Length = 1.27f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 110, Length = 3.67f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
1231, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "dance", Frame = 135, Length = 4.50f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 6, Length = 0.20f},
new(){Key = "hit2", Frame = 6, Length = 0.20f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 38, Length = 1.27f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 110, Length = 3.67f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "marryBaitang", Frame = 50, Length = 1.67f},
new(){Key = "marryHug", Frame = 80, Length = 2.67f},
new(){Key = "marryKiss", Frame = 70, Length = 2.33f},
}
},
{
1232, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "dance", Frame = 135, Length = 4.50f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 6, Length = 0.20f},
new(){Key = "hit2", Frame = 6, Length = 0.20f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 38, Length = 1.27f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 110, Length = 3.67f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "marryBaitang", Frame = 50, Length = 1.67f},
new(){Key = "marryHug", Frame = 80, Length = 2.67f},
new(){Key = "marryKiss", Frame = 70, Length = 2.33f},
}
},
{
1270, new ClipInfo[]
{
new(){Key = "attack1", Frame = 33, Length = 1.10f},
new(){Key = "dance", Frame = 160, Length = 5.33f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 12, Length = 0.40f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 77, Length = 2.57f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
1271, new ClipInfo[]
{
new(){Key = "attack1", Frame = 33, Length = 1.10f},
new(){Key = "dance", Frame = 160, Length = 5.33f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 6, Length = 0.20f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 77, Length = 2.57f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "marryBaitang", Frame = 50, Length = 1.67f},
new(){Key = "marryHug", Frame = 80, Length = 2.67f},
new(){Key = "marryKiss", Frame = 70, Length = 2.33f},
}
},
{
1272, new ClipInfo[]
{
new(){Key = "attack1", Frame = 33, Length = 1.10f},
new(){Key = "dance", Frame = 160, Length = 5.33f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 6, Length = 0.20f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 77, Length = 2.57f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "marryBaitang", Frame = 50, Length = 1.67f},
new(){Key = "marryHug", Frame = 80, Length = 2.67f},
new(){Key = "marryKiss", Frame = 70, Length = 2.33f},
}
},
{
1310, new ClipInfo[]
{
new(){Key = "attack1", Frame = 34, Length = 1.13f},
new(){Key = "attack2", Frame = 30, Length = 1.00f},
new(){Key = "attack3", Frame = 40, Length = 1.33f},
new(){Key = "attack4", Frame = 48, Length = 1.60f},
new(){Key = "attack5", Frame = 60, Length = 2.00f},
new(){Key = "attack6", Frame = 24, Length = 0.80f},
new(){Key = "attack7", Frame = 34, Length = 1.13f},
new(){Key = "dance", Frame = 152, Length = 5.07f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 11, Length = 0.37f},
new(){Key = "show", Frame = 90, Length = 3.00f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "rolecreate1", Frame = 78, Length = 2.60f},
new(){Key = "rolecreate2", Frame = 40, Length = 1.33f},
new(){Key = "rolecreate3", Frame = 82, Length = 2.73f},
}
},
{
1320, new ClipInfo[]
{
new(){Key = "attack1", Frame = 28, Length = 0.93f},
new(){Key = "attack2", Frame = 33, Length = 1.10f},
new(){Key = "attack3", Frame = 37, Length = 1.23f},
new(){Key = "attack4", Frame = 34, Length = 1.13f},
new(){Key = "attack5", Frame = 38, Length = 1.27f},
new(){Key = "attack6", Frame = 49, Length = 1.63f},
new(){Key = "dance", Frame = 125, Length = 4.17f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 40, Length = 1.33f},
new(){Key = "magic", Frame = 36, Length = 1.20f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 125, Length = 4.17f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "rolecreate1", Frame = 97, Length = 3.23f},
new(){Key = "rolecreate2", Frame = 40, Length = 1.33f},
new(){Key = "rolecreate3", Frame = 100, Length = 3.33f},
}
},
{
1330, new ClipInfo[]
{
new(){Key = "attack1", Frame = 34, Length = 1.13f},
new(){Key = "attack2", Frame = 31, Length = 1.03f},
new(){Key = "attack3", Frame = 40, Length = 1.33f},
new(){Key = "attack4", Frame = 48, Length = 1.60f},
new(){Key = "attack5", Frame = 58, Length = 1.93f},
new(){Key = "attack6", Frame = 24, Length = 0.80f},
new(){Key = "attack7", Frame = 33, Length = 1.10f},
new(){Key = "dance", Frame = 153, Length = 5.10f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 11, Length = 0.37f},
new(){Key = "show", Frame = 90, Length = 3.00f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "marryBaitang", Frame = 50, Length = 1.67f},
new(){Key = "marryHug", Frame = 80, Length = 2.67f},
new(){Key = "marryKiss", Frame = 70, Length = 2.33f},
}
},
{
1331, new ClipInfo[]
{
new(){Key = "attack1", Frame = 34, Length = 1.13f},
new(){Key = "attack2", Frame = 30, Length = 1.00f},
new(){Key = "attack3", Frame = 40, Length = 1.33f},
new(){Key = "attack4", Frame = 48, Length = 1.60f},
new(){Key = "attack5", Frame = 58, Length = 1.93f},
new(){Key = "attack6", Frame = 24, Length = 0.80f},
new(){Key = "attack7", Frame = 29, Length = 0.97f},
new(){Key = "dance", Frame = 152, Length = 5.07f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 11, Length = 0.37f},
new(){Key = "show", Frame = 90, Length = 3.00f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "marryBaitang", Frame = 50, Length = 1.67f},
new(){Key = "marryHug", Frame = 80, Length = 2.67f},
new(){Key = "marryKiss", Frame = 70, Length = 2.33f},
}
},
{
1332, new ClipInfo[]
{
new(){Key = "attack1", Frame = 34, Length = 1.13f},
new(){Key = "attack2", Frame = 30, Length = 1.00f},
new(){Key = "attack3", Frame = 40, Length = 1.33f},
new(){Key = "attack4", Frame = 48, Length = 1.60f},
new(){Key = "attack5", Frame = 58, Length = 1.93f},
new(){Key = "attack6", Frame = 24, Length = 0.80f},
new(){Key = "attack7", Frame = 24, Length = 0.80f},
new(){Key = "dance", Frame = 153, Length = 5.10f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 11, Length = 0.37f},
new(){Key = "show", Frame = 90, Length = 3.00f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "marryBaitang", Frame = 50, Length = 1.67f},
new(){Key = "marryHug", Frame = 80, Length = 2.67f},
new(){Key = "marryKiss", Frame = 70, Length = 2.33f},
}
},
{
1370, new ClipInfo[]
{
new(){Key = "attack1", Frame = 28, Length = 0.93f},
new(){Key = "attack2", Frame = 33, Length = 1.10f},
new(){Key = "attack3", Frame = 37, Length = 1.23f},
new(){Key = "attack4", Frame = 34, Length = 1.13f},
new(){Key = "attack5", Frame = 38, Length = 1.27f},
new(){Key = "attack6", Frame = 49, Length = 1.63f},
new(){Key = "dance", Frame = 125, Length = 4.17f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 16, Length = 0.53f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 40, Length = 1.33f},
new(){Key = "magic", Frame = 32, Length = 1.07f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 126, Length = 4.20f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
1371, new ClipInfo[]
{
new(){Key = "attack1", Frame = 28, Length = 0.93f},
new(){Key = "attack2", Frame = 33, Length = 1.10f},
new(){Key = "attack3", Frame = 37, Length = 1.23f},
new(){Key = "attack4", Frame = 34, Length = 1.13f},
new(){Key = "attack5", Frame = 38, Length = 1.27f},
new(){Key = "attack6", Frame = 49, Length = 1.63f},
new(){Key = "dance", Frame = 125, Length = 4.17f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 13, Length = 0.43f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 40, Length = 1.33f},
new(){Key = "magic", Frame = 32, Length = 1.07f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 125, Length = 4.17f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "marryBaitang", Frame = 50, Length = 1.67f},
new(){Key = "marryHug", Frame = 80, Length = 2.67f},
new(){Key = "marryKiss", Frame = 70, Length = 2.33f},
}
},
{
1372, new ClipInfo[]
{
new(){Key = "attack1", Frame = 28, Length = 0.93f},
new(){Key = "attack2", Frame = 33, Length = 1.10f},
new(){Key = "attack3", Frame = 37, Length = 1.23f},
new(){Key = "attack4", Frame = 37, Length = 1.23f},
new(){Key = "attack5", Frame = 38, Length = 1.27f},
new(){Key = "attack6", Frame = 49, Length = 1.63f},
new(){Key = "dance", Frame = 125, Length = 4.17f},
new(){Key = "defend", Frame = 2, Length = 0.07f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleRide", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 40, Length = 1.33f},
new(){Key = "magic", Frame = 32, Length = 1.07f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runRide", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 125, Length = 4.17f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "marryBaitang", Frame = 50, Length = 1.67f},
new(){Key = "marryHug", Frame = 80, Length = 2.67f},
new(){Key = "marryKiss", Frame = 70, Length = 2.33f},
}
},
{
2101, new ClipInfo[]
{
new(){Key = "attack1", Frame = 31, Length = 1.03f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 28, Length = 0.93f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 92, Length = 3.07f},
new(){Key = "show2", Frame = 172, Length = 5.73f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
2102, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "dance", Frame = 90, Length = 3.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 115, Length = 3.83f},
new(){Key = "show2", Frame = 90, Length = 3.00f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
2201, new ClipInfo[]
{
new(){Key = "attack1", Frame = 35, Length = 1.17f},
new(){Key = "attack2", Frame = 31, Length = 1.03f},
new(){Key = "attack3", Frame = 35, Length = 1.17f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 57, Length = 1.90f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 90, Length = 3.00f},
new(){Key = "show2", Frame = 100, Length = 3.33f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
2202, new ClipInfo[]
{
new(){Key = "attack1", Frame = 31, Length = 1.03f},
new(){Key = "attack2", Frame = 35, Length = 1.17f},
new(){Key = "attack3", Frame = 35, Length = 1.17f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 22, Length = 0.73f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 50, Length = 1.67f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 95, Length = 3.17f},
new(){Key = "show2", Frame = 120, Length = 4.00f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
2301, new ClipInfo[]
{
new(){Key = "attack1", Frame = 29, Length = 0.97f},
new(){Key = "dance", Frame = 120, Length = 4.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 17, Length = 0.57f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 118, Length = 3.93f},
new(){Key = "show2", Frame = 120, Length = 4.00f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
2302, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "dance", Frame = 45, Length = 1.50f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 18, Length = 0.60f},
new(){Key = "show", Frame = 45, Length = 1.50f},
new(){Key = "show2", Frame = 83, Length = 2.77f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
2401, new ClipInfo[]
{
new(){Key = "attack1", Frame = 35, Length = 1.17f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 110, Length = 3.67f},
new(){Key = "show2", Frame = 121, Length = 4.03f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
2402, new ClipInfo[]
{
new(){Key = "attack1", Frame = 16, Length = 0.53f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 7, Length = 0.23f},
new(){Key = "hit2", Frame = 7, Length = 0.23f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 29, Length = 0.97f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runBack", Frame = 24, Length = 0.80f},
new(){Key = "runWar", Frame = 14, Length = 0.47f},
new(){Key = "show", Frame = 90, Length = 3.00f},
new(){Key = "show2", Frame = 103, Length = 3.43f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
2501, new ClipInfo[]
{
new(){Key = "attack1", Frame = 24, Length = 0.80f},
new(){Key = "dance", Frame = 91, Length = 3.03f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 50, Length = 1.67f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 89, Length = 2.97f},
new(){Key = "show2", Frame = 91, Length = 3.03f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
2502, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "dance", Frame = 93, Length = 3.10f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 64, Length = 2.13f},
new(){Key = "show2", Frame = 93, Length = 3.10f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
2601, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "attack2", Frame = 22, Length = 0.73f},
new(){Key = "attack3", Frame = 40, Length = 1.33f},
new(){Key = "attack4", Frame = 38, Length = 1.27f},
new(){Key = "dance", Frame = 72, Length = 2.40f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 102, Length = 3.40f},
new(){Key = "show2", Frame = 72, Length = 2.40f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
2602, new ClipInfo[]
{
new(){Key = "attack1", Frame = 50, Length = 1.67f},
new(){Key = "attack2", Frame = 20, Length = 0.67f},
new(){Key = "attack3", Frame = 43, Length = 1.43f},
new(){Key = "attack4", Frame = 49, Length = 1.63f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 55, Length = 1.83f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 115, Length = 3.83f},
new(){Key = "show2", Frame = 155, Length = 5.17f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
3101, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 142, Length = 4.73f},
}
},
{
3102, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 80, Length = 2.67f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
3103, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 48, Length = 1.60f},
new(){Key = "show", Frame = 75, Length = 2.50f},
}
},
{
3104, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 109, Length = 3.63f},
}
},
{
3105, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 100, Length = 3.33f},
new(){Key = "Take 001", Frame = 100, Length = 3.33f},
}
},
{
3106, new ClipInfo[]
{
new(){Key = "attack1", Frame = 28, Length = 0.93f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 43, Length = 1.43f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 15, Length = 0.50f},
new(){Key = "show", Frame = 90, Length = 3.00f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
3107, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "migic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 22, Length = 0.73f},
new(){Key = "runWar", Frame = 15, Length = 0.50f},
new(){Key = "show", Frame = 92, Length = 3.07f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
3108, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 48, Length = 1.60f},
new(){Key = "show", Frame = 56, Length = 1.87f},
}
},
{
3110, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 90, Length = 3.00f},
}
},
{
3111, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 93, Length = 3.10f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
3112, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 85, Length = 2.83f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
3113, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 18, Length = 0.60f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 40, Length = 1.33f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runBack", Frame = 24, Length = 0.80f},
new(){Key = "runWar", Frame = 9, Length = 0.30f},
new(){Key = "show", Frame = 63, Length = 2.10f},
}
},
{
3114, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 108, Length = 3.60f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
3115, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 165, Length = 5.50f},
}
},
{
3116, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 14, Length = 0.47f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 28, Length = 0.93f},
new(){Key = "magic", Frame = 50, Length = 1.67f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runBack", Frame = 24, Length = 0.80f},
new(){Key = "runWar", Frame = 6, Length = 0.20f},
new(){Key = "show", Frame = 81, Length = 2.70f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
3117, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 100, Length = 3.33f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
3119, new ClipInfo[]
{
new(){Key = "attack1", Frame = 33, Length = 1.10f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 53, Length = 1.77f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "runBack", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 110, Length = 3.67f},
}
},
{
3120, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 215, Length = 7.17f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
3121, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 80, Length = 2.67f},
}
},
{
3122, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 55, Length = 1.83f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
3123, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 80, Length = 2.67f},
new(){Key = "walk", Frame = 40, Length = 1.33f},
}
},
{
3124, new ClipInfo[]
{
new(){Key = "attack1", Frame = 22, Length = 0.73f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 4, Length = 0.13f},
new(){Key = "hit2", Frame = 3, Length = 0.10f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 43, Length = 1.43f},
new(){Key = "run", Frame = 28, Length = 0.93f},
new(){Key = "runWar", Frame = 12, Length = 0.40f},
new(){Key = "show", Frame = 93, Length = 3.10f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
3125, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 75, Length = 2.50f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
3126, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 117, Length = 3.90f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
3127, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 48, Length = 1.60f},
new(){Key = "show", Frame = 77, Length = 2.57f},
new(){Key = "walk", Frame = 42, Length = 1.40f},
}
},
{
3128, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 32, Length = 1.07f},
new(){Key = "run", Frame = 16, Length = 0.53f},
new(){Key = "show", Frame = 75, Length = 2.50f},
}
},
{
3129, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 48, Length = 1.60f},
new(){Key = "show", Frame = 50, Length = 1.67f},
}
},
{
3130, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 90, Length = 3.00f},
}
},
{
3131, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 69, Length = 2.30f},
}
},
{
3132, new ClipInfo[]
{
new(){Key = "attack1", Frame = 27, Length = 0.90f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 4, Length = 0.13f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 48, Length = 1.60f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 12, Length = 0.40f},
new(){Key = "show", Frame = 60, Length = 2.00f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
3134, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
}
},
{
4000, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 20, Length = 0.67f},
}
},
{
4001, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 20, Length = 0.67f},
}
},
{
4002, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 20, Length = 0.67f},
}
},
{
4003, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 20, Length = 0.67f},
}
},
{
4004, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 18, Length = 0.60f},
}
},
{
4005, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 20, Length = 0.67f},
}
},
{
4006, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 20, Length = 0.67f},
}
},
{
4007, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 20, Length = 0.67f},
}
},
{
4008, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 20, Length = 0.67f},
}
},
{
5101, new ClipInfo[]
{
new(){Key = "attack1", Frame = 33, Length = 1.10f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show1", Frame = 102, Length = 3.40f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5102, new ClipInfo[]
{
new(){Key = "attack1", Frame = 17, Length = 0.57f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 21, Length = 0.70f},
new(){Key = "hit1", Frame = 7, Length = 0.23f},
new(){Key = "hit2", Frame = 7, Length = 0.23f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 46, Length = 1.53f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runWar", Frame = 14, Length = 0.47f},
new(){Key = "show", Frame = 82, Length = 2.73f},
}
},
{
5103, new ClipInfo[]
{
new(){Key = "attack1", Frame = 28, Length = 0.93f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 28, Length = 0.93f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 18, Length = 0.60f},
new(){Key = "runWar", Frame = 18, Length = 0.60f},
new(){Key = "show", Frame = 100, Length = 3.33f},
new(){Key = "walk", Frame = 28, Length = 0.93f},
}
},
{
5104, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 22, Length = 0.73f},
new(){Key = "runWar", Frame = 18, Length = 0.60f},
new(){Key = "show", Frame = 115, Length = 3.83f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5105, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 18, Length = 0.60f},
new(){Key = "show", Frame = 120, Length = 4.00f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5106, new ClipInfo[]
{
new(){Key = "attack1", Frame = 20, Length = 0.67f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 7, Length = 0.23f},
new(){Key = "hit2", Frame = 7, Length = 0.23f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 33, Length = 1.10f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runBack", Frame = 16, Length = 0.53f},
new(){Key = "runwar", Frame = 14, Length = 0.47f},
new(){Key = "show", Frame = 70, Length = 2.33f},
new(){Key = "walk", Frame = 24, Length = 0.80f},
}
},
{
5107, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 4, Length = 0.13f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 105, Length = 3.50f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5108, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 7, Length = 0.23f},
new(){Key = "hit2", Frame = 7, Length = 0.23f},
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 42, Length = 1.40f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 56, Length = 1.87f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5109, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 41, Length = 1.37f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 75, Length = 2.50f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5110, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 18, Length = 0.60f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 44, Length = 1.47f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 159, Length = 5.30f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5111, new ClipInfo[]
{
new(){Key = "attack1", Frame = 33, Length = 1.10f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show1", Frame = 102, Length = 3.40f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5112, new ClipInfo[]
{
new(){Key = "attack1", Frame = 18, Length = 0.60f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 7, Length = 0.23f},
new(){Key = "hit2", Frame = 7, Length = 0.23f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 50, Length = 1.67f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 16, Length = 0.53f},
new(){Key = "show", Frame = 120, Length = 4.00f},
}
},
{
5113, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 9, Length = 0.30f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 125, Length = 4.17f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5114, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 22, Length = 0.73f},
new(){Key = "show", Frame = 70, Length = 2.33f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5115, new ClipInfo[]
{
new(){Key = "attack1", Frame = 20, Length = 0.67f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 27, Length = 0.90f},
new(){Key = "hit", Frame = 17, Length = 0.57f},
new(){Key = "hit1", Frame = 7, Length = 0.23f},
new(){Key = "hit2", Frame = 7, Length = 0.23f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 29, Length = 0.97f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "runBack", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 14, Length = 0.47f},
new(){Key = "show", Frame = 112, Length = 3.73f},
}
},
{
5116, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 130, Length = 4.33f},
}
},
{
5117, new ClipInfo[]
{
new(){Key = "attack1", Frame = 35, Length = 1.17f},
new(){Key = "attack2", Frame = 45, Length = 1.50f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 45, Length = 1.50f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 130, Length = 4.33f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "Take 001", Frame = 14, Length = 0.47f},
}
},
{
5118, new ClipInfo[]
{
new(){Key = "attack1", Frame = 34, Length = 1.13f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 36, Length = 1.20f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 90, Length = 3.00f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5119, new ClipInfo[]
{
new(){Key = "attack1", Frame = 35, Length = 1.17f},
new(){Key = "defend", Frame = 35, Length = 1.17f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 46, Length = 1.53f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 67, Length = 2.23f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5120, new ClipInfo[]
{
new(){Key = "attack1", Frame = 35, Length = 1.17f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 4, Length = 0.13f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 45, Length = 1.50f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 80, Length = 2.67f},
}
},
{
5121, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 2, Length = 0.07f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 4, Length = 0.13f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 50, Length = 1.67f},
new(){Key = "run", Frame = 16, Length = 0.53f},
new(){Key = "show", Frame = 130, Length = 4.33f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5122, new ClipInfo[]
{
new(){Key = "attack1", Frame = 45, Length = 1.50f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 45, Length = 1.50f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 85, Length = 2.83f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5123, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 3, Length = 0.10f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 38, Length = 1.27f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 90, Length = 3.00f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5124, new ClipInfo[]
{
new(){Key = "attack1", Frame = 29, Length = 0.97f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 40, Length = 1.33f},
new(){Key = "magic", Frame = 26, Length = 0.87f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 87, Length = 2.90f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5125, new ClipInfo[]
{
new(){Key = "attack1", Frame = 28, Length = 0.93f},
new(){Key = "defend", Frame = 7, Length = 0.23f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 56, Length = 1.87f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 125, Length = 4.17f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5126, new ClipInfo[]
{
new(){Key = "attack1", Frame = 28, Length = 0.93f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 48, Length = 1.60f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5127, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 8, Length = 0.27f},
new(){Key = "hit2", Frame = 6, Length = 0.20f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 95, Length = 3.17f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5128, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 6, Length = 0.20f},
new(){Key = "hit2", Frame = 3, Length = 0.10f},
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 37, Length = 1.23f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 140, Length = 4.67f},
new(){Key = "walk", Frame = 28, Length = 0.93f},
}
},
{
5129, new ClipInfo[]
{
new(){Key = "attack1", Frame = 28, Length = 0.93f},
new(){Key = "defend", Frame = 14, Length = 0.47f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 34, Length = 1.13f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 80, Length = 2.67f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "Take 001", Frame = 100, Length = 3.33f},
}
},
{
5130, new ClipInfo[]
{
new(){Key = "attack1", Frame = 37, Length = 1.23f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 44, Length = 1.47f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 109, Length = 3.63f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5131, new ClipInfo[]
{
new(){Key = "attack1", Frame = 25, Length = 0.83f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 16, Length = 0.53f},
new(){Key = "runWar", Frame = 48, Length = 1.60f},
new(){Key = "show", Frame = 170, Length = 5.67f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5132, new ClipInfo[]
{
new(){Key = "attack1", Frame = 40, Length = 1.33f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 18, Length = 0.60f},
new(){Key = "hit1", Frame = 4, Length = 0.13f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 50, Length = 1.67f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 159, Length = 5.30f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5133, new ClipInfo[]
{
new(){Key = "attack1", Frame = 17, Length = 0.57f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 4, Length = 0.13f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 40, Length = 1.33f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 22, Length = 0.73f},
new(){Key = "runBack", Frame = 22, Length = 0.73f},
new(){Key = "runWar", Frame = 10, Length = 0.33f},
new(){Key = "show", Frame = 82, Length = 2.73f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5134, new ClipInfo[]
{
new(){Key = "attack1", Frame = 23, Length = 0.77f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 50, Length = 1.67f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "runWar", Frame = 12, Length = 0.40f},
new(){Key = "show", Frame = 56, Length = 1.87f},
}
},
{
5135, new ClipInfo[]
{
new(){Key = "attack1", Frame = 63, Length = 2.10f},
new(){Key = "defend", Frame = 6, Length = 0.20f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 35, Length = 1.17f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 150, Length = 5.00f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5136, new ClipInfo[]
{
new(){Key = "attack1", Frame = 44, Length = 1.47f},
new(){Key = "attack2", Frame = 19, Length = 0.63f},
new(){Key = "defend", Frame = 30, Length = 1.00f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 28, Length = 0.93f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 120, Length = 4.00f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5137, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 8, Length = 0.27f},
new(){Key = "hit2", Frame = 6, Length = 0.20f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 95, Length = 3.17f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
5138, new ClipInfo[]
{
new(){Key = "attack1", Frame = 35, Length = 1.17f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 48, Length = 1.60f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 130, Length = 4.33f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5139, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 25, Length = 0.83f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 36, Length = 1.20f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 70, Length = 2.33f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5140, new ClipInfo[]
{
new(){Key = "attack1", Frame = 29, Length = 0.97f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 31, Length = 1.03f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "runWar", Frame = 11, Length = 0.37f},
new(){Key = "show", Frame = 64, Length = 2.13f},
new(){Key = "walk", Frame = 28, Length = 0.93f},
}
},
{
5141, new ClipInfo[]
{
new(){Key = "attack1", Frame = 40, Length = 1.33f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 7, Length = 0.23f},
new(){Key = "hit2", Frame = 6, Length = 0.23f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 107, Length = 3.57f},
new(){Key = "walk", Frame = 20, Length = 0.67f},
}
},
{
5142, new ClipInfo[]
{
new(){Key = "attack1", Frame = 36, Length = 1.20f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 72, Length = 2.40f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5143, new ClipInfo[]
{
new(){Key = "attack1", Frame = 29, Length = 0.97f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 16, Length = 0.53f},
new(){Key = "hit1", Frame = 4, Length = 0.13f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 30, Length = 1.00f},
new(){Key = "run", Frame = 18, Length = 0.60f},
new(){Key = "show", Frame = 64, Length = 2.13f},
new(){Key = "walk", Frame = 28, Length = 0.93f},
}
},
{
5144, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 4, Length = 0.13f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 35, Length = 1.17f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 95, Length = 3.17f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5145, new ClipInfo[]
{
new(){Key = "attack1", Frame = 36, Length = 1.20f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 52, Length = 1.73f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 124, Length = 4.13f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5146, new ClipInfo[]
{
new(){Key = "attack1", Frame = 35, Length = 1.17f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 6, Length = 0.20f},
new(){Key = "hit2", Frame = 6, Length = 0.20f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 32, Length = 1.07f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 77, Length = 2.57f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
new(){Key = "Take 001", Frame = 40, Length = 1.33f},
}
},
{
5147, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 15, Length = 0.50f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 55, Length = 1.83f},
new(){Key = "show2", Frame = 76, Length = 2.53f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5148, new ClipInfo[]
{
new(){Key = "attack1", Frame = 30, Length = 1.00f},
new(){Key = "defend", Frame = 11, Length = 0.37f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 38, Length = 1.27f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 85, Length = 2.83f},
new(){Key = "walk", Frame = 32, Length = 1.07f},
}
},
{
5149, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
}
},
{
60100, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
}
},
{
60101, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
}
},
{
60102, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
}
},
{
60103, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
}
},
{
60104, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
}
},
{
60105, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 40, Length = 1.33f},
}
},
{
60200, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
}
},
{
60201, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
}
},
{
60202, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
}
},
{
60203, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 40, Length = 1.33f},
}
},
{
60204, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 24, Length = 0.80f},
new(){Key = "run", Frame = 24, Length = 0.80f},
}
},
{
60205, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 24, Length = 0.80f},
}
},
{
6101, new ClipInfo[]
{
new(){Key = "attack1", Frame = 38, Length = 1.27f},
new(){Key = "attack2", Frame = 57, Length = 1.90f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 77, Length = 2.57f},
new(){Key = "show", Frame = 120, Length = 4.00f},
new(){Key = "show2", Frame = 96, Length = 3.20f},
}
},
{
6102, new ClipInfo[]
{
new(){Key = "attack1", Frame = 33, Length = 1.10f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 6, Length = 0.20f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 50, Length = 1.67f},
new(){Key = "show2", Frame = 90, Length = 3.00f},
new(){Key = "Take 001", Frame = 15, Length = 0.50f},
}
},
{
6103, new ClipInfo[]
{
new(){Key = "attack1", Frame = 36, Length = 1.20f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 40, Length = 1.33f},
new(){Key = "run", Frame = 18, Length = 0.60f},
new(){Key = "show", Frame = 70, Length = 2.33f},
new(){Key = "show2", Frame = 111, Length = 3.70f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
6104, new ClipInfo[]
{
new(){Key = "attack1", Frame = 40, Length = 1.33f},
new(){Key = "attack2", Frame = 62, Length = 2.07f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 57, Length = 1.90f},
new(){Key = "run", Frame = 24, Length = 0.80f},
new(){Key = "show", Frame = 90, Length = 3.00f},
new(){Key = "show2", Frame = 148, Length = 4.93f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
6105, new ClipInfo[]
{
new(){Key = "attack1", Frame = 39, Length = 1.30f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 20, Length = 0.67f},
new(){Key = "hit1", Frame = 6, Length = 0.20f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 45, Length = 1.50f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 110, Length = 3.67f},
new(){Key = "show2", Frame = 191, Length = 6.37f},
new(){Key = "walk", Frame = 30, Length = 1.00f},
}
},
{
6106, new ClipInfo[]
{
new(){Key = "attack1", Frame = 45, Length = 1.50f},
new(){Key = "defend", Frame = 1, Length = 0.03f},
new(){Key = "die", Frame = 18, Length = 0.60f},
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 5, Length = 0.17f},
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "idleWar", Frame = 30, Length = 1.00f},
new(){Key = "magic", Frame = 32, Length = 1.07f},
new(){Key = "run", Frame = 20, Length = 0.67f},
new(){Key = "show", Frame = 120, Length = 4.00f},
}
},
{
8214, new ClipInfo[]
{
new(){Key = "hit1", Frame = 5, Length = 0.17f},
new(){Key = "hit2", Frame = 4, Length = 0.13f},
new(){Key = "idleWar", Frame = 40, Length = 1.33f},
new(){Key = "magic", Frame = 35, Length = 1.17f},
}
},
{
8215, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 8, Length = 0.27f},
new(){Key = "run", Frame = 8, Length = 0.27f},
new(){Key = "show", Frame = 8, Length = 0.27f},
}
},
{
8216, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 1, Length = 0.03f},
}
},
{
8217, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
}
},
{
8218, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
}
},
{
8219, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 1, Length = 0.03f},
}
},
{
8220, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
}
},
{
8221, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
}
},
{
8222, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
}
},
{
8223, new ClipInfo[]
{
new(){Key = "model8223_ani", Frame = 120, Length = 2.00f},
}
},
{
8224, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 46, Length = 1.53f},
}
},
{
8225, new ClipInfo[]
{
new(){Key = "idelCity", Frame = 30, Length = 1.00f},
new(){Key = "shwo", Frame = 30, Length = 1.00f},
}
},
{
8226, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 1, Length = 0.03f},
new(){Key = "show", Frame = 20, Length = 0.67f},
}
},
{
8227, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
new(){Key = "show", Frame = 20, Length = 0.67f},
}
},
{
8228, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 1, Length = 0.03f},
new(){Key = "run", Frame = 1, Length = 0.03f},
new(){Key = "show", Frame = 20, Length = 0.67f},
}
},
{
8229, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 1, Length = 0.03f},
new(){Key = "run", Frame = 1, Length = 0.03f},
new(){Key = "show", Frame = 20, Length = 0.67f},
}
},
{
8230, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 1, Length = 0.03f},
new(){Key = "run", Frame = 1, Length = 0.03f},
new(){Key = "show", Frame = 20, Length = 0.67f},
}
},
{
8231, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 1, Length = 0.03f},
new(){Key = "run", Frame = 1, Length = 0.03f},
new(){Key = "show", Frame = 20, Length = 0.67f},
}
},
{
8232, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 1, Length = 0.03f},
}
},
{
8233, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
}
},
{
8234, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
}
},
{
8235, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 30, Length = 1.00f},
}
},
{
8236, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 1, Length = 0.03f},
new(){Key = "run", Frame = 1, Length = 0.03f},
new(){Key = "show", Frame = 10, Length = 0.33f},
}
},
{
8237, new ClipInfo[]
{
}
},
{
8401, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 40, Length = 1.33f},
}
},
{
8402, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 40, Length = 1.33f},
}
},
{
8403, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 40, Length = 1.33f},
}
},
{
8404, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 40, Length = 1.33f},
}
},
{
8405, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 40, Length = 1.33f},
}
},
{
8406, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 40, Length = 1.33f},
}
},
{
8407, new ClipInfo[]
{
new(){Key = "idleCity", Frame = 40, Length = 1.33f},
new(){Key = "show", Frame = 40, Length = 1.33f},
}
},

        };
    
	
		/// <summary>
		///  获得动画片段信息
		/// </summary>
		/// <param name="id"></param>
		/// <param name="clipName"></param>
		/// <param name="clipInfo"></param>
		/// <returns></returns>
		public static bool TryGetAnimClipInfo(int id, string clipName, out ClipInfo clipInfo)
		{
			var result = false;
			clipInfo = default;
			if (Data.TryGetValue(id, out var clipInfos))
			{
				foreach (var info in clipInfos)
				{
					if (info.Key != clipName) continue;
					clipInfo = info;
					result = true;
					break;
				}
			}
			
			return result;
		}
	}
}
