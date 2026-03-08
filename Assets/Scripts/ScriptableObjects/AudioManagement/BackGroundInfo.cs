using Type.Audio;
using Type.Addressable.Table;
using UnityEngine;

namespace AudioManagement
{

    /// <summary>
    /// 배경음악에서 사용되는 노래의 정보들을 담는 스크립터블 오브젝트입니다.
    /// </summary>
    
    [CreateAssetMenu(fileName = "EmptyBackGroundInfo", menuName = "AudioAssets/New BackGroundInfo")]
    public class BackGroundInfo : IndexedScriptableObject<MusicIndex>
    {
        public MusicPart low;
        public MusicPart middle;
        public MusicPart high;


        //애니메이션용
        [Space(10)]
        public int beatOffset;

        [Space(10)]

        public int arrowBeatResetCycle;
        public int playerBeatResetCycle;

        [Space(10)]

        public int arrowBeatCycle;
        public int playerBeatCycle;
    }
}