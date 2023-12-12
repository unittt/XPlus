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
        private ArgBoxEntityBase _parent;

        public bool ActiveSelf => _entity == null || _entity.activeSelf;

        public virtual void Fill(GameObject entity,VarFieldInfo varFieldInfo, ArgBoxEntityBase parent)
        {
            _entity = entity;
            _varFieldInfo = varFieldInfo;
            _parent = parent;
            _argumentAttribute = varFieldInfo.GetCustomAttribute<ArgumentAttribute>();
            _entity.GetComponentByChild<Text>("Label").text = _argumentAttribute.displayName;
            if (!string.IsNullOrEmpty(_argumentAttribute.ShowMethod))
            {
                _isShowMethod = _varFieldInfo.Target.GetType().GetMethod(_argumentAttribute.ShowMethod, BindingFlags.Instance |BindingFlags.Public|BindingFlags.NonPublic );
            }

            SetDefaultValue();
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        protected virtual void SetDefaultValue()
        {
            var defaultValue = _argumentAttribute.DefaultValue;
            if (defaultValue is not  null)
            {
                _varFieldInfo.Value = defaultValue;
            }
            else
            {
                if (_varFieldInfo.FieldType == typeof(string))
                {
                    _varFieldInfo.Value = string.Empty;
                }
            }
        }
        

        public void Refresh()
        {
            var active = _parent?.ActiveSelf ?? true;
            if (active && _isShowMethod != null)
            {
                active = (bool)_isShowMethod.Invoke(_varFieldInfo.Target, null);
            }
            _entity.SetActive(active);
        }
        
        
        public virtual void Reset()
        {
            if (_entity is null) return;
            Main.Destroy(_entity);
            _entity = null;
            _isShowMethod = null;
        }
    }
}