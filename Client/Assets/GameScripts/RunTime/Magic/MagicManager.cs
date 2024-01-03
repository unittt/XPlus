using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using GameScripts.RunTime.Magic.Command;
using GameScripts.RunTime.Magic.Command.Handler;
using GameScripts.RunTime.War;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Magic
{
    public class MagicManager: SingletonBase<MagicManager>
    {

        internal static int CurUnitIdx;
        private StringBuilder _sb;
        //指令数据类型 - 指令处理类型
        private Dictionary<Type, Type> _d2HInstances;
        private Dictionary<string, MagicData> _magicDataInstance;
        private List<MagicUnit> _units;

        public MagicManager()
        {
            
            _d2HInstances = new Dictionary<Type, Type>();
            _magicDataInstance = new Dictionary<string, MagicData>();
            _units = new List<MagicUnit>();
            
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

        public async UniTaskVoid NewMagicUnit(int magicID, int magicIndex, Warrior atkObj, List<Warrior> refVicObjs, bool isPursued)
        {
            var shape = 1110;
            var oWarriorName = atkObj.GetName();
            // Log.Info(string.Format("<color=#F75000> >>> .%s | %s </color>", "NewMagicUnit", "加载技能"), string.Format("Name：%s | ID：%s | Shape：%s | Index：%s", oWarriorName, magicID, shape, magicIndex));
            
            // var dFileData = self:GetFileData(id, shape, index);

            var magicData = await GetMagicData(magicID, shape, magicIndex);
            var magicUnit = Main.m_ReferencePool.Spawn<MagicUnit>();
            magicUnit.Fill(magicID, shape,magicIndex,isPursued,atkObj,refVicObjs,magicData.Commands);
        }

        public Type GetHandlerType(CommandData commandData)
        {
            return _d2HInstances[commandData.GetType()];
        }
        
        /// <summary>
        /// 获取法术文件
        /// </summary>
        /// <param name="id">法术编号</param>
        /// <param name="shape">模型</param>
        /// <param name="index">索引(1开始)</param>
        /// <returns></returns>
        private async UniTask<MagicData> GetMagicData(int id, int shape, int index)
        {
            var key = GetKey(id, shape, index);
            if (_magicDataInstance.TryGetValue(key, out var magicData)) return magicData;
            var textAsset = await Main.m_Resource.LoadAsset<TextAsset>(key);
            magicData = MagicData.Deserialize(textAsset.bytes);
            _magicDataInstance.TryAdd(key, magicData);
            return magicData;
        }

        private string GetKey(int id, int shape, int index)
        {
            _sb.Clear();
            _sb.Append("magic_");
            _sb.Append(id);
            if (shape > 0)
            {
                _sb.Append($"_{shape.ToString()}");
            }
            if (index > 0)
            {
                _sb.Append($"_{index.ToString()}");
            }
            return _sb.ToString();
        }


    }
}