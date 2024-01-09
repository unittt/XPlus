using HT.Framework;
using System;
using System.Collections.Generic;

namespace GameScripts.RunTime.Utility.Selector
{
    
    public static class SelectorManager
    {
        private static Dictionary<Type, SelectorHandler> _handlers = new();
        
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
        /// <param name="terms"></param>
        /// <typeparam name="T"></typeparam>
        public static void GetTerms<T>(List<string> terms) where  T : SelectorHandler
        {
            GetHandler(typeof(T)).GetTerms(terms);
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

        /// <summary>
        /// 获取条目对应值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        public static object GetTermValue(Type type,string term)
        {
            return GetHandler(type).GetTermValue(term);
        }
    }
}