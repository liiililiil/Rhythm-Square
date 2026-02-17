using Types.Addressable.Table;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AudioManagement
{
    /// <summary>
    /// 일반적인 음악 트랙을 나타내는 스크립터블 오브젝트입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "EmptyMusic", menuName = "AudioAssets/New Music")]
    public class Music : IndexedScriptableObject<MusicIndex>
    {
        public AudioClip audioClip;
    }
}