using System.Collections;
using System.Collections.Generic;
using AudioManagement;
using Type;
using UnityEngine;
using Utils;

public class MusicBarChanger : MusicSelectValueEaser
{
    [SerializeField]
    private GameObject musicBarPrefab;
    private List<MusicBar> musicBars = new List<MusicBar>();
    private const int sensitivity = 200;

    protected override void OnStart()
    {
        PlayableMusicSender.Instance.onLoadMusicInfo.AddListener(OnLoadMusicInfo);
    }

    protected override void UpdatePosition(float position)
    {
        for (int i = 0; i < musicBars.Count; i++)
        {
            musicBars[i].PositionUpdate((-position + i) * -sensitivity);
        }
    }

    private void OnLoadMusicInfo(MusicInfo musicInfo)
    {
        // 정보 받으면 그 정보대로 생성
        GameObject musicBar = Instantiate(musicBarPrefab, transform);
        musicBars.Add(musicBar.GetComponent<MusicBar>());

        // 정보 적용
        musicBar.GetComponent<MusicBarInfo>().LoadInfo(musicInfo.title, musicInfo.artist);
    }

    // 이 클래스의 활성화 여부에 따라 하위 오브젝트의 활성화도 같이 변경
    private void OnEnable()
    {
        foreach (var musicBar in musicBars)
        {
            musicBar.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (var musicBar in musicBars)
        {
            musicBar.gameObject.SetActive(false);
        }
    }
}
