using System;
using System.Reflection;
using GameScripts.RunTime.Magic.Command;
using HT.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.RunTime.EditorMagic
{
    public sealed class CmdOptionEntity : IReference
    {
        private GameObject _entity;
        private GameObject _selected;

        private Action<int,bool> _callBack;

        public int Index { get; private set; }

        public void Fill(GameObject entity,CommandData command, int index, Action<int,bool> callBack)
        {
            _entity = entity;
            _callBack = callBack;
            Index = index;
            var attribute = command.GetType().GetCustomAttribute<CommandAttribute>();
            
            _entity.GetComponentByChild<Text>("Index").text = (index + 1).ToString();
            _entity.GetComponentByChild<Text>("Label").text = attribute.WrapName;
             _entity.GetComponentByChild<Text>("Time").text = command.StartTime.ToString();
        
            _selected = _entity.FindChildren("Selected");
            
            _entity.GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _callBack?.Invoke(Index,_selected.activeSelf);
        }
        
        
        public void SetSelectedActive(bool active)
        {
            _selected.SetActive(active);
        }
        
        public void Reset()
        {
            if (_entity is null) return;
            Main.Kill(_entity);
            _entity = null;
        }
    }
}