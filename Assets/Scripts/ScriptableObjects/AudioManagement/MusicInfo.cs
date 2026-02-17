using Types.Addressable.Table;
using UnityEngine;


namespace AudioManagement
{

    /// <summary>
    /// 음악의 단순한 정보를 표기하는 스크립터블 오브젝트입니다.
    /// </summary>
    
    [CreateAssetMenu(fileName = "EmptyMusicInfo", menuName = "AudioAssets/New Music Info")]
    public class MusicInfo : IndexedScriptableObject<MusicIndex>
    {
        public string title;
        public string artist;
        public float bpm;
    }
}