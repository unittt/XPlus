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
        
        public static List<MagicData> MagicDatas = new();

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
                if (!Path.GetExtension(file)
                        .Equals(EditorMagicDefine.SUFFIX, System.StringComparison.OrdinalIgnoreCase)) continue;
                var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(file);

                if (textAsset == null) continue;

                var magicData = MagicData.Deserialize(textAsset.bytes);
                MagicDatas.Add(magicData);
            }

            Main.m_UI.OpenUI<UIEditorMagic>();
        }
        
        //增加
        //删除
    }
}