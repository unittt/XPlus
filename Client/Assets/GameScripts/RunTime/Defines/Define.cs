using System.Collections.Generic;

namespace GameScripts.RunTime.Define
{
    public static class GameDefines
    {
        public static class UniqueID
        {
            public const int QueryBack = 1;
        }

        public static class Net
        {
            public const int Event_Sockect = 1;
        }

        public static class Gm
        {
            public static class Event
            {
                public const int RefreshTime = 1;
                public const int RefreshLastInfo = 2;
                public const int RefreshGmHelpMsg = 3;
                public const int ShowItemId = 4;
            }
        }

        public static class Time
        {
            public static class Event
            {
                public const int NextDay = 0;
            }
        }

        public static class Layer
        {
            public const int MapTerrain = 8;
            public const int MapWalker = 9;
            public const int War = 10;
            public const int ModelTexture = 11;
            public const int Hide = 12;
            public const int RoleCreate = 14;
            public const int HudLayer = 15;
            public const int Magic = 16;
        }

        public static class RoleColor
        {
            public const int Protagonist = 0; // 主角
            public const int Player = 1; // 场景玩家
            public const int SceneNPC = 2; // 场景NPC
            public const int DynamicNPC = 3; // 动态NPC
            public const int WarFriend = 4; // 战斗友
            // ... 这里会继续添加更多的常量
        }

        
        public static class Model
        {
            public const int Defalut_Figure = 1110;
            public const int Defalut_Shape = 1110;

            
            public static readonly Dictionary<int, WeaponInfo> WEAPON = new Dictionary<int, WeaponInfo>
            {
                {1110, new WeaponInfo(new Dictionary<int, string>{{1, "right_hand"}})},
                {1120, new WeaponInfo(new Dictionary<int, string>{{1, "right_hand"}})},
                {1210, new WeaponInfo(new Dictionary<int, string>{{1, "right_hand"}})},
                {1220, new WeaponInfo(new Dictionary<int, string>{{1, "right_hand"}})},
                {1310, new WeaponInfo(new Dictionary<int, string>{{1, "right_hand"}})},
                {1320, new WeaponInfo(new Dictionary<int, string>{{1, "right_hand"}})},
            };

            // Nested class to represent weapon information
            public class WeaponInfo
            {
                public Dictionary<int, string> Mounts { get; private set; }

                public WeaponInfo(Dictionary<int, string> mounts)
                {
                    Mounts = mounts;
                }
            }
        }
    }
}