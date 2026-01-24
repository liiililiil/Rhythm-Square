using UnityEngine;
using UnityEngine.AddressableAssets;
using Types;
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
        public AssetReferenceT<AudioClip> audioClip;
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
        public AssetReferenceT<Music> music;
        public int difficultyLevel;

        public float previewStartTime;

        public float previewEndTime;

    }

    /// <summary>
    /// 적응형 음악을 위한 음악 파트 정의 스크립터블 오브젝트입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "EmptyMusicPart", menuName = "AudioAssets/New Music Part")]
    public class MusicPart : ScriptableObject
    {
        public FloatRange startAt;
        public FloatRange loop;
        public FloatRange endAt;
    }

    

}

