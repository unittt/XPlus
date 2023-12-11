using UnityEngine;

namespace GameScripts.RunTime.Utility.ContentSizeFitter
{
    public class ContentSizeFitterParent : UnityEngine.UI.ContentSizeFitter
    {
        private RectTransform _rect;
        private RectTransform rectTm
        {
            get
            {
                if (_rect == null)
                    _rect = GetComponent<RectTransform>();
                return _rect;
            }
        }
        
        private RectTransform _prarentRect;
        private RectTransform prarentRect
        {
            get
            {
                if (_prarentRect == null)
                    _prarentRect = transform.parent.GetComponent<RectTransform>();
                return _prarentRect;
            }
        }
        
        
        protected override void OnRectTransformDimensionsChange()
        {
           base.OnRectTransformDimensionsChange();
           if (prarentRect is null) return;
           var sizeDelta = rectTm.sizeDelta;
           var parentRectSizeDelta = prarentRect.sizeDelta;
               
           if (horizontalFit != FitMode.Unconstrained)
           {
               parentRectSizeDelta.x = sizeDelta.x;
           }

           if (verticalFit != FitMode.Unconstrained)
           {
               parentRectSizeDelta.y = sizeDelta.y;
           }

           prarentRect.sizeDelta = parentRectSizeDelta;
        }
    }
}