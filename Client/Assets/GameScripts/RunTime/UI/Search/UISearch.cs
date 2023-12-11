using System;
using System.Collections.Generic;
using GameScripts.RunTime.Utility.Selector;
using HT.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace GameScript.RunTime.UI.Search
{
    
    [UIResource("UISearch")]
    public class UISearch: UILogicResident
    {
        private InputField _searchInputField;
        private Transform _content;
        private GameObject _searchElementEntityPrefab;

        //选择器
        private SelectorHandler _selectorHandler;
        private List<string> _context;

        //元素集合
        private List<SearchElement> _elements;
        private SearchElement _element;

        private Action<object> _callBack;


        public override void OnInit()
        {
            var variableArray = UIEntity.GetComponent<VariableBehaviour>().Container;
            _searchInputField = variableArray.Get<InputField>("searchInputField");
            _searchInputField.onValueChanged.AddListener(OnSearchInputValueChanged);
            _content = variableArray.Get<Transform>("content");
            _searchElementEntityPrefab = variableArray.Get<GameObject>("searchElementEntity");
            variableArray.Get<Button>("confirmBtn").onClick.AddListener(OnClickConfirm);
            variableArray.Get<Button>("closeBtn").onClick.AddListener(Close);

            _elements = new List<SearchElement>();
            _context = new List<string>();
        }
        
        public override void OnOpen(params object[] args)
        {
            base.OnOpen(args);
            
            var selectHandlerType = args[0].Cast<Type>();
            _callBack = args[1].Cast<Action<object>>();
            _selectorHandler = Activator.CreateInstance(selectHandlerType).Cast<SelectorHandler>();
            _selectorHandler.GetElementCollection(_context);
            
            //1.清理数据
            Main.m_ReferencePool.Despawns(_elements);
            _searchInputField.text = "";
            _element = null;
            
            //2.生成数据
            foreach (var value in _context)
            {
                var element = Main.m_ReferencePool.Spawn<SearchElement>();
                var entity = Main.Clone(_searchElementEntityPrefab, _content);
                element.Fill(entity,value,OnSelect);
                _elements.Add(element);
            }
        }
        
        private void OnSearchInputValueChanged(string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
            {
                foreach (var element in _elements)
                {
                    element.SetActive(true);
                }
                return;
            }
            
            foreach (var element in _elements)
            {
                element.SetActive(element.Context.Contains(newValue));
            }
        }
        
        private void OnSelect(SearchElement obj)
        {
            foreach (var element in _elements)
            {
                element.SetSelectedActive(false);
            }
            obj.SetSelectedActive(true);
            _element = obj;
        }
        
        private void OnClickConfirm()
        {
            if (_element != null)
            {
                //设置值
                var obj = _selectorHandler.GetValue(_element.Context);
                _callBack?.Invoke(obj);
            }
            
            Close();
        }
    }
}