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

        //选择器类型
        private Type _selectHandlerType;
        private List<string> _terms;

        //元素集合
        private List<SearchTerm> _searchTerms;
        private SearchTerm term;

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

            _searchTerms = new List<SearchTerm>();
            _terms = new List<string>();
        }
        
        public override void OnOpen(params object[] args)
        {
            base.OnOpen(args);
            
            _selectHandlerType = args[0].Cast<Type>();
            _callBack = args[1].Cast<Action<object>>();
            
            //1.清理数据
            _terms.Clear();
            Main.m_ReferencePool.Despawns(_searchTerms);
            _searchInputField.text = "";
            term = null;
            
            //2.获取新的数据
            SelectorManager.GetTerms(_selectHandlerType,_terms);
            
            //3.生成数据
            foreach (var value in _terms)
            {
                var element = Main.m_ReferencePool.Spawn<SearchTerm>();
                var entity = Main.Clone(_searchElementEntityPrefab, _content);
                element.Fill(entity,value,OnSelect);
                _searchTerms.Add(element);
            }
        }
        
        private void OnSearchInputValueChanged(string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
            {
                foreach (var element in _searchTerms)
                {
                    element.SetActive(true);
                }
                return;
            }
            
            foreach (var element in _searchTerms)
            {
                element.SetActive(element.Context.Contains(newValue));
            }
        }
        
        private void OnSelect(SearchTerm obj)
        {
            foreach (var element in _searchTerms)
            {
                element.SetSelectedActive(false);
            }
            obj.SetSelectedActive(true);
            term = obj;
        }
        
        private void OnClickConfirm()
        {
            if (term != null)
            {
                //设置值
                var obj =  SelectorManager.GetTermValue(_selectHandlerType,term.Context);
                _callBack?.Invoke(obj);
            }
            
            Close();
        }
    }
}