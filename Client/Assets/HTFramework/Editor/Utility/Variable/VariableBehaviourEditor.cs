using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace HT.Framework
{
    [CustomEditor(typeof(VariableBehaviour))]
    public sealed class VariableBehaviourEditor : HTFEditor<VariableBehaviour>
    {
        protected override bool IsEnableRuntimeData => false;

        protected override void OnDefaultEnable()
        {
            base.OnDefaultEnable();
        }

        [DidReloadScripts]
        private static void BindScriptsToObj()
        {
            var className = EditorPrefs.GetString("VariableScriptName","");
            if (string.IsNullOrEmpty(className)) return;
            var type = ReflectionToolkit.GetTypeInRunTimeAssemblies(className);
            
            // HierarchyProperty property = new HierarchyProperty(assetPath);

            var variableBehaviours = FindObjectsByType<VariableBehaviour>(FindObjectsSortMode.None);
            var instanceID = EditorPrefs.GetInt("VariableObjInstanceID",0);
            VariableBehaviour vbbb = null;
            foreach (var vb in variableBehaviours)
            {
                if (vb.gameObject.GetInstanceID() != instanceID) continue;
                vbbb = vb;
                break;
            }

            if (vbbb != null)
            {
                vbbb.gameObject.AddComponent(type);
            }
            
            // EditorApplication.HierarchyWindowItemCallback.
            // Selection.activeInstanceID = property.GetInstanceIDIfImported();
     
            
            // Selection.activeTransform;
            // if (_obj == null)
            // {
            //     Log.Info("对象为空");
            // }
            // var c = _obj.AddComponent(type);
        }
        
        protected override void OnInspectorDefaultGUI()
        {
            
            PropertyField(nameof(Target.Container), "Container");
            
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Generate Script", EditorGlobalTools.Styles.LargeButton))
            {
                EditorPrefs.SetString(EditorPrefsTable.Script_HotfixProcedure_Folder, "/Hotfix");
                EditorPrefs.SetInt("VariableObjInstanceID",Target.gameObject.GetInstanceID());
                var className = EditorGlobalTools.CreateScriptFormTemplate(EditorPrefsTable.Script_HotfixProcedure_Folder, "VariableBehaviour", "VariableBehaviourTemplate",Handler);
                EditorPrefs.SetString("VariableScriptName",className);
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
            //参数
            Regex r = new Regex("(?<=(#region parameter))[.\\s\\S]*?(?=(#endregion))");//A为起始字符 B为结束字符
            arg = r.Replace(arg, sb.ToString(), 1); //*为替换字符串  1为替换最大次数
            
            //awake
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
