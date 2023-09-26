using System;
using UnityEditor;
using UnityEngine;

namespace QFramework
{
    public class AudioKitWithResKitInit 
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            AudioKit.Config.AudioLoaderPool = new ResKitAudioLoaderPool();
        }
    }

    public class ResKitAudioLoaderPool : AbstractAudioLoaderPool
    {
        public class ResKitAudioLoader : IAudioLoader
        {
            private ResLoader mResLoader = null;

            public AudioClip Clip => mClip;
            private AudioClip mClip;

            public AudioClip LoadClip(AudioSearchKeys audioSearchKeys)
            {
                if (mResLoader == null)
                {
                    mResLoader = ResLoader.Allocate();
                }

                mClip = mResLoader.LoadSync<AudioClip>(audioSearchKeys.AssetName);

                return mClip;
            }

            //魔改
            public void LoadClipAsync(AudioSearchKeys audioSearchKeys, Action<bool, AudioClip> onLoad)
            {
                if (!AssetBundlePathHelper.SimulationMode)
                {
                    //其他平台使用QFramework原方式
                    //测试发现windows平台不能加载WebGL包中的音频，fmod的老问题了
                    if (PlatformCheck.IsWebGL)
                    {
                        QFramework.Custom.NetManager.LoadAsync<AudioClip>(audioSearchKeys.AssetName, (clip) =>
                        {
                            if (clip.LoadAudioData())
                            {
                                Debug.Log("音频已成功加载");
                            }
                            else
                            {
                                Debug.LogError("音效加载失败");
                            }
                            mClip = clip;
                            onLoad(true, clip);
                        });
                    }
                    return;
                }
#if UNITY_EDITOR
                if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.WebGL) return;
#endif

                if (mResLoader == null)
                {
                    mResLoader = ResLoader.Allocate();
                }

                mResLoader.Add2Load<AudioClip>(audioSearchKeys.AssetName, (b, res) =>
                {
                    mClip = res.Asset as AudioClip;
                    onLoad(b, res.Asset as AudioClip);
                });

                mResLoader.LoadAsync();
            }

            public void Unload()
            {
                mClip = null;
                mResLoader?.Recycle2Cache();
                mResLoader = null;
            }
        }

        protected override IAudioLoader CreateLoader()
        {
            return new ResKitAudioLoader();
        }
    }
}