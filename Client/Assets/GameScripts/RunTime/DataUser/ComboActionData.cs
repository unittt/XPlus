using System.Collections.Generic;

namespace GameScripts.RunTime.DataUser
{

    /// <summary>
    /// 组合动作信息
    /// </summary>
    public struct ComboActionInfo
    {
        public string action;
        public int start_frame;
        public int hit_frame;
        public int end_frame;
        public int speed;
    }

    /// <summary>
    /// 组合动作数据
    /// </summary>
    public static class ComboActionData
    {

        public static Dictionary<int, Dictionary<string, ComboActionInfo[]>> Data =
            new()
            {
                {
                    101, new Dictionary<string, ComboActionInfo[]>()
                    {
                        {
                            "attack1001", new ComboActionInfo[]
                            {
                                new() { action = "attack3", end_frame = 7, hit_frame = 10, start_frame = 0 },
                                new() { action = "attack3", end_frame = 11, hit_frame = 10, start_frame = 7 },
                                new() { action = "attack3", end_frame = 11, hit_frame = 10, start_frame = 7 },
                                new() { action = "attack3", end_frame = 29, hit_frame = 0, start_frame = 11 }
                            }
                        },
                        {
                            "attack1002", new ComboActionInfo[]
                            {
                                new() { action = "attack2", end_frame = 20, hit_frame = 6, start_frame = 4 },
                                new() { action = "attack3", end_frame = 30, hit_frame = 20, start_frame = 8 },
                            }
                        }
                    }
                },
                {
                    102, new Dictionary<string, ComboActionInfo[]>()
                    {
                        {
                            "attack1002", new ComboActionInfo[]
                            {
                                new() { action = "attack1", end_frame = 30, hit_frame = 10, start_frame = 0 },
                                new() { action = "attack2", end_frame = 30, hit_frame = 20, start_frame = 10 },
                            }
                        }
                    }
                },
                {
                    1110, new Dictionary<string, ComboActionInfo[]>()
                    {
                        {
                            "1101", new ComboActionInfo[]
                            {
                                new()
                                {
                                    action = "attack2", end_frame = 13, hit_frame = 20, speed = 1, start_frame = 8
                                },
                                new()
                                {
                                    action = "attack3", end_frame = 17, hit_frame = 20, speed = 1, start_frame = 6
                                },
                                new() { action = "magic", end_frame = 29, hit_frame = 20, speed = 1, start_frame = 17 }
                            }
                        },
                        {
                            "1102", new ComboActionInfo[]
                            {
                                new()
                                {
                                    action = "attack1", end_frame = 15, hit_frame = 14, speed = 1, start_frame = 0
                                },
                                new() { action = "attack2", end_frame = 20, hit_frame = 0, speed = 1, start_frame = 8 },
                                new() { action = "magic", end_frame = 35, hit_frame = 0, speed = 1, start_frame = 7 }
                            }
                        },
                        {
                            "11021", new ComboActionInfo[]
                            {
                                new()
                                {
                                    action = "attack1", end_frame = 15, hit_frame = 14, speed = 1, start_frame = 0
                                },
                                new() { action = "attack2", end_frame = 20, hit_frame = 0, speed = 1, start_frame = 8 },
                                new() { action = "magic", end_frame = 35, hit_frame = 0, speed = 1, start_frame = 7 }
                            }
                        },
                        {
                            "1103", new ComboActionInfo[]
                            {
                                new()
                                {
                                    action = "attack2", end_frame = 12, hit_frame = 20, speed = 1, start_frame = 8
                                },
                                new() { action = "attack3", end_frame = 11, hit_frame = 3, speed = 1, start_frame = 8 },
                                new() { action = "attack2", end_frame = 12, hit_frame = 3, speed = 1, start_frame = 8 },
                                new() { action = "attack3", end_frame = 25, hit_frame = 3, speed = 1, start_frame = 8 }
                            }
                        },
                        {
                            "1104", new ComboActionInfo[]
                            {
                                new() { action = "attack3", end_frame = 12, hit_frame = 3, speed = 1, start_frame = 8 },
                                new()
                                {
                                    action = "attack1", end_frame = 21, hit_frame = 10, speed = 1, start_frame = 9
                                },
                            }
                        }
                    }
                },
                {
                    5115, new Dictionary<string, ComboActionInfo[]>()
                    {
                        {
                            "5115", new ComboActionInfo[]
                            {
                                new() { action = "attack1", end_frame = 16, hit_frame = 16, speed = 1, start_frame = 0 }
                            }
                        }
                    }
                },
            };


        public static  ComboActionInfo[] GetComboActionInfos(int key, string actName)
        {
            if (Data.TryGetValue(key, out var actionInfosMap) && actionInfosMap.TryGetValue(actName, out var cActionInfos))
            {
                return cActionInfos;
            }
            return null;
        }
    }
}

