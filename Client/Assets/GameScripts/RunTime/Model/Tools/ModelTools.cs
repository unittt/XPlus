using System;
using System.Collections.Generic;
using GameScripts.RunTime.Define;
using UnityEngine;


namespace GameScripts.RunTime.Model
{
    public static class ModelTools
    {
        private static Dictionary<string, int> g_StateToHash = new Dictionary<string, int>();
        private static Dictionary<int, string> g_HashToState = new Dictionary<int, string>();
        private static float g_FrameDelta = 1f / 30f;

        

        /// <summary>
        /// 将动画状态名称转换为一个哈希值
        /// </summary>
        /// <param name="sState"></param>
        /// <returns></returns>
        public static int StateToHash(string sState)
        {
            if (g_StateToHash.TryGetValue(sState, out var iHash)) return iHash;
            var sHashStr = $"BaseLayer.{sState}";
            iHash = Animator.StringToHash(sHashStr);
            g_StateToHash[sState] = iHash;
            g_HashToState[iHash] = sState;

            return iHash;
        }

        /// <summary>
        /// 将哈希值转换回状态名称
        /// </summary>
        /// <param name="iHash"></param>
        /// <returns></returns>
        public static string HashToState(int iHash)
        {
            return g_HashToState.TryGetValue(iHash, out var sState) ? sState : null;
        }

        /// <summary>
        /// 将帧数转换为时间
        /// </summary>
        /// <param name="iFrame"></param>
        /// <returns></returns>
        public static float FrameToTime(int iFrame)
        {
            return iFrame * g_FrameDelta;
        }

        /// <summary>
        /// 将时间转换回帧数
        /// </summary>
        /// <param name="iTime"></param>
        /// <returns></returns>
        public static int TimeToFrame(float iTime)
        {
            return Mathf.Max(0, Mathf.FloorToInt(iTime / g_FrameDelta + 0.5f));
        }

        /// <summary>
        /// 检查一个状态是否是通用状态
        /// </summary>
        /// <param name="sState"></param>
        /// <returns></returns>
        public static bool IsCommonState(string sState)
        {
            return Array.IndexOf(GameDefines.Model.COMMON_STATE, sState) >= 0;
        }
    }
}