using AudioManagement;
using SimpleActions;
using Tables.MusicTable;
using UnityEngine;

public class PlayableMusicSender : MonoBehaviour
{

    // 데이터가 로딩될때마다 invoke 되는 이벤트
    public SimpleEvent<PlayableMusic> onLoadPlayerableMusic = new SimpleEvent<PlayableMusic>();
    public SimpleEvent<MusicInfo> onLoadMusicInfo = new SimpleEvent<MusicInfo>();

    private void Awake()
    {
    }
    private void Start()
    {
        if (MenuAssetLoadManager.Instance.assetLoadRecoder.IsAllComplete())
        {
            OnStartLoad();
        }
        else
        {
            MenuAssetLoadManager.Instance.AssetLoaderBind(OnStartLoad);
        }
    }
    private void OnStartLoad()
    {
        // 우선 playable 음악 정보를 불러와서 각각 invoke 하기
        PlayableMusic[] playableList = MusicTable.Instance.GetPlayableMusic();

        foreach (PlayableMusic playable in playableList)
        {
            onLoadPlayerableMusic.Invoke(playable);

            // 불러온 playable의 index를 기반으로 음악 정보를 불러와서 invoke 하기
            MusicInfo musicInfo = MusicTable.Instance.GetMusicInfo(playable.index);
            onLoadMusicInfo.Invoke(musicInfo);
        }
    }



}
