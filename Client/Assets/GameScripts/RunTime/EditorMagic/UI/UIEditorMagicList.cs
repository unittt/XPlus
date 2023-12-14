using System.Collections.Generic;
using GameScript.RunTime.UI.Search;
using GameScripts.RunTime.Magic;
using GameScripts.RunTime.Utility.Selector;
using HT.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.RunTime.EditorMagic
{
    
    /// <summary>
    /// 法术列表
    /// </summary>
    [UIResource("UIEditorMagicList")]
    public sealed class UIEditorMagicList:UILogicResident
    {
        private InputField _searchInputField;
        private Transform _content;
        private GameObject _searchElementEntityPrefab;

        private GameObject _confirmBtn;
        
        
        //选择器
        private SelectorHandler _selectorHandler;
        private List<string> _context;

        //元素集合
        private List<SearchElement> _elements;
        private SearchElement _element;

        private List<MagicData> _magicDatas;

        public override void OnInit()
        {
            var variableArray = UIEntity.GetComponent<VariableBehaviour>().Container;
            _searchInputField = variableArray.Get<InputField>("searchInputField");
            _searchInputField.onValueChanged.AddListener(OnSearchInputValueChanged);
            _content = variableArray.Get<Transform>("content");
            _searchElementEntityPrefab = variableArray.Get<GameObject>("searchElementEntity");
            _confirmBtn = variableArray.Get<Button>("confirmBtn").gameObject;
            
            variableArray.Get<Button>("confirmBtn").onClick.AddListener(OnClickConfirm);
            variableArray.Get<Button>("closeBtn").onClick.AddListener(Close);
            variableArray.Get<Button>("addBtn").onClick.AddListener(OnClickAdd);
            variableArray.Get<Button>("deleteBtn").onClick.AddListener(OnClickDelete);

            _elements = new List<SearchElement>();
            _context = new List<string>();
            _magicDatas = new List<MagicData>();
        }
        
        public override void OnOpen(params object[] args)
        {
            //1.清理数据
            Main.m_ReferencePool.Despawns(_elements);
            _searchInputField.text = "";
            _element = null;
       
            _confirmBtn.SetActive(false);
            
            //2.生成数据
            foreach (var fileName in EditorMagicManager.MagicDatas.Keys)
            {
                SpawnElement(fileName);
            }
            
            EditorMagicManager.OnCreateMagic += OnCreateMagic;
            EditorMagicManager.OnDeleteMagic += OnDeleteMagic;
        }

        public override void OnClose()
        {
            EditorMagicManager.OnCreateMagic -= OnCreateMagic;
            EditorMagicManager.OnDeleteMagic -= OnDeleteMagic;
        }

        private void OnCreateMagic(string fileName)
        {
            SpawnElement(fileName);
        }
        
        private void OnDeleteMagic(string fileName)
        {
            var element = _elements.Find(e => e.Context == fileName);
            if (element == null) return;
            _elements.Remove(element);
            Main.m_ReferencePool.Despawn(element);
        }

        private void SpawnElement(string context)
        {
            var element = Main.m_ReferencePool.Spawn<SearchElement>();
            var entity = Main.Clone(_searchElementEntityPrefab, _content);
            element.Fill(entity,context,OnSelect);
            _elements.Add(element);
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
           
            _element = obj;
            _element.SetSelectedActive(true);
            _confirmBtn.SetActive(true);
        }
        
        private void OnClickConfirm()
        {
            if (_element != null)
            {
                //设置值
                var obj = _selectorHandler.GetValue(_element.Context);
                // _callBack?.Invoke(obj);
            }
            
            Close();
        }
        
        private void OnClickDelete()
        {
            if (_element == null) return;
            EditorMagicManager.DeleteMagicFile(_element.Context);
            _element = null;
            _confirmBtn.SetActive(false);
        }

        private void OnClickAdd()
        {
            Main.m_UI.OpenUI<UIEditorMagicNew>();
        }
    }
}