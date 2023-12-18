using System;
using System.Collections.Generic;
using GameScripts.RunTime.Magic.Command.Handler;
using HT.Framework;

namespace GameScripts.RunTime.Magic
{
    public class MagicManager: SingletonBase<MagicManager>
    {

        //指令数据类型 - 指令处理类型
        private Dictionary<Type, Type> _d2HInstances;

        public MagicManager()
        {
            var types = ReflectionToolkit.GetTypesInRunTimeAssemblies(type => type.IsSubclassOf(typeof(CmdHandler)) && !type.IsAbstract);
            foreach (var type in types)
            {
                var baseType = type.BaseType;
                if (!baseType.IsGenericType) continue;
                var argumentType = baseType.GetGenericArguments()[0];
                _d2HInstances.Add(argumentType, type);
            }
        }
        
        public void ResetCalcPosObject()
        {
           
        }
    }
}