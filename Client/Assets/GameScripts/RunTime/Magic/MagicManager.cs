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
    [CustomModule("施法模块", true)]
    public class MagicManager : CustomModuleBase
    {

        internal static int CurUnitIdx;

        private StringBuilder _sb;

        //指令数据类型 - 指令处理类型
        private Dictionary<Type, Type> _d2HInstances;
        private Dictionary<string, MagicData> _magicDataInstance;
        private List<MagicUnit> _units;
        private List<MagicUnit> _unitsQueue;



        private Transform _calcPosTransform;


        public static MagicManager Current { get; private set; }


        public override void OnInit()
        {
         
            base.OnInit();
            _sb = new StringBuilder();
            _d2HInstances = new Dictionary<Type, Type>();
            _magicDataInstance = new Dictionary<string, MagicData>();
            _units = new List<MagicUnit>();
            _unitsQueue = new List<MagicUnit>();

            var types = ReflectionToolkit.GetTypesInRunTimeAssemblies(type =>
                type.IsSubclassOf(typeof(CmdHandler)) && !type.IsAbstract);
            foreach (var type in types)
            {
                var baseType = type.BaseType;
                if (!baseType.IsGenericType) continue;
                var argumentType = baseType.GetGenericArguments()[0];
                _d2HInstances.Add(argumentType, type);
            }

            _calcPosTransform = new GameObject("CalcPosObject").transform;
            Current = this;
            IsRunning = true;
        }


        public Transform GetCalcPosTransform(bool isReset = true)
        {
            if (!isReset) return _calcPosTransform;
            _calcPosTransform.SetParent(null);
            _calcPosTransform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            return _calcPosTransform;
        }


        public async UniTaskVoid NewMagicUnit(int magicID, int magicIndex, Warrior atkObj, List<Warrior> refVicObjs,
            bool isPursued,Action completedCallBack)
        {
            var shape = 1110;
            // var oWarriorName = atkObj.GetName();
            // Log.Info(string.Format("<color=#F75000> >>> .%s | %s </color>", "NewMagicUnit", "加载技能"), string.Format("Name：%s | ID：%s | Shape：%s | Index：%s", oWarriorName, magicID, shape, magicIndex));

            // var dFileData = self:GetFileData(id, shape, index);

            var magicData = await GetMagicData(magicID, shape, magicIndex);
            var magicUnit = Main.m_ReferencePool.Spawn<MagicUnit>();
            magicUnit.Fill(magicID, shape, magicIndex, isPursued, atkObj, refVicObjs, magicData.Commands,completedCallBack);
            _units.Add(magicUnit);
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


        public override void OnUpdate()
        {
            base.OnUpdate();

            _unitsQueue.Clear();
            if (_units.Count <= 0) return;
            _unitsQueue.AddRange(_units);
            foreach (var magicUnit in _unitsQueue)
            {
                magicUnit.OnUpdate();
                if (magicUnit.IsFinish)
                {
                    Log.Info("法术完成----------------");
                    magicUnit.OnCompleted();
                    RemoveUnit(magicUnit);
                }
            }
        }

        private void RemoveUnit(MagicUnit magicUnit)
        {
            if (!_units.Contains(magicUnit)) return;
            _units.Remove(magicUnit);
            Main.m_ReferencePool.Despawn(magicUnit);
        }
        
        private void RemoveAllUnit()
        {
            
        }
    }
}