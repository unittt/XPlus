using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Utility.Selector
{
    public class SelectorHandler_WarSound:SelectorHandler
    {
        
        public SelectorHandler_WarSound()
        {
            Main.m_Resource.LoadAssetByTag<AudioClip>("WarSound").ContinueWith(OnLoadDone).Forget();
        }

        private void OnLoadDone(Dictionary<string, AudioClip> obj)
        {
            foreach (var clipName in obj.Keys)
            {
                Terms.Add(clipName);
            }
        }
        
        internal override object GetTermValue(string value)
        {
            return value;
        }
    }
}