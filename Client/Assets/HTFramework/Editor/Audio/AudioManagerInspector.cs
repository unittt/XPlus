﻿using UnityEditor;
using UnityEngine;

namespace HT.Framework
{
    [CustomEditor(typeof(AudioManager))]
    [GiteeURL("https://gitee.com/SaiTingHu/HTFramework")]
    [GithubURL("https://github.com/SaiTingHu/HTFramework")]
    [CSDNBlogURL("https://wanderer.blog.csdn.net/article/details/89874351")]
    internal sealed class AudioManagerInspector : InternalModuleInspector<AudioManager, IAudioHelper>
    {
        private bool _backgroundAudioFoldout = true;
        private bool _singleAudioFoldout = true;
        private bool _multipleAudioFoldout = true;
        private bool _worldAudioFoldout = true;
        private bool _oneShootAudioFoldout = true;

        protected override string Intro => "Audio Manager, manage all audio playback, pause, stop, etc.";

        protected override void OnInspectorDefaultGUI()
        {
            base.OnInspectorDefaultGUI();

            GUI.enabled = !EditorApplication.isPlaying;

            PropertyField(nameof(AudioManager.MuteDefault), "Mute");
            
            GUILayout.BeginHorizontal();
            IntSlider(Target.BackgroundPriorityDefault, out Target.BackgroundPriorityDefault, 0, 256, "Background Priority");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            FloatSlider(Target.BackgroundVolumeDefault, out Target.BackgroundVolumeDefault, 0f, 1f, "Background Volume");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            IntSlider(Target.SinglePriorityDefault, out Target.SinglePriorityDefault, 0, 256, "Single Priority");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            FloatSlider(Target.SingleVolumeDefault, out Target.SingleVolumeDefault, 0f, 1f, "Single Volume");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            IntSlider(Target.MultiplePriorityDefault, out Target.MultiplePriorityDefault, 0, 256, "Multiple Priority");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            FloatSlider(Target.MultipleVolumeDefault, out Target.MultipleVolumeDefault, 0f, 1f, "Multiple Volume");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            IntSlider(Target.WorldPriorityDefault, out Target.WorldPriorityDefault, 0, 256, "WorldSound Priority");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            FloatSlider(Target.WorldVolumeDefault, out Target.WorldVolumeDefault, 0f, 1f, "WorldSound Volume");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            IntSlider(Target.OneShootPriorityDefault, out Target.OneShootPriorityDefault, 0, 256, "OneShoot Priority");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            FloatSlider(Target.OneShootVolumeDefault, out Target.OneShootVolumeDefault, 0f, 1f, "OneShoot Volume");
            GUILayout.EndHorizontal();

            GUI.enabled = true;
        }
        protected override void OnInspectorRuntimeGUI()
        {
            base.OnInspectorRuntimeGUI();

            GUILayout.BeginHorizontal();
            Target.Mute = EditorGUILayout.Toggle("Mute", Target.Mute);
            GUILayout.EndHorizontal();

            #region Background Audio
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            _backgroundAudioFoldout = EditorGUILayout.Foldout(_backgroundAudioFoldout, "Background Audio", true);
            GUILayout.EndHorizontal();

            if (_backgroundAudioFoldout)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                GUI.enabled = Target.BackgroundMusicClip;
                if (GUILayout.Button("Play", EditorStyles.miniButtonLeft))
                {
                    Target.PlayBackgroundMusic(Target.BackgroundMusicClip);
                }
                if (GUILayout.Button("Pause", EditorStyles.miniButtonMid))
                {
                    Target.PauseBackgroundMusic();
                }
                if (GUILayout.Button("Resume", EditorStyles.miniButtonMid))
                {
                    Target.ResumeBackgroundMusic();
                }
                if (GUILayout.Button("Stop", EditorStyles.miniButtonRight))
                {
                    Target.StopBackgroundMusic();
                }
                GUI.enabled = true;
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                GUI.enabled = false;
                EditorGUILayout.ObjectField("Clip:", Target.BackgroundMusicClip, typeof(AudioClip), true);
                GUI.enabled = true;
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                Target.BackgroundVolume = EditorGUILayout.Slider("Volume: ", Target.BackgroundVolume, 0f, 1f);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                Target.BackgroundPriority = EditorGUILayout.IntSlider("Priority: ", Target.BackgroundPriority, 0, 256);
                GUILayout.EndHorizontal();
            }
            #endregion

            #region Single Audio
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            _singleAudioFoldout = EditorGUILayout.Foldout(_singleAudioFoldout, "Single Audio", true);
            GUILayout.EndHorizontal();

            if (_singleAudioFoldout)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                GUI.enabled = Target.SingleSoundClip;
                if (GUILayout.Button("Play", EditorStyles.miniButtonLeft))
                {
                    Target.PlaySingleSound(Target.SingleSoundClip);
                }
                if (GUILayout.Button("Pause", EditorStyles.miniButtonMid))
                {
                    Target.PauseSingleSound();
                }
                if (GUILayout.Button("Resume", EditorStyles.miniButtonMid))
                {
                    Target.ResumeSingleSound();
                }
                if (GUILayout.Button("Stop", EditorStyles.miniButtonRight))
                {
                    Target.StopSingleSound();
                }
                GUI.enabled = true;
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                GUI.enabled = false;
                EditorGUILayout.ObjectField("Clip:", Target.SingleSoundClip, typeof(AudioClip), true);
                GUI.enabled = true;
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                Target.SingleVolume = EditorGUILayout.Slider("Volume: ", Target.SingleVolume, 0f, 1f);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                Target.SinglePriority = EditorGUILayout.IntSlider("Priority: ", Target.SinglePriority, 0, 256);
                GUILayout.EndHorizontal();
            }
            #endregion

            #region Multiple Audio
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            _multipleAudioFoldout = EditorGUILayout.Foldout(_multipleAudioFoldout, "Multiple Audio", true);
            GUILayout.EndHorizontal();

            if (_multipleAudioFoldout)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                if (GUILayout.Button("Clear", EditorStyles.miniButtonLeft))
                {
                    Target.ClearIdleMultipleAudioSource();
                }
                if (GUILayout.Button("Stop All", EditorStyles.miniButtonRight))
                {
                    Target.StopAllMultipleSound();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                Target.MultipleVolume = EditorGUILayout.Slider("Volume: ", Target.MultipleVolume, 0f, 1f);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                Target.MultiplePriority = EditorGUILayout.IntSlider("Priority: ", Target.MultiplePriority, 0, 256);
                GUILayout.EndHorizontal();
            }
            #endregion

            #region World Audio
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            _worldAudioFoldout = EditorGUILayout.Foldout(_worldAudioFoldout, "World Audio", true);
            GUILayout.EndHorizontal();

            if (_worldAudioFoldout)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                if (GUILayout.Button("Clear", EditorStyles.miniButtonLeft))
                {
                    Target.ClearIdleWorldAudioSource();
                }
                if (GUILayout.Button("Stop All", EditorStyles.miniButtonRight))
                {
                    Target.StopAllWorldSound();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                Target.WorldVolume = EditorGUILayout.Slider("Volume: ", Target.WorldVolume, 0f, 1f);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                Target.WorldPriority = EditorGUILayout.IntSlider("Priority: ", Target.WorldPriority, 0, 256);
                GUILayout.EndHorizontal();
            }
            #endregion

            #region OneShoot Audio
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            _oneShootAudioFoldout = EditorGUILayout.Foldout(_oneShootAudioFoldout, "OneShoot Audio", true);
            GUILayout.EndHorizontal();

            if (_oneShootAudioFoldout)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                Target.OneShootVolume = EditorGUILayout.Slider("Volume: ", Target.OneShootVolume, 0f, 1f);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                Target.OneShootPriority = EditorGUILayout.IntSlider("Priority: ", Target.OneShootPriority, 0, 256);
                GUILayout.EndHorizontal();
            }
            #endregion
        }
    }
}