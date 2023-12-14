using System;
using System.Collections.Generic;
using GameScripts.RunTime.Magic;
using GameScripts.RunTime.Utility;
using HT.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.RunTime.EditorMagic
{
    /// <summary>
    /// 编辑法术
    /// </summary>
    [UIResource("UIEditorMagic")]
    public sealed class UIEditorMagic : UILogicResident
    {
        private Transform _content;
        private GameObject _cmdOptionEntityPrefab;
        private Text _magicName;
        
        private MagicData _magicData;
        private List<CmdOptionEntity> _cmdOptionEntities;

        private int? _index;

        public override void OnInit()
        {
            var variableArray = UIEntity.GetComponent<VariableBehaviour>().Container;
            _content = variableArray.Get<Transform>("content");
            _cmdOptionEntityPrefab = variableArray.Get<GameObject>("cmdOption");
            
            _magicName = variableArray.Get<Text>("magicName");
            variableArray.Get<Button>("selectBtn").onClick.AddListener(OnClickSelect);
            variableArray.Get<Button>("saveBtn").onClick.AddListener(OnClickSave);
            variableArray.Get<Button>("btnAdd").onClick.AddListener(OnClickAdd);
            variableArray.Get<Button>("btnDelete").onClick.AddListener(OnClickDelete);

            _cmdOptionEntities = new List<CmdOptionEntity>();
        }
        
        public override void OnOpen(params object[] args)
        {
            OnMagicChanged(null,string.Empty);
            EditorMagicManager.OnMagicChanged += OnMagicChanged;
            EditorMagicManager.OnMagicCmdChanged += OnMagicCmdChanged;
        }
        
        private void OnMagicChanged(MagicData magicData, string fileName)
        {
            _magicData = magicData;
            _magicName.text = string.IsNullOrEmpty(fileName)?"选择法术文件": fileName;
            OnMagicCmdChanged(magicData);
        }
        
        private void OnMagicCmdChanged(MagicData magicData)
        {
            //清理指令
            Main.m_ReferencePool.Despawns(_cmdOptionEntities);
            _index = null;
            if (magicData is null) return;
            //创建指令
            var count = magicData.Commands.Count;
            for (var i = 0; i < count; i++)
            {
                var entity = Main.Clone(_cmdOptionEntityPrefab, _content);
                var cmdOptionEntity = Main.m_ReferencePool.Spawn<CmdOptionEntity>();
                cmdOptionEntity.Fill(entity,magicData.Commands[i],i, OnSelect);
                _cmdOptionEntities.Add(cmdOptionEntity);
            }
        }
        

        private void OnSelect(int index, bool isSelect)
        {
            if (isSelect)
            {
                //克隆一个实体
                var command = _magicData.Commands[index];
                command = SerializationHelper.DeepClone(command);
                Main.m_UI.OpenUI<UIEditorMagicBuildCmd>(index,command);
            }
            else
            {
                //选中
                foreach (var element in _cmdOptionEntities)
                {
                    element.SetSelectedActive(element.Index == index);
                }
            }
            _index = index; 
        }

        private void OnClickSave()
        {
            if (_magicData != null)
            {
                EditorMagicManager.SaveMagicData(_magicData);
            }
        }

        private void OnClickSelect()
        {
            Main.m_UI.OpenUI<UIEditorMagicList>();
        }
        
        private void OnClickAdd()
        {
            Main.m_UI.OpenUI<UIEditorMagicBuildCmd>();
        }
        
        private void OnClickDelete()
        {
            if (!_index.HasValue) return;
            EditorMagicManager.RemoveCmd(_index.Value);
            _index = null;
        }
    }
}