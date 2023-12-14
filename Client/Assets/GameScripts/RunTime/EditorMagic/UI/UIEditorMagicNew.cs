using HT.Framework;
using UnityEngine.UI;

namespace GameScripts.RunTime.EditorMagic
{
    
    [UIResource("UIEditorMagicNew")]
    public sealed class UIEditorMagicNew:UILogicResident
    {
        private InputField _inputField;
        
        public override void OnInit()
        {
            var variableArray = UIEntity.GetComponent<VariableBehaviour>().Container;
            _inputField = variableArray.Get<InputField>("inputField");
            variableArray.Get<Button>("confirmBtn").onClick.AddListener(OnClickConfirm);
            variableArray.Get<Button>("closeBtn").onClick.AddListener(Close);
        }

        public override void OnOpen(params object[] args)
        {
            base.OnOpen(args);
            _inputField.text = "";
        }

        private void OnClickConfirm()
        {
            if (string.IsNullOrEmpty( _inputField.text))
            {
                return;
            }

            if (EditorMagicManager.CreateMagicFile(_inputField.text))
            {
                Close();
            }
        }
    }
}