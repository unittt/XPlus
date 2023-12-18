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
        private static Type ComplexType = typeof(ComplexBase);
        private List<CommandOption> _cmdOptions;

        private Transform _argContent;
        private GameObject _argBoxPrefab;
        private GameObject _complexArgBoxPrefab;
        private InputField _startInputField;
        
        private List<ArgBoxEntityBase> _argBoxEntities = new();

        private Type CmdType => _curCommand?.GetType();

        //当前指令数据
        private CommandData _curCommand;

        //替换的索引
        private int _replaceIndex;

        public override void OnInit()
        {
            var variableArray = UIEntity.GetComponent<VariableBehaviour>().Container;
            var commandBtnPrefab = variableArray.Get<GameObject>("commandBtn");
            var commandBtnParent = variableArray.Get<Transform>("commandBtnParent");
            _argContent = variableArray.Get<Transform>("argContent");
            _argBoxPrefab = variableArray.Get<GameObject>("argBox");
            _complexArgBoxPrefab = variableArray.Get<GameObject>("complexArgBox");

            _startInputField = variableArray.Get<InputField>("startInputField");
            _startInputField.onValueChanged.AddListener(OnStartTimeValueChanged);
            variableArray.Get<Button>("confirmBtn").onClick.AddListener(OnClickConfirm);
            variableArray.Get<Button>("closeBtn").onClick.AddListener(Close);
            
            
            //2.实例化指令按钮
            _cmdOptions = new List<CommandOption>();
            foreach (var pair in EditorMagicManager.T2AInstance)
            {
                var entity = Main.Clone(commandBtnPrefab, commandBtnParent, false);
                var commandOption = new CommandOption(entity, pair.Key, pair.Value.WrapName, ShowCmd);
                _cmdOptions.Add(commandOption);
            }
        }

        private void OnStartTimeValueChanged(string arg0)
        {
            float.TryParse(arg0, out var value);
            if (_curCommand != null)
            {
                _curCommand.StartTime = value;
            }
        }


        public override void OnOpen(params object[] args)
        {
            _curCommand = null;
            _replaceIndex = -1;
            _startInputField.SetTextWithoutNotify(string.Empty);
            if (args is { Length: > 0 })
            {
                _replaceIndex = (int)args[0];
                var command = args[1].Cast<CommandData>();
                var type = command.GetType();
                foreach (var option in _cmdOptions)
                {
                    option.SetActive(option.CmdType == type);
                }

                ShowCmd(command);
            }
            else
            {
                foreach (var option in _cmdOptions)
                {
                    option.SetActive(true);
                }

                ShowCmd(_cmdOptions[0].CmdType);
            }
        }
        
        private void ShowCmd(Type cmdType)
        {
            if (CmdType == cmdType) return;

            //实例化指令
            var curCommand = Activator.CreateInstance(cmdType).Cast<CommandData>();
            ShowCmd(curCommand);
        }

        
        private void ShowCmd(CommandData command)
        {
            //记录指令的类型
            _curCommand = command;

            //高亮选中的标签
            foreach (var commandOption in _cmdOptions)
            {
                commandOption.SetSelectActive(commandOption.CmdType == CmdType);
            }
            
            //清理所有元素
            var maxIndex = _argBoxEntities.Count - 1;
            for (var i = maxIndex; i >= 0; i--)
            {
                Main.m_ReferencePool.Despawn(_argBoxEntities[i]);
            }
            _argBoxEntities.Clear();
            
            CreateCmdFieldView(_curCommand,CmdType, _argContent);
            _startInputField.SetTextWithoutNotify(_curCommand.StartTime.ToString());
            //刷新
            foreach (var argBoxEntity in _argBoxEntities)
            {
                argBoxEntity.Refresh();
            }
        }
        
        
        private void CreateCmdFieldView(object target, Type cmdType, Transform parent, ArgBoxEntityBase parentArgBox = null)
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

                //复合参数
                if (fieldInfo.FieldType.IsSubclassOf(ComplexType))
                {
                  
                    varFieldInfo.Value ??= Activator.CreateInstance(fieldInfo.FieldType);
                    
                    //创建一个复合box
                    // var complexArgBox = Main.Clone(_complexArgBoxPrefab, parent);
                    var complexArgBox = Main.Clone(_complexArgBoxPrefab, _argContent);
                    
                    var complexArgBoxEntity = Main.m_ReferencePool.Spawn<ComplexArgBoxEntity>();
                    complexArgBoxEntity.Fill(complexArgBox, varFieldInfo,parentArgBox);
                    _argBoxEntities.Add(complexArgBoxEntity);
                    
                    var target2 = fieldInfo.GetValue(target);
                    CreateCmdFieldView(target2,fieldInfo.FieldType, complexArgBoxEntity.Container, complexArgBoxEntity);
                }
                else
                {
                    var argBox = Main.Clone(_argBoxPrefab, parent);
                    var argBoxEntity = Main.m_ReferencePool.Spawn<ArgBoxEntity>();
                    
                    argBoxEntity.Fill(argBox,varFieldInfo,parentArgBox);
                    _argBoxEntities.Add(argBoxEntity);
                }
                
                varFieldInfo.ValueChange += OnFieldInfoValueChanged;
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
        
        
        private void OnClickConfirm()
        {
            if (_curCommand == null) return;

            //为替换
            if (_replaceIndex >= 0)
            {
                EditorMagicManager.ReplaceCmd(_replaceIndex, _curCommand);
            }
            else
            {
                //增加指令
                EditorMagicManager.AddCmd(_curCommand);
            }
           
            Close();
        }


        #region 指令标签选项
        private class CommandOption
        {
            private GameObject _entity;
            private GameObject _select;
            public Type CmdType { get; }

            public CommandOption(GameObject entity ,Type cmdType, string label, Action<Type> callBack)
            {
                _entity = entity;
                CmdType = cmdType;
                _entity.GetComponentByChild<Text>("Label").text = label;
                _entity.GetComponent<Button>().onClick.AddListener(() => { callBack(CmdType);});
                _select = _entity.FindChildren("sprite");
            }

            public void SetSelectActive(bool active)
            {
                _select.SetActive(active);
            }

            public void SetActive(bool active)
            {
                _entity.SetActive(active);
            }
        }
        #endregion
    }
}