using DG.Tweening;
using GameScripts.RunTime.Hud.Attribute;
using GameScripts.RunTime.Magic;
using GameScripts.RunTime.Utility.Timer;
using HT.Framework;
using TMPro;
using UnityEngine;

namespace GameScripts.RunTime.Hud
{
    /// <summary>
    /// 聊天 HUD(文字内容)
    /// </summary>
    [HudEntity("ChatHud",false)]
    public class ChatHudEntityLogic : HudEntityLogicBase
    {
        /// <summary>
        /// 持续时间
        /// </summary>
        private const float DURATION = 2f;
        private static readonly Vector2 padding = new Vector2(0.2f, 0);
        private static readonly float offsetY = 0.1f;
        
        private SpriteRenderer _background;
        private TextMeshPro _chatTMP;
        private Vector2 _backgroundSize;
        
        private ChatHudEntityLogic _newChatHud;
        private float _endY;
        
        public override BodyPart Part { get; } = BodyPart.Head;


        private int _timeKey;


        public override void OnInit()
        {
            _background = Entity.GetComponentByChild<SpriteRenderer>("Table/Bg");
            _chatTMP = Entity.GetComponentByChild<TextMeshPro>("Table/Chat");
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            TimerManager.StopTimer(_timeKey);
        }

        public void Show(string chat)
        {
            _endY = Entity.transform.localPosition.y;
            _chatTMP.text = chat;
            Main.Current.NextFrameExecute(UpdateBackgroundSize);
            _timeKey = TimerManager.RegisterTimer(DURATION, Kill);
        }

        private void UpdateBackgroundSize()
        {
            var textRenderedSize = _chatTMP.GetRenderedValues(false);
            _backgroundSize = new Vector2(
                textRenderedSize.x + padding.x, 
                textRenderedSize.y + padding.y
            );
            // 调整背景大小以适应文本，加上额外的padding
            _background.size = _backgroundSize;
        }

        public override void OnCreateNewHud(HudEntityLogicBase newHud)
        {
            _newChatHud = newHud.Cast<ChatHudEntityLogic>();
            //等待下一帧
            Main.Current.NextFrameExecute(MoveUp);
        }

        private void MoveUp()
        {
            _endY += _newChatHud._backgroundSize.y + offsetY;
            Entity.transform.DOKill();
            Entity.transform.DOLocalMoveY(_endY, 0.2f);
        }
    }
}