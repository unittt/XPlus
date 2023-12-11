using System.Reflection;
using GameScripts.RunTime.Magic.Command;
using GameScripts.RunTime.Utility.Variable;
using HT.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.RunTime.EditorMagic
{
    public abstract class ArgBoxEntityBase: IReference
    {
        protected VarFieldInfo _varFieldInfo;
        protected ArgumentAttribute _argumentAttribute;
        protected GameObject _entity;
        private MethodInfo _isShowMethod;

        public virtual void Fill(GameObject entity,VarFieldInfo varFieldInfo)
        {
            _entity = entity;
            _varFieldInfo = varFieldInfo;
            _argumentAttribute = varFieldInfo.GetCustomAttribute<ArgumentAttribute>();
            _entity.GetComponentByChild<Text>("Label").text = _argumentAttribute.displayName;
            if (!string.IsNullOrEmpty(_argumentAttribute.ShowMethod))
            {
                _isShowMethod = _varFieldInfo.Target.GetType().GetMethod(_argumentAttribute.ShowMethod);
            }
        }

        public virtual void Refresh()
        {
            if (_isShowMethod == null) return;
            var result = (bool)_isShowMethod.Invoke(_varFieldInfo.Target, null);
            _entity.SetActive(result);
        }
        
        public virtual void Reset()
        {
            if (_entity is null) return;
            Main.Destroy(_entity);
            _entity = null;
        }
    }
}