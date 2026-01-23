using UnityEngine;

/// <summary>
/// 오디오 관련 스크립터블 오브젝트들을 정의합니다.
/// </summary>

namespace AudioManagement
{
    /// <summary>
    /// 일반적인 음악 트랙을 나타내는 스크립터블 오브젝트입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "EmptyMusic", menuName = "AudioAssets/New Music")]
    public class Music : ScriptableObject
    {
        public AudioClip audioClip;
        public Sprite sprite;
        public string title;
        public string artist;
        public float bpm;
    }


    /// <summary>
    /// 게임 플레이가 가능한 음악 트랙을 나타내는 스크립터블 오브젝트입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "EmptyPlayableMusic", menuName = "AudioAssets/New Playable Music")]
    public class PlayableMusic : ScriptableObject
    {
        public Music music;
        public int difficultyLevel;

        public float previewStartTime;

        public float previewEndTime;

    }

}

