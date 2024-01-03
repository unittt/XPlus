using HT.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameScripts.RunTime.Utility.Selector
{
    
   
    public static class SelectorManager
    {
        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            Main.m_Resource.InitializationCompleted += OnResourceInitialized;
        }


        private static Dictionary<Type, SelectorHandler> _handlers;

        private static void OnResourceInitialized(bool arg)
        {
            _handlers = new Dictionary<Type, SelectorHandler>();
            var types = ReflectionToolkit.GetTypesInRunTimeAssemblies(type => type.IsSubclassOf(typeof(SelectorHandler)) && !type.IsAbstract);
            foreach (var type in types)
            {
                //如果不为泛型将其实例化
                if (type.IsGenericType) continue;
                var handler = Activator.CreateInstance(type).Cast<SelectorHandler>();
                _handlers.Add(type, handler);
            }
        }
        
        private static SelectorHandler GetHandler(Type type)
        {
            if (_handlers.ContainsKey(type))
            {
                return _handlers[type];
            }
            
            var handler = Activator.CreateInstance(type).Cast<SelectorHandler>();
            _handlers.Add(type, handler);
            return handler;
        }

        /// <summary>
        /// 获取条目
        /// </summary>
        /// <param name="type"></param>
        /// <param name="terms"></param>
        public static void GetTerms(Type type, List<string> terms)
        {
            GetHandler(type).GetTerms(terms);
        }

        public static object GetTermValue(Type type,string term)
        {
            return GetHandler(type).GetTermValue(term);
        }
    }
}