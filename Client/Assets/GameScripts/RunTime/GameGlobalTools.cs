using Cysharp.Threading.Tasks;
using HT.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.RunTime
{
    public static class GameGlobalTools
    {
        
        public static UniTask<Sprite> LoadDynamicImage(string folder, int id)
        {
            var location = $"{folder}_{id.ToString()}";
            return Main.m_Resource.LoadAsset<Sprite>(location);
        }

        /// <summary>
        /// 加载动态图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="folder"></param>
        /// <param name="id"></param>
        /// <param name="isSetNativeSize"></param>
        public static void LoadDynamicImage(this Image image, string folder, int id, bool isSetNativeSize = true)
        {
            if (image is null)
            {
                Log.Error("Image 为空");
                return;
            }
            
            LoadDynamicImage(folder, id).ContinueWith((sprite) =>
            {
                image.sprite = sprite;
                if (isSetNativeSize)
                {
                    image.SetNativeSize();
                }
            }).Forget();
        }
    }
}