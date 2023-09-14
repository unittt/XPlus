using System;
using UnityEngine;

namespace GameScripts.RunTime.GridMapEditor
{
    public class GridMapSceneListener : MonoBehaviour
    {
        public static Action onApplicationQuit;
        void OnApplicationQuit()
        {
            if (onApplicationQuit != null)
                onApplicationQuit();
        }
    }
}