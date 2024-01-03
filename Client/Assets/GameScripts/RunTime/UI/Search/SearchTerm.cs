using System;
using HT.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace GameScript.RunTime.UI.Search
{
    public sealed class SearchTerm: IReference
    {
        private GameObject _entity;
        private GameObject _selected;
        private Action<SearchTerm> _callback;
        public string Context { get; private set; }

        public void Fill(GameObject entity, string context, Action<SearchTerm> callBack)
        {
            _entity = entity;
            Context = context;
            _callback = callBack;
            _selected = _entity.FindChildren("Selected");
            _entity.GetComponent<Button>().onClick.AddListener(OnClick);
            _entity.GetComponentByChild<Text>("Label").text = context;
        }

        private void OnClick()
        {
            _callback?.Invoke(this);
        }
        
        public void SetSelectedActive(bool active)
        {
            _selected.SetActive(active);
        }

        public void SetActive(bool active)
        {
            _entity.SetActive(active);
        }
        
        public void Reset()
        {
           Main.Kill(_entity);
           _entity = null;
        }
    }
}