using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Audio
{
    public class AudioManager : SingletonBase<AudioManager>
    {
        private bool _isLoading;    //单线下载中
        private WaitUntil _loadWait;    //单线下载等待;


        private readonly Dictionary<string, AudioClip> audioClips = new();

        public AudioManager()
        {
            _loadWait = new WaitUntil(() => !_isLoading);
        }
        
        public void PlayEffect(string location)
        {
            PlayEffectAsync(location).Forget();
        }

        private async UniTaskVoid PlayEffectAsync(string location)
        {
            var audioClip = await LoadAudioClip(location);
            Main.m_Audio.PlayMultipleSound(audioClip);
        }

        private async UniTask<AudioClip> LoadAudioClip(string location)
        {
            //单线加载，如果其他地方在加载资源，则等待
            if (_isLoading)
            {
                await _loadWait;
            }
            //轮到本线路加载资源
            _isLoading = true;
            
            if (!audioClips.ContainsKey(location))
            {
                var audioClip = await Main.m_Resource.LoadAsset<AudioClip>(location);
                audioClips[location] = audioClip;
            }
            _isLoading = false;
            return audioClips[location];
        }
    }
}