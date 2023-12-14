using System;
using System.Collections.Generic;
using System.IO;
using GameScript.RunTime.Procedure;
using GameScripts.RunTime.Magic;
using HT.Framework;
using UnityEditor;
using UnityEngine;

namespace GameScripts.RunTime.EditorMagic
{

    [InitializeOnLoad]
    public static class EditorMagicManager
    {
        
        public static Dictionary<string,MagicData> MagicDatas = new();

        public static event Action<string> OnCreateMagic;
        public static event Action<string> OnDeleteMagic;
        
        static EditorMagicManager()
        {
            //添加自定义程序集到运行时程序域
            ReflectionToolkit.AddRunTimeAssembly("EditorMagic");
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

                var magicData = MagicData.Deserialize(textAsset.bytes);
                MagicDatas.Add(textAsset.name, magicData);
            }

            Main.m_UI.OpenUI<UIEditorMagic>();
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
            MagicDatas.Add(fileName, magicData);

            var path = GetMagicFilePath(fileName);
            var bytes = magicData.SerializeCommands();
            //写入数据
            File.WriteAllBytes(path,bytes);
            // 刷新Asset数据库，以便Unity编辑器能够检测到新文件
            // AssetDatabase.Refresh();
            
            OnCreateMagic?.Invoke(fileName);
            return true;
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
        }

        /// <summary>
        /// 替换法术文件
        /// </summary>
        public static void ReplaceMagicFile(string key ,MagicData magicData)
        {
            if (MagicDatas.ContainsKey(key))
            {
                
            }
        }

        private static string GetMagicFilePath(string fileName)
        {
            return $"{EditorMagicDefine.PATH}/{fileName}{EditorMagicDefine.SUFFIX}";
        }
    }
}