
	/// <summary>
	/// 补丁监听器接口
	/// </summary>
	public interface IPatchListener
	{
		/// <summary>
		/// 当初始化失败
		/// </summary>
		void OnInitializeFailed();
		/// <summary>
		/// 当补丁流程步骤改变
		/// </summary>
		/// <param name="tips">提示</param>
		void OnPatchStatesChange(string tips);
		/// <summary>
		///  当发现更新文件
		/// </summary>
		/// <param name="totalCount">文件总数</param>
		/// <param name="totalSizeBytes">文件大小</param>
		void OnFoundUpdateFiles(int totalCount, long totalSizeBytes);
		/// <summary>
		/// 当下载进度更新
		/// </summary>
		/// <param name="totalDownloadCount">下载总数</param>
		/// <param name="currentDownloadCount">当前下载数</param>
		/// <param name="totalDownloadSizeBytes">下载大小字节数</param>
		/// <param name="currentDownloadSizeBytes">当前下载大小字节数</param>
		void OnDownloadProgressUpdate(int totalDownloadCount, int currentDownloadCount, long totalDownloadSizeBytes, long currentDownloadSizeBytes);
		/// <summary>
		/// 资源版本号更新失败
		/// </summary>
		void OnPackageVersionUpdateFailed();
		/// <summary>
		/// 补丁清单更新失败
		/// </summary>
		void OnPatchManifestUpdateFailed();
		/// <summary>
		/// 网络文件下载失败
		/// </summary>
		/// <param name="fileName">稳健命</param>
		/// <param name="error">错误</param>
		void OnWebFileDownloadFailed(string fileName, string error);
	}