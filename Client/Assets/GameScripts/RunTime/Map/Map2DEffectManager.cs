using System.Collections.Generic;
using UnityEngine;

namespace GameScripts.RunTime.Map
{
	/// <summary>
	/// 2d地图特效管理
	/// </summary>
	public class Map2DEffectManager
	{
		public static readonly int FG_Z = 0;
		public static readonly int BG_Z = 90;
		public static readonly int TG_Z = 90;

		private int mapId;
		private GameObject mapRootGo;
		private GameObject mapEffectGo;
		private GameObject bgEffectGo;
		private GameObject fgEffectGo;
		private GameObject tgEffectGo;

		private List<Map2DEffect> bgList;
		private List<Map2DEffect> fgList;
		private List<Map2DEffect> tgList;


		public Map2DEffectManager(int mapId, GameObject mapRootGo)
		{
			this.mapId = mapId;
			this.mapRootGo = mapRootGo;
			InitMapEffectRoot();
			bgList = new List<Map2DEffect>();
			fgList = new List<Map2DEffect>();
			tgList = new List<Map2DEffect>();

		}

		public void UpdateEffect(Bounds cameraBounds)
		{
			for (int i = 0; i < bgList.Count; i++)
			{
				bgList[i].CheckShow(cameraBounds);
			}

			for (int i = 0; i < fgList.Count; i++)
			{
				fgList[i].CheckShow(cameraBounds);
			}

			for (int i = 0; i < tgList.Count; i++)
			{
				tgList[i].CheckShow(cameraBounds);
			}
		}

		private void InitMapEffectRoot()
		{
			mapEffectGo = new GameObject("MapEffect" + mapId);
			bgEffectGo = new GameObject("bg");
			fgEffectGo = new GameObject("fg");
			tgEffectGo = new GameObject("tg");
			bgEffectGo.transform.parent = mapEffectGo.transform;
			bgEffectGo.transform.position = new Vector3(0, 0, BG_Z);
			bgEffectGo.transform.localScale = Vector3.one;
			fgEffectGo.transform.parent = mapEffectGo.transform;
			fgEffectGo.transform.position = new Vector3(0, 0, FG_Z);
			tgEffectGo.transform.localScale = Vector3.one;
			tgEffectGo.transform.parent = mapEffectGo.transform;
			tgEffectGo.transform.position = new Vector3(0, 0, TG_Z);
			tgEffectGo.transform.localScale = Vector3.one;
		}

		public void LoadBgEffect(List<GridMapEffectData> list)
		{

			for (int i = 0; i < list.Count; i++)
			{
				bgList.Add(new Map2DEffect(bgEffectGo, list[i]));
			}
		}

		public void LoadFgEffect(List<GridMapEffectData> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				fgList.Add(new Map2DEffect(fgEffectGo, list[i]));
			}
		}

		public void LoadTgEffect(List<GridMapEffectData> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				tgList.Add(new Map2DEffect(tgEffectGo, list[i]));
			}
		}

		public void Release()
		{
			for (int i = 0; i < bgList.Count; i++)
			{
				bgList[i].Release();
			}

			for (int i = 0; i < fgList.Count; i++)
			{
				fgList[i].Release();
			}

			for (int i = 0; i < tgList.Count; i++)
			{
				tgList[i].Release();
			}

			if (mapEffectGo != null)
			{
				GameObject.Destroy(mapEffectGo);
			}

			mapRootGo = null;
			bgEffectGo = null;
			fgEffectGo = null;
			tgEffectGo = null;
		}

		public void SetMapEffectGoActive(bool show)
		{
			if (mapEffectGo != null)
			{
				mapEffectGo.SetActive(show);
			}
		}

		public void SetMapEffectNodeActive(bool active)
		{
			if (bgEffectGo != null)
			{
				bgEffectGo.SetActive(active);
			}

			if (fgEffectGo != null)
			{
				fgEffectGo.SetActive(active);
			}

		}
	}
}