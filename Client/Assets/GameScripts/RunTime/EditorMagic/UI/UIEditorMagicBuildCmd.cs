using System;
using System.Collections.Generic;
using System.Reflection;
using GameScripts.RunTime.Magic.Command;
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
        private Dictionary<Type, GameObject> _t2BtnOption;

        public override void OnInit()
        {
            //1.获取所有指令数据
            //2.生成指令
            //3.

            var variableArray = UIEntity.GetComponent<VariableBehaviour>().Container;
            var commandBtnPrefab = variableArray.Get<GameObject>("commandBtn");
            var commandBtnParent  = variableArray.Get<Transform>("commandBtnParent");

            
            _t2BtnOption = new Dictionary<Type, GameObject>();
            var types = ReflectionToolkit.GetTypesInRunTimeAssemblies(type => type.IsSubclassOf(typeof(CommandBase)) && !type.IsAbstract);
            for (var i = 0; i < types.Count; i++)
            {
                var cmdType = types[i];
                var attribute = cmdType.GetCustomAttribute<CommandAttribute>();
                var commandBtn = Main.Clone(commandBtnPrefab, commandBtnParent,false);
                commandBtn.GetComponentByChild<Text>("Label").text = attribute.WrapName;
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
            
            //1.删除
            
            //2.查找属性,显示对应的属性
        }
        
        public void ShowCmd(CommandBase command)
        {
            
        }
    }
}