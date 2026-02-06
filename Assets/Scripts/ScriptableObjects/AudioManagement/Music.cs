using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AudioManagement
{
    /// <summary>
    /// 일반적인 음악 트랙을 나타내는 스크립터블 오브젝트입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "EmptyMusic", menuName = "AudioAssets/New Music")]
    public class Music : ScriptableObject
    {
        public AssetReferenceT<AudioClip> audioClip;
        public Sprite sprite;
        public string title;
        public string artist;
        public float bpm;

    }
}