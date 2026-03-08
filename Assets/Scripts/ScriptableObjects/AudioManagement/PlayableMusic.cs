using UnityEngine;
using UnityEngine.AddressableAssets;

using Type.Addressable.Table;

/// <summary>
/// 오디오 관련 스크립터블 오브젝트들을 정의합니다.
/// </summary>

namespace AudioManagement
{
    /// <summary>
    /// 게임 플레이가 가능한 음악 트랙을 나타내는 스크립터블 오브젝트입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "EmptyPlayableMusic", menuName = "AudioAssets/New Playable Music")]
    public class PlayableMusic : IndexedScriptableObject<MusicIndex>
    {
        public int difficultyLevel;
        public float previewStartTime;
        public float previewEndTime;

    }
}

