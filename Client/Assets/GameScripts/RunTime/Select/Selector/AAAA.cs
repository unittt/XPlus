using System;
using System.Collections.Generic;
using GameScripts.RunTime.Select.Attributes;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Select.Selector
{
    public class AAAA : MonoBehaviour
    {
        public ExecutorType eType;

        [Argument("测试")]
        public List<int> B;

        private void Start()
        {
            var xx = new EnumSelectorHandler<MoveDirection>();
            List<string> strList = new List<string>();
            xx.GetElementCollection(strList);

          
            foreach (var str in strList)
            {
                Log.Info(str);
            }

            Type type = typeof(AAAA);
            var xxx = type.GetField("eType");
            xx.SetTarget(this,xxx);
            xx.Select("友军(全部)");
            
        //     var c = typeof(EnumSelectorHandler<MoveDirection>);
        //     var handler = Activator.CreateInstance(c).Cast<SelectorHandler>();
        //     handler.GetElementCollection(strList);
        //     
        //     foreach (var str in strList)
        //     {
        //         Log.Info(str);
        //     }
        }
    }
}