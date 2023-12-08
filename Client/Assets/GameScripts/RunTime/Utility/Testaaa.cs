using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.RunTime.Utility
{
    public class Testaaa : ContentSizeFitter
    {

        public Testaaa xxxxx;
        
        public  override void SetLayoutHorizontal()
        {
            base.SetLayoutHorizontal();
        }

        /// <summary>
        /// Calculate and apply the vertical component of the size to the RectTransform
        /// </summary>
        public override void SetLayoutVertical()
        {
           base.SetLayoutVertical();
        }
        
        protected override void OnRectTransformDimensionsChange()
        {
            SetDirty();
        }
    }
}