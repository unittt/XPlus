using HT.Framework;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT.Framework
{
    public sealed class VariableBehaviourEditor : HTFEditor<VariableBehaviour>
    {

        protected override void OnInspectorDefaultGUI()
        {
            if (GUILayout.Button("AAAAAAAAAAAAA"))
            {
                // Application.OpenURL(_GithubURL.URL);
            }

            Vector2Field(Vector2.down, out var a, "--------------------");
        }

        protected override bool IsEnableBaseInspectorGUI => true;
    }
}
