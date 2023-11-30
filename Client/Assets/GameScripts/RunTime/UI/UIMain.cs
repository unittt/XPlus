using HT.Framework;
using UnityEngine.UI;

namespace GameScript.RunTime.UI
{
    
    [UIResource("UIMain", UIType.Camera)]
    public class UIMain: UILogicResident
    {
        public override void OnInit()
        {
            base.OnInit();
            UIEntity.GetComponentInChildren<Button>().onClick.AddListener(OnClickPK);
        }

        private void OnClickPK()
        {
            
        }
    }
}