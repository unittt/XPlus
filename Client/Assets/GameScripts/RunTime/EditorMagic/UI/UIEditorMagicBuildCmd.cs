using System;
using System.Collections.Generic;
using System.Reflection;
using GameScripts.RunTime.Magic.Command;
using GameScripts.RunTime.Utility.Variable;
using HT.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.RunTime.EditorMagic
{
    /// <summary>
    /// 编辑指令
    /// </summary>
    [UIResource("UIEditorMagicBuildCmd")]
    public sealed class UIEditorMagicBuildCmd : UILogicResident
    {
        private static Type IComplexType = typeof(IComplex);
        private Dictionary<Type, GameObject> _t2BtnOption;

        private Transform _argContent;
        private GameObject _argBoxPrefab;
        private GameObject _complexArgBoxPrefab;


        private List<ArgBoxEntityBase> _argBoxEntities = new();

        public override void OnInit()
        {
            var variableArray = UIEntity.GetComponent<VariableBehaviour>().Container;
            var commandBtnPrefab = variableArray.Get<GameObject>("commandBtn");
            var commandBtnParent  = variableArray.Get<Transform>("commandBtnParent");
            _argContent = variableArray.Get<Transform>("argContent");
            _argBoxPrefab = variableArray.Get<GameObject>("argBox");
            _complexArgBoxPrefab = variableArray.Get<GameObject>("complexArgBox");
            
            
            //1.查找所有指令
            var types = ReflectionToolkit.GetTypesInRunTimeAssemblies(type => type.IsSubclassOf(typeof(CommandBase)) && !type.IsAbstract);

            var c2t = new Dictionary<CommandAttribute, Type>();
            var commandAttributes = new List<CommandAttribute>();
            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<CommandAttribute>();
                commandAttributes.Add(attribute);
                c2t.Add(attribute, type);
            }
            //2.指令排序
            commandAttributes.Sort((x, y) => x.Sort.CompareTo(y.Sort));
            
            //3.实例化指令按钮
            _t2BtnOption = new Dictionary<Type, GameObject>();
            foreach (var commandAttribute in commandAttributes)
            {
                var cmdType = c2t[commandAttribute];
                var commandBtn = Main.Clone(commandBtnPrefab, commandBtnParent,false);
                commandBtn.GetComponentByChild<Text>("Label").text = commandAttribute.WrapName;
                commandBtn.GetComponent<Button>().onClick.AddListener(() => { ShowCmd(cmdType);});
                _t2BtnOption.Add(cmdType, commandBtn.FindChildren("sprite"));
            }
        }
        
        public override void OnOpen(params object[] args)
        {
            base.OnOpen(args);
        }
        
        private void ShowCmd(Type cmdType)
        {
            foreach (var expr in _t2BtnOption)
            {
                expr.Value.SetActive(expr.Key == cmdType);
            }

            var maxIndex = _argBoxEntities.Count - 1;
            for (var i = maxIndex; i >= 0; i--)
            {
                Main.m_ReferencePool.Despawn(_argBoxEntities[i]);
            }
            _argBoxEntities.Clear();
            
            //1.实例化对象
            var command = Activator.CreateInstance(cmdType).Cast<CommandBase>();
            ShowCmd2(command,cmdType, _argContent);

            //刷新
            foreach (var argBoxEntity in _argBoxEntities)
            {
                argBoxEntity.Refresh();
            }
        }


        
        public void ShowCmd(CommandBase command)
        {
            var cmdType = command.GetType();
            
        }
        
        private void ShowCmd2(object target, Type cmdType, Transform parent)
        {
            //2.获取目标的字段
            var fieldInfos = cmdType.GetFields((field) =>
            {
                var attribute = field.GetCustomAttribute<ArgumentAttribute>();
                return attribute != null;
            });

            foreach (var fieldInfo in fieldInfos)
            {
                var varFieldInfo = new VarFieldInfo(target, fieldInfo);
                varFieldInfo.ValueChange += OnFieldInfoValueChanged;
                
                //复合参数
                if (IComplexType.IsAssignableFrom(fieldInfo.FieldType))
                {
                    //创建一个复合box
                    var complexArgBox = Main.Clone(_complexArgBoxPrefab, parent);
                    
                    var complexArgBoxEntity = Main.m_ReferencePool.Spawn<ComplexArgBoxEntity>();
                    complexArgBoxEntity.Fill(complexArgBox, varFieldInfo);
                    _argBoxEntities.Add(complexArgBoxEntity);
                    
                    var target2 = fieldInfo.GetValue(target);
                    ShowCmd2(target2,fieldInfo.FieldType, complexArgBoxEntity.Container);
                }
                else
                {
                    var argBox = Main.Clone(_argBoxPrefab, parent);
                    var argBoxEntity = Main.m_ReferencePool.Spawn<ArgBoxEntity>();
                    
                    argBoxEntity.Fill(argBox,varFieldInfo);
                    _argBoxEntities.Add(argBoxEntity);
                }
            }
        }

        private void OnFieldInfoValueChanged(object obj)
        {
            //刷新
            foreach (var argBoxEntity in _argBoxEntities)
            {
                argBoxEntity.Refresh();
            }
        }
    }
}