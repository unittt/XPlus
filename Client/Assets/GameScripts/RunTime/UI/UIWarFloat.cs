using GameScript.RunTime.Config;
using GameScripts.RunTime;
using GameScripts.RunTime.Utility.Timer;
using HT.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScript.RunTime.UI
{
    [UIResource("UIWarFloat", UIType.Camera)]
    public class UIWarFloat:UILogicResident
    {
        private GameObject _bossWarShotHud;
        private Image _bossIcon;
        private TextMeshProUGUI _bossShotLabel;
        private int _hideBossTimeID;
        
        public override void OnInit()
        {
            base.OnInit();
            _bossWarShotHud = UIEntity.FindChildren("BossWarShotHud");
            _bossIcon = _bossWarShotHud.GetComponentByChild<Image>("Icon");
            _bossShotLabel =  _bossWarShotHud.GetComponentByChild<TextMeshProUGUI>("ShotLabel");
        }

        public void AddMsg(int icon, string msg, float time)
        {
            // TimerManager.StopTimer(_hideBossTimeID);
            _bossIcon.LoadDynamicImage(AssetConfig.AVATAR_ATLAS1, icon);
            _bossShotLabel.text = msg;
            _bossWarShotHud.SetActive(true);
            // _hideBossTimeID = TimerManager.RegisterTimer(time, HideBossWarShotHud);
        }

        private void HideBossWarShotHud()
        {
            _bossWarShotHud.SetActive(false);
        }
    }
}