using UnityEditor;
using UnityEngine;

namespace HT.Framework
{
    [CustomEditor(typeof(ResourceManager))]
    [GiteeURL("https://gitee.com/SaiTingHu/HTFramework")]
    [GithubURL("https://github.com/SaiTingHu/HTFramework")]
    [CSDNBlogURL("https://wanderer.blog.csdn.net/article/details/88852698")]
    internal sealed class ResourceManagerInspector : InternalModuleInspector<ResourceManager, IResourceHelper>
    {
        protected override string Intro => "Resource Manager, use this to complete the loading and unloading of resources!";

        protected override void OnInspectorDefaultGUI()
        {
            base.OnInspectorDefaultGUI();

            GUI.enabled = !EditorApplication.isPlaying;

            PropertyField(nameof(ResourceManager.Mode), "Load Mode");
            PropertyField(nameof(ResourceManager.PackageName), "PackageName");
            PropertyField(nameof(ResourceManager.DefaultHostServer), "DefaultHostServer");
            PropertyField(nameof(ResourceManager.FallbackHostServer), "FallbackHostServer");
            PropertyField(nameof(ResourceManager.Manually), "手动初始化");
            GUI.enabled = true;
        }
        protected override void OnInspectorRuntimeGUI()
        {
            base.OnInspectorRuntimeGUI();

            // if (Target.Mode == ResourceLoadMode.AssetBundle)
            // {
            //     if (!Target.IsEditorMode)
            //     {
            //         GUILayout.BeginHorizontal();
            //         GUILayout.Label("Root Path: ", GUILayout.Width(LabelWidth));
            //         EditorGUILayout.TextField(_helper.AssetBundleRootPath);
            //         GUILayout.EndHorizontal();
            //
            //         GUILayout.BeginHorizontal();
            //         GUILayout.Label("Manifest: ", GUILayout.Width(LabelWidth));
            //         EditorGUILayout.ObjectField(_helper.AssetBundleManifest, typeof(AssetBundleManifest), false);
            //         GUILayout.EndHorizontal();
            //
            //         GUILayout.BeginHorizontal();
            //         GUILayout.Label("AssetBundles: ", GUILayout.Width(LabelWidth));
            //         GUILayout.Label(_helper.AssetBundles.Count.ToString());
            //         GUILayout.EndHorizontal();
            //
            //         foreach (var item in _helper.AssetBundles)
            //         {
            //             GUILayout.BeginHorizontal();
            //             GUILayout.Space(20);
            //             GUILayout.Label(item.Key, GUILayout.Width(LabelWidth - 20));
            //             EditorGUILayout.ObjectField(item.Value, typeof(AssetBundle), false);
            //             GUILayout.EndHorizontal();
            //         }
            //     }
            //     else
            //     {
            //         GUILayout.BeginHorizontal();
            //         GUILayout.Label("No Runtime Data!");
            //         GUILayout.EndHorizontal();
            //     }
            // }
            // else
            // {
            //     GUILayout.BeginHorizontal();
            //     GUILayout.Label("No Runtime Data!");
            //     GUILayout.EndHorizontal();
            // }
        }
    }
}