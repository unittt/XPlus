using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GameScript.RunTime.Procedure;
using GameScripts.RunTime.Magic;
using GameScripts.RunTime.Magic.Command;
using HT.Framework;
using UnityEditor;
using UnityEngine;

namespace GameScripts.RunTime.EditorMagic
{

    [InitializeOnLoad]
    public static class EditorMagicManager
    {

        private static string EditorMagicFileName;
        
        public static Dictionary<string,byte[]> MagicDatas = new();
        public static event Action<string> OnCreateMagic;
        public static event Action<string> OnDeleteMagic;
        
        /// <summary>
        /// 编辑的法术改变
        /// </summary>
        public static event Action<MagicData,string> OnMagicChanged;
        /// <summary>
        /// 编辑的法术指令发生改变
        /// </summary>
        public static event Action<MagicData> OnMagicCmdChanged;

        //当前编辑的法术数据
        private static MagicData _curMagicData;


        public static Dictionary<Type, CommandAttribute> T2AInstance;

        static EditorMagicManager()
        {
            //添加自定义程序集到运行时程序域
            ReflectionToolkit.AddRunTimeAssembly("EditorMagic");
            
            
            //1.查找所有指令
            var types = ReflectionToolkit.GetTypesInRunTimeAssemblies(type => type.IsSubclassOf(typeof(CommandBase)) && !type.IsAbstract);

            T2AInstance = new Dictionary<Type, CommandAttribute>();
            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<CommandAttribute>();
                T2AInstance.Add(type, attribute);
            }
        }

        [RuntimeInitializeOnLoadMethod]
        public static void RuntimeInitialize()
        {
            Main.m_Procedure.AnyProcedureSwitchEvent += OnProcedureSwitch;
        }

        private static void OnProcedureSwitch(ProcedureBase arg1, ProcedureBase arg2)
        {
            if (arg2.GetType() == typeof(ProcedureEditor))
            {
                Init();
            }
        }

        private static void Init()
        {
            //1.加载目录下所有的数据
            var files = Directory.GetFiles(EditorMagicDefine.PATH);
            foreach (var file in files)
            {
                // 检查文件扩展名
                if (!Path.GetExtension(file).Equals(EditorMagicDefine.SUFFIX, System.StringComparison.OrdinalIgnoreCase)) continue;
                
                var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(file);
                if (textAsset == null) continue;

                // var magicData = MagicData.Deserialize(textAsset.bytes);
                // MagicDatas.Add(textAsset.name, magicData);
                MagicDatas.Add(textAsset.name, textAsset.bytes);
            }

            Main.m_UI.OpenUI<UIEditorMagic>();
        }

        /// <summary>
        /// 编辑法术
        /// </summary>
        /// <param name="fileName"></param>
        public static void EditorMagic(string fileName)
        {
            EditorMagicFileName = fileName;
            
            _curMagicData = null;
            if (MagicDatas.TryGetValue(fileName, out var bytes))
            {
                _curMagicData = MagicData.Deserialize(bytes);
            }
            OnMagicChanged?.Invoke(_curMagicData,fileName);
        }


        /// <summary>
        /// 创建一个法术文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool CreateMagicFile(string fileName)
        {
            if (MagicDatas.ContainsKey(fileName))
            {
                return false;
            }

            var magicData = new MagicData();
            var bytes = magicData.SerializeCommands();
            MagicDatas.Add(fileName, bytes);
            
            WriteMagicFile(fileName,bytes);
            
            OnCreateMagic?.Invoke(fileName);
            return true;
        }

        private static void WriteMagicFile(string fileName, byte[] bytes)
        {
            //写入数据
            var path = GetMagicFilePath(fileName);
            File.WriteAllBytes(path,bytes);
            // 刷新Asset数据库，以便Unity编辑器能够检测到新文件
            AssetDatabase.Refresh();
        }
        
        
        /// <summary>
        /// 删除法术文件
        /// </summary>
        public static void DeleteMagicFile(string fileName)
        {
            if (!MagicDatas.ContainsKey(fileName)) return;
            MagicDatas.Remove(fileName);
            AssetDatabase.DeleteAsset(GetMagicFilePath(fileName));
            OnDeleteMagic?.Invoke(fileName);

            //如果删除的是正在编辑的技能
            if (EditorMagicFileName == fileName)
            {
                EditorMagic(string.Empty);
            }
        }

        /// <summary>
        /// 保存法术文件
        /// </summary>
        public static void SaveMagicData(MagicData magicData)
        {
            if (!MagicDatas.ContainsKey(EditorMagicFileName)) return;
            var bytes =  magicData.SerializeCommands();
            MagicDatas[EditorMagicFileName] = bytes;
            WriteMagicFile(EditorMagicFileName, bytes);
        }


        #region 指令
        /// <summary>
        /// 添加指令
        /// </summary>
        /// <param name="cmd"></param>
        public static void AddCmd(CommandBase cmd)
        {
            _curMagicData.Commands.Add(cmd);
            // _curMagicData.Commands.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
            CommandsSort(_curMagicData);
            OnMagicCmdChanged?.Invoke(_curMagicData);
        }
        
        /// <summary>
        /// 替换指令
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cmd"></param>
        public static void ReplaceCmd(int index, CommandBase cmd)
        {
            _curMagicData.Commands[index] = cmd;
            // _curMagicData.Commands.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
            CommandsSort(_curMagicData);
            OnMagicCmdChanged?.Invoke(_curMagicData);
        }

        private static void CommandsSort(MagicData magicData)
        {
            magicData.Commands.OrderBy(c => c.StartTime).ThenBy(c => T2AInstance[c.GetType()].Sort).ToList();
        }
        
        /// <summary>
        /// 移除指令
        /// </summary>
        /// <param name="index"></param>
        public static void RemoveCmd(int index)
        {
            _curMagicData.Commands.RemoveAt(index);
            OnMagicCmdChanged?.Invoke(_curMagicData);
        }
        #endregion
       
        
        private static string GetMagicFilePath(string fileName)
        {
            return $"{EditorMagicDefine.PATH}/{fileName}{EditorMagicDefine.SUFFIX}";
        }
    }
}