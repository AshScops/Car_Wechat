using QFramework.Custom;
using System;
using UnityEngine;

namespace QFramework.Car
{
    public class ResUtil
    {
        private static ResLoader m_resLoader = ResLoader.Allocate();

        public static GameObject GenerateGO(string path, Vector3 pos)
        {
            GameObject prefab = m_resLoader.LoadSync<GameObject>(path);
            return GameObject.Instantiate(prefab, pos, Quaternion.identity);
        }

        public static GameObject GenerateGO(string path, Transform parent)
        {
            GameObject prefab = m_resLoader.LoadSync<GameObject>(path);
            return GameObject.Instantiate(prefab, parent);
        }

        public static Sprite LoadSprite(string path) 
        {
            return m_resLoader.LoadSync<Sprite>(path);
        }
        public static Texture2D LoadTexture2D(string path)
        {
            return m_resLoader.LoadSync<Texture2D>(path);
        }


        public static void LoadPrefabAsync(string path, Action<GameObject> callback = null)
        {
            NetManager.LoadAsync<GameObject>(path, (prefab) =>
            {
                if (callback != null)
                    callback(prefab);
            });
        }

        public static void GenerateGOAsync(string path, Vector3 pos, Action<GameObject> callback = null)
        {
            NetManager.LoadAsync<GameObject>(path, (prefab) =>
            {
                var go = GameObject.Instantiate(prefab, pos, Quaternion.identity);
                if (callback != null)
                    callback(go);
            });
        }

        public static void GenerateGOAsync(string path, Transform parent = null, Action<GameObject> callback = null)
        {
            NetManager.LoadAsync<GameObject>(path, (prefab) =>
            {
                var go = GameObject.Instantiate(prefab, parent);
                if (callback != null)
                    callback(go);
            });
        }

        //public static void LoadSpriteAsync(string path, Action<Sprite> callback = null)
        //{
        //    NetManager.LoadAsync<Sprite>(path, (sprite) =>
        //    {
        //        if (callback != null)
        //            callback(sprite);
        //    });
        //}


        public static void LoadTextureAsync(string path, Action<Texture2D> callback = null)
        {
            NetManager.LoadAsync<Texture2D>(path, (texture) =>
            {
                if(callback != null)
                    callback(texture);
            });
        }

        public static void LoadAssetAsync<T>(string path, Action<T> callback = null) where T : UnityEngine.Object
        {
            NetManager.LoadAsync<T>(path, (obj) =>
            {
                if (callback != null)
                    callback(obj);
            });
        }

    }


}