using System.Collections.Generic;
using System.IO;
using GameScript.RunTime.UI.Search;
using GameScripts.RunTime.Magic;
using GameScripts.RunTime.Utility.Selector;
using HT.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.RunTime.EditorMagic
{
    
    /// <summary>
    /// 法术列表
    /// </summary>
    [UIResource("UIEditorMagicList")]
    public class UIEditorMagicList:UILogicResident
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

        private List<MagicData> _magicDatas;

        public override void OnInit()
        {
            var variableArray = UIEntity.GetComponent<VariableBehaviour>().Container;
            _searchInputField = variableArray.Get<InputField>("searchInputField");
            _searchInputField.onValueChanged.AddListener(OnSearchInputValueChanged);
            _content = variableArray.Get<Transform>("content");
            _searchElementEntityPrefab = variableArray.Get<GameObject>("searchElementEntity");
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
        
            //2.生成数据
            foreach (var value in EditorMagicManager.MagicDatas)
            {
                var element = Main.m_ReferencePool.Spawn<SearchElement>();
                var entity = Main.Clone(_searchElementEntityPrefab, _content);
                element.Fill(entity,value.Name,OnSelect);
                _elements.Add(element);
            }
        }

        private void xxxxx()
        {
            
            //
            
            _magicDatas.Clear();
            var path = "Assets/GameRes/MagicFile";
            // 遍历目录下的所有文件
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                // 检查文件扩展名是否为 .jso
                if (!Path.GetExtension(file).Equals(".json", System.StringComparison.OrdinalIgnoreCase)) continue;
                var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(file);

                if (textAsset == null) continue;
              
                // var mapData = MagicData .Deserialize(textAsset.text);
                // _magicDatas.Add(mapData);
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
                // _callBack?.Invoke(obj);
            }
            
            Close();
        }
        
        private void OnClickDelete()
        {
           
            
        }

        private void OnClickAdd()
        {
            Main.m_UI.OpenUI<UIEditorMagicNew>();
            
        }
    }
}