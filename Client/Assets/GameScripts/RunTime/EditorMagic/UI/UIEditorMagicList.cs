using System.Collections.Generic;
using GameScript.RunTime.UI.Search;
using GameScripts.RunTime.Magic;
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
        
        //元素集合
        private List<SearchTerm> _elements;
        private SearchTerm term;

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

            _elements = new List<SearchTerm>();
            _magicDatas = new List<MagicData>();
        }
        
        public override void OnOpen(params object[] args)
        {
            //1.清理数据
            Main.m_ReferencePool.Despawns(_elements);
            _searchInputField.text = "";
            term = null;
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
            var element = Main.m_ReferencePool.Spawn<SearchTerm>();
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
        
        private void OnSelect(SearchTerm obj)
        {
            foreach (var element in _elements)
            {
                element.SetSelectedActive(false);
            }
           
            term = obj;
            term.SetSelectedActive(true);
            _confirmBtn.SetActive(true);
        }
        
        private void OnClickConfirm()
        {
            if (term != null)
            {
                //编辑这个法术
                EditorMagicManager.EditorMagic(term.Context);
            }
            
            Close();
        }
        
        private void OnClickDelete()
        {
            if (term == null) return;
            EditorMagicManager.DeleteMagicFile(term.Context);
            term = null;
            _confirmBtn.SetActive(false);
        }

        private void OnClickAdd()
        {
            Main.m_UI.OpenUI<UIEditorMagicNew>();
        }
    }
}