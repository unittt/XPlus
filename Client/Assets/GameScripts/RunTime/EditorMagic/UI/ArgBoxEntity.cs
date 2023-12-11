using System;
using System.Reflection;
using GameScript.RunTime.UI.Search;
using GameScripts.RunTime.Magic.Command;
using GameScripts.RunTime.Utility;
using GameScripts.RunTime.Utility.Variable;
using HT.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.RunTime.EditorMagic
{
    public class ArgBoxEntity : ArgBoxEntityBase
    {
        private Type _selectHandlerType;
        private InputField _inputField;
        private bool _isEnum;

        public override void Fill(GameObject entity,VarFieldInfo varFieldInfo)
        {
            base.Fill(entity, varFieldInfo);
            _isEnum = varFieldInfo.FieldType.IsEnum;
            
            var selectHandlerAttribute = varFieldInfo.Info.GetCustomAttribute<SelectHandlerAttribute>();
            var hasSelectHandler = selectHandlerAttribute != null;
            
            var addBtn = entity.FindChildren("AddBtn");
            addBtn.GetComponent<Button>().onClick.AddListener(OnClickAdd);
            _inputField = entity.GetComponentByChild<InputField>("InputField");
            
            RefreshFieldInfoText();
            
            _inputField.interactable = !hasSelectHandler;
            addBtn.SetActive(hasSelectHandler);
            if (hasSelectHandler)  _selectHandlerType = selectHandlerAttribute.SelectHandler;
        }
        
        private void OnClickAdd()
        {
            var action = (Action<object>)OnSelectCallBack;
            Main.m_UI.OpenUI<UISearch>(_selectHandlerType,action);
        }

        private void OnSelectCallBack(object value)
        {
            _varFieldInfo.Value = value;
            RefreshFieldInfoText();
        }


        private void RefreshFieldInfoText()
        {
            _inputField.text = _isEnum ? ((Enum)_varFieldInfo.Value).GetInspectorName() : _varFieldInfo.Value.ToString();
        }
    }
}