using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace HT.Framework
{
    [CustomEditor(typeof(VariableBehaviour))]
    public sealed class VariableBehaviourEditor : HTFEditor<VariableBehaviour>
    {
        private bool _isCompiling;
        private string _className;
        protected override bool IsEnableRuntimeData => false;

        protected override void OnDefaultEnable()
        {
            base.OnDefaultEnable();
            _className = "";
            _isCompiling = false;
            EditorApplication.update += OnEditorUpdate;
        }

        private void OnEditorUpdate()
        {
            if (EditorApplication.isCompiling)
            {
                _isCompiling = true;
            }
            else if (_isCompiling)
            {
                _isCompiling = false;

                if (!string.IsNullOrEmpty(_className))
                {
                    Type type = Type.GetType(_className);
                    var c = Target.gameObject.AddComponent(type);
                }
                // 编译完成后的操作
                // AddGeneratedComponent();
                EditorApplication.update -= OnEditorUpdate;
            }
        }

        protected override void OnDefaultDisable()
        {
            base.OnDefaultDisable();
            // EditorApplication.update -= OnEditorUpdate;
        }


        protected override void OnInspectorDefaultGUI()
        {
            
            PropertyField(nameof(Target.Container), "Container");
            
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Generate Script", EditorGlobalTools.Styles.LargeButton))
            {
                // EditorGlobalTools.CreateScriptFormTemplate("","","");
                // EditorPrefs.SetString(EditorPrefsTable.Script_HotfixProcedure_Folder, "/Hotfix");
                
                EditorPrefs.SetString(EditorPrefsTable.Script_HotfixProcedure_Folder, "/Hotfix");
                _className = EditorGlobalTools.CreateScriptFormTemplate(EditorPrefsTable.Script_HotfixProcedure_Folder, "VariableBehaviour", "VariableBehaviourTemplate",Handler);

                // Log.Info("脚本名称:" + className);
                // if (!string.IsNullOrEmpty(className))
                // {
                //     Type type = Type.GetType(className);
                //     var c = Target.gameObject.AddComponent(type);
                // }
            }
            GUI.backgroundColor = Color.white;
        }

        private string Handler(string arg)
        {
            var sb = new StringBuilder();
            sb.Append("\r\n");
            foreach (var variable in Target.Container.Variables)
            {
                sb.Append($" public {variable.ValueType} {variable.Name};");
                sb.Append("\r\n");
            }
            //替换参数
            Regex r = new Regex("(?<=(#region parameter))[.\\s\\S]*?(?=(#endregion))");//A为起始字符 B为结束字符
            arg = r.Replace(arg, sb.ToString(), 1); //*为替换字符串  1为替换最大次数
            
            //赋值
            sb.Clear();
            sb.Append("\r\n");
            foreach (var variable in Target.Container.Variables)
            {
                sb.Append($"            {variable.Name} = Container.Get<{variable.ValueType}>(\"{variable.Name}\");");
                sb.Append("\r\n");
            }
          
            r = new Regex("(?<=(#region Awake))[.\\s\\S]*?(?=(#endregion))");//A为起始字符 B为结束字符
            arg = r.Replace(arg, sb.ToString(), 1); //*为替换字符串  1为替换最大次数
            return arg;
        }
    }
}
