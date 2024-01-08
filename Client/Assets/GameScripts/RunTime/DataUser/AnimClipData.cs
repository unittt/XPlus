using System.Collections.Generic;

namespace GameScripts.RunTime.DataUser
{
    public struct ActionData
    {
        public string key;
        public int frame;
        public float length;
    }
    
    public static class AnimClipData
    {
        private static Dictionary<int, ActionData[]> DATA = new Dictionary<int, ActionData[]>()
        {
            {1110,  new ActionData[]
                {
                    new ActionData(){key = "attack1", frame = 20, length =  0.66f},
                    new ActionData(){key = "attack2", frame = 24, length =  0.66f},
                    new ActionData(){key = "attack3", frame = 35, length =  0.66f},
                    new ActionData(){key = "attack4", frame = 29, length =  0.66f},
                    new ActionData(){key = "attack5", frame = 41, length =  0.66f},
                    new ActionData(){key = "attack6", frame = 32, length =  0.66f},
                    new ActionData(){key = "attack7", frame = 32, length =  0.66f},
                    new ActionData(){key = "attack8", frame = 43, length =  0.66f},
                    new ActionData(){key = "attack9", frame = 26, length =  0.66f},
                } 
            }
        };
    }
}