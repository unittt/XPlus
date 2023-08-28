using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace HT.Framework
{
    
    [CustomEditor(typeof(VariableBehaviour),true)]
    public class VariableBehaviourEditor : HTFEditor<VariableBehaviour>
    {
        
        private const string VARIABLE_Script_Name = "VariableScriptName";
        private const  string VARIABLE_INSTANCE_ID = "VariableInstanceID";
        
        private bool _isDefault;
        
        protected override bool IsEnableRuntimeData => true;

        
        protected override void OnDefaultEnable()
        {
            base.OnDefaultEnable();
            // _isDefault = !Target.GetType().IsSubclassOf(typeof(VariableBehaviour));
        }
        
        #region InspectorDefault
        protected override void OnInspectorDefaultGUI()
        {
            if (!(EditorApplication.isPlaying && !_isDefault))
            {
                PropertyField(nameof(Target.Container), "Container");
            }
         
            if (EditorApplication.isPlaying) return;
            
            if (_isDefault)
            {
                InspectorDefaultGUI();
            }
            else
            {
                InspectorDerivedGUI();
            }
        }
        
        /// <summary>
        /// 绘制默认的Inspector
        /// </summary>
        private void InspectorDefaultGUI()
        {
            if (!SafetyCheckTarget())
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.HelpBox("请检查变量的合法性", MessageType.Error);
                EditorGUILayout.EndHorizontal();
                return;
            }
            
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Generate Script", EditorGlobalTools.Styles.LargeButton))
            {
                CreateScript();
            }
            GUI.backgroundColor = Color.white;
        }

        /// <summary>
        /// 绘制派生类的Inspector
        /// </summary>
        private void InspectorDerivedGUI()
        {
            if (!SafetyCheckTarget())
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.HelpBox("请检查变量的合法性", MessageType.Error);
                EditorGUILayout.EndHorizontal();
                return;
            }

            if (!IsChanged())
            {
                return;
            }
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("变量发生改变 请应用", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
            
            GUI.backgroundColor = Color.green;
            
            if (GUILayout.Button("Apply", EditorGlobalTools.Styles.LargeButton))
            {
                ApplyScript();
            }
            GUI.backgroundColor = Color.white;
        }
        #endregion

        
        protected override void OnInspectorRuntimeGUI()
        {
            base.OnInspectorRuntimeGUI();

            if (_isDefault)return;
            
            GUI.enabled = false;
            var properties = Target.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var attribute = propertyInfo.GetCustomAttribute<VariableAutoGenerateAttribute>();

                if (attribute == null) continue;
                EditorGUILayout.BeginHorizontal();
                 var value = Target.Container.Get(propertyInfo.Name);
                 var valueType = value.GetType();
                 if (valueType.IsValueType || valueType == typeof(string))
                 {
                     //绘制文本
                     TextField(value.ToString(),out _,propertyInfo.Name);
                 }
                 else
                 {
                     var objValue = value.Cast<UnityEngine.Object>();
                     if (objValue)
                     {
                         EditorGUILayout.ObjectField(propertyInfo.Name, objValue, valueType, true);
                     }
                 }
                 EditorGUILayout.EndHorizontal();
            }
            GUI.enabled = true;
        }

        #region 检查数据
        /// <summary>
        /// 数据是否发生了变动
        /// </summary>
        /// <returns></returns>
        private bool IsChanged()
        {
            //获取所有自动生成的属性
            var properties = Target.GetType().GetProperties();
            var propertyInfos = (from propertyInfo in properties let attribute = propertyInfo.GetCustomAttribute<VariableAutoGenerateAttribute>() where attribute != null select propertyInfo).ToList();

            //判断属性数量是否一致
            if (propertyInfos.Count != Target.Container.Variables.Count)
            {
                return true;
            }
            //1.判断该变量名称是否存在
            //2.判断变量类型是否一致
            foreach (var variable in Target.Container.Variables)
            {
                var propertyInfo = propertyInfos.Find(pInfo =>pInfo.Name == variable.Name);
                if (propertyInfo == null || variable.ValueType != propertyInfo.PropertyType)return true;
            }
            return false;
        }

        /// <summary>
        /// 检测目标的合法性
        /// </summary>
        /// <returns></returns>
        private bool SafetyCheckTarget()
        {
            var names = new List<string>();
            foreach (var variable in Target.Container.Variables)
            {
                if (!variable.IsValidVariable || names.Contains(variable.Name))
                {
                    return false;
                }
                //加入变量名称
                names.Add(variable.Name);
            }
            return true;
        }
        #endregion
        
        #region Tool
        /// <summary>
        /// 创建脚本
        /// </summary>
        private void CreateScript()
        {
            EditorPrefs.SetInt(VARIABLE_INSTANCE_ID,Target.gameObject.GetInstanceID());
            var className = EditorGlobalTools.CreateScriptFormTemplate("", "VariableBehaviour", "VariableBehaviourTemplate",ReplaceHandler);
            EditorPrefs.SetString(VARIABLE_Script_Name,className);
        }

        
        /// <summary>
        /// 应用脚本 
        /// </summary>
        private void ApplyScript()
        {
            var monoScript = MonoScript.FromMonoBehaviour(Target);
            var scriptPath = AssetDatabase.GetAssetPath(monoScript);
            var code = monoScript.text;
            code = ReplaceHandler(code);
            
            File.WriteAllText(scriptPath, code, Encoding.UTF8);
            AssetDatabase.Refresh();
        }
   

        /// <summary>
        /// 脚本文本替换
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string ReplaceHandler(string arg)
        {
            var sb = new StringBuilder();
            sb.Append("\r\n");
            foreach (var variable in Target.Container.Variables)
            {
                sb.Append($"    [VariableAutoGenerate] public {variable.ValueType} {variable.Name} {{ get; private set; }}");
                sb.Append("\r\n");
            }
            //参数
            var r = new Regex("(?<=(#region parameter))[.\\s\\S]*?(?=(#endregion))");//A为起始字符 B为结束字符
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
        
        /// <summary>
        /// 绑定脚本
        /// </summary>
        [DidReloadScripts]
        private static void BindScriptsToObj()
        {
            var className = EditorPrefs.GetString(VARIABLE_Script_Name,"");
            EditorPrefs.SetString(VARIABLE_Script_Name,"");
            if (string.IsNullOrEmpty(className)) return;
            
            var type = ReflectionToolkit.GetTypeInRunTimeAssemblies(className);
            
            var instanceID = EditorPrefs.GetInt(VARIABLE_INSTANCE_ID,0);

            var vbGo = EditorUtility.InstanceIDToObject(instanceID).Cast<GameObject>();
            
            var vb = vbGo.GetComponent<VariableBehaviour>();
            var newVB = vbGo.AddComponent(type).Cast<VariableBehaviour>();
            newVB.Container = vb.Container;
            
            DestroyImmediate(vb);
            
            //如果在Hierarchy视图中 判断是否为预制体
            if ( PrefabUtility.IsPartOfPrefabInstance(vbGo))
            {
                //如果是预制体
                PrefabUtility.ApplyPrefabInstance(vbGo, InteractionMode.AutomatedAction);
            }
            
            //如果在编辑预制体视图中
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage)
            {
                EditorUtility.SetDirty(vbGo);
            }
        }
        #endregion
    }
}
