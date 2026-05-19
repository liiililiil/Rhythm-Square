using System.Collections.Generic;
using AudioManagement;
using Types.Utils;
using UnityEditor;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    List<PlayableMusic> musicList = new List<PlayableMusic>();
    void Start()
    {
        PlayableMusicSender.Instance.onLoadPlayerableMusic.AddListener(OnLoadPlayableMusic);
        MusicSelectManager.Instance.onChangeIndex.AddListener(OnChangeIndex);
    }

    private void OnLoadPlayableMusic(PlayableMusic playableMusic)
    {
        musicList.Add(playableMusic);
    }


    private void OnChangeIndex(int index)
    {
        // 인덱스 변경시 그 인덱스에 대응하는 음악의 preview를 호출
        if (index < 0 || index >= musicList.Count)
            return;
        FloatRange preview = musicList[index].preview;

        MenuMusicManager.Instance.ClipChange(musicList[index].index, false);
        MenuMusicManager.Instance.RangeLoop(preview, isSmoothLoop: true, adaptiveloop: false);
    }
}
