using HT.Framework;
using UnityEngine.UI;

namespace GameScripts.RunTime.EditorMagic
{
    /// <summary>
    /// 编辑法术
    /// </summary>
    [UIResource("UIEditorMagic")]
    public sealed class UIEditorMagic : UILogicResident
    {
        public override void OnInit()
        {
            var variableArray = UIEntity.GetComponent<VariableBehaviour>().Container;
            variableArray.Get<Button>("selectBtn").onClick.AddListener(OnClickSelect);
            variableArray.Get<Button>("saveBtn").onClick.AddListener(OnClickSave);
        }

        private void OnClickSave()
        {
            
        }

        private void OnClickSelect()
        {
            Main.m_UI.OpenUI<UIEditorMagicList>();
        }
    }
}