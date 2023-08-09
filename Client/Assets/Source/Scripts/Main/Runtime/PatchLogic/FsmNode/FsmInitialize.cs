using System;
using System.IO;
using HT.Framework;
using UnityEngine;
using YooAsset;
using ZEngine.Utility.State;


/// <summary>
/// 初始化资源包
/// </summary>
internal sealed class FsmInitialize : StateBase
{
	
	public  override void OnEnter()
	{
		PatchManager.Listener.OnPatchStatesChange("初始化资源包！");
		InitPackage().Forget();
	}
	
	private async UniTaskVoid InitPackage()
	{
		await UniTask.Delay(TimeSpan.FromSeconds(1));

		// 创建默认的资源包
		const string packageName = "DefaultPackage";
		var package = YooAssets.TryGetAssetsPackage(packageName);
		if (package == null)
		{
			package = YooAssets.CreateAssetsPackage(packageName);
			YooAssets.SetDefaultAssetsPackage(package);
		}

		// 编辑器下的模拟模式
		InitializationOperation initializationOperation = null;
		switch (Main.m_Resource.LoadMode)
		{
			case EPlayMode.EditorSimulateMode:
			{
				var createParameters = new EditorSimulateModeParameters
				{
					SimulatePatchManifestPath = EditorSimulateModeHelper.SimulateBuild(packageName)
				};
				initializationOperation = package.InitializeAsync(createParameters);
				break;
			}
			// 单机运行模式
			case EPlayMode.OfflinePlayMode:
			{
				var createParameters = new OfflinePlayModeParameters
				{
					DecryptionServices = new GameDecryptionServices()
				};
				initializationOperation = package.InitializeAsync(createParameters);
				break;
			}
			// 联机运行模式
			case EPlayMode.HostPlayMode:
			{
				var createParameters = new HostPlayModeParameters
				{
					DecryptionServices = new GameDecryptionServices(),
					QueryServices = new GameQueryServices(),
					DefaultHostServer = GetHostServerURL(),
					FallbackHostServer = GetHostServerURL()
				};
				initializationOperation = package.InitializeAsync(createParameters);
				break;
			}
		}

		await initializationOperation.ToUniTask();
		if (package.InitializeStatus == EOperationStatus.Succeed)
		{
			_machine.ChangeState<FsmUpdateVersion>();
		}
		else
		{
			Debug.LogWarning($"{initializationOperation.Error}");
			PatchManager.Listener.OnInitializeFailed();
		}
	}

	/// <summary>
	/// 获取资源服务器地址
	/// </summary>
	private string GetHostServerURL()
	{
		//string hostServerIP = "http://10.0.2.2"; //安卓模拟器地址
		const string hostServerIP = "http://127.0.0.1";
		const string gameVersion = "v1.0";

#if UNITY_EDITOR
		if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
			return $"{hostServerIP}/CDN/Android/{gameVersion}";
		else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
			return $"{hostServerIP}/CDN/IPhone/{gameVersion}";
		else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
			return $"{hostServerIP}/CDN/WebGL/{gameVersion}";
		else
			return $"{hostServerIP}/CDN/PC/{gameVersion}";
#else
		if (Application.platform == RuntimePlatform.Android)
			return $"{hostServerIP}/CDN/Android/{gameVersion}";
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
			return $"{hostServerIP}/CDN/IPhone/{gameVersion}";
		else if (Application.platform == RuntimePlatform.WebGLPlayer)
			return $"{hostServerIP}/CDN/WebGL/{gameVersion}";
		else
			return $"{hostServerIP}/CDN/PC/{gameVersion}";
#endif
	}

	/// <summary>
	/// 内置文件查询服务类
	/// </summary>
	private class GameQueryServices : IQueryServices
	{
		public bool QueryStreamingAssets(string fileName)
		{
			// 注意：使用了BetterStreamingAssets插件，使用前需要初始化该插件！
			var buildingFolderName = YooAssets.GetStreamingAssetBuildinFolderName();
			return BetterStreamingAssets.FileExists($"{buildingFolderName}/{fileName}");
		}
	}

	/// <summary>
	/// 资源文件解密服务类
	/// </summary>
	private class GameDecryptionServices : IDecryptionServices
	{
		public ulong LoadFromFileOffset(DecryptFileInfo fileInfo)
		{
			return 32;
		}

		public byte[] LoadFromMemory(DecryptFileInfo fileInfo)
		{
			throw new NotImplementedException();
		}

		public FileStream LoadFromStream(DecryptFileInfo fileInfo)
		{
			var bundleStream = new BundleStream(fileInfo.FilePath, FileMode.Open);
			return bundleStream;
		}

		public uint GetManagedReadBufferSize()
		{
			return 1024;
		}
	}
}