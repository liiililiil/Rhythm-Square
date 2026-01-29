using UnityEngine;
using System.Collections;

using AudioManagement;
using Utils;
using Types;
using SimpleEasing;
using SimpleActions;

public class MenuMusicManager : Managers<MenuMusicManager>
{
    [SerializeField]
    private AudioSource audioSourceOne;

    [SerializeField]
    private AudioSource audioSourceTwo;

    private AudioSource audioSource;
    private AudioSource otherSource;
    
    
    //음악
    [Space(10), SerializeField]
    private Music menuMusic;

    
    //각 메뉴 상태별 음악 위치
    [Space(10), SerializeField]
    private MusicPart main;
    [SerializeField]
    private MusicPart setting;
    [SerializeField]
    private MusicPart credits;
    [SerializeField]
    private MusicPart exitWarning;  

    //노래 부분 기록
    private MusicPart currentPart;
    private MusicPart previousPart;

    //현재 메뉴 기록
    private MenuState previousMenuState;
    private MenuState currentMenuState;


    [SerializeField]
    private bool isFading = false;

    // 노래 관리용 코루틴
    private Coroutine coroutine;
    private Coroutine sourceCoroutine;

    // 박자 마다 실행되는 이벤트
    public SimpleEvent invokeBeat = new SimpleEvent();

    // 다음 박자
    [SerializeField]
    private float nextBeat;

    // 오디오 소스 교체 시간
    private const float SOURCE_FADE_DURATION = 1f;



    private void Awake()
    {

        //기초 설정
        audioSource = audioSourceOne;
        otherSource = audioSourceTwo;

        currentPart = main;
        previousPart = main;

        Singleton(false);
    }

    private void Start()
    {
        MenuStateManager.Instance.onMenuStateChanged.AddListener(OnMenuStateChanged);
    }

    private void Update()
    {

        AudioLoop();
        BeatDetection();

    }

    private void AudioLoop()
    {
        try
        {
            if (!isFading)
            {
                if(audioSource.time >= currentPart.loop.end)
                {
                    audioSource.time -= currentPart.loop.end - currentPart.loop.start;
                    BeatTimeCorrection();
                }
            } 
        } catch
        {
            return;
        }
    }

    private void BeatDetection()
    {
        if(audioSource.time >= nextBeat)
        {
            invokeBeat.Invoke();
            nextBeat +=  Temps.BPM_TO_SEC;
        }
    }
    private void SourceChange()
    {

        //오디오 스왑
        AudioSource temp = audioSource;
        audioSource = otherSource;
        otherSource = temp;


        audioSource.volume = 1;
        audioSource.Play();

        this.SafeStartCoroutine(ref sourceCoroutine, SourceSlowChange());


    }

    private IEnumerator SourceSlowChange()
    {

        float elapsed = 0;
        float otherVolume = otherSource.volume;


        while(elapsed <= SOURCE_FADE_DURATION)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / SOURCE_FADE_DURATION);

            t = Ease.Easing(t, EaseType.Linear);

            audioSource.volume = Mathf.Lerp(0, SettingManager.Instance.setting.musicVolume, t);
            otherSource.volume = Mathf.Lerp(otherVolume, 0, t);

            yield return null;
        }

        audioSource.volume = SettingManager.Instance.setting.musicVolume;
        otherSource.Stop();

        this.SafeStopCoroutine(ref sourceCoroutine);
    }

    private void BeatTimeCorrection(){
        nextBeat = Mathf.Floor(audioSource.time / Temps.BPM_TO_SEC + 1f) * Temps.BPM_TO_SEC;
    }



    

    private void OnMenuStateChanged(Types.MenuState newState)
    {
        SourceChange();

        previousMenuState = currentMenuState;
        currentMenuState = newState;
        
        switch(newState)
        {
            case Types.MenuState.Main:
                GoToPart(main);
                break;

            case Types.MenuState.Setting:
                GoToPart(setting);
                break;

            case Types.MenuState.Credits:
                GoToPart(credits);
                break;
            case Types.MenuState.ExitWarning:
                GoToPart(exitWarning);
                break;
            
        }

    }

        private void GoToPart(MusicPart newPart)
    {
        previousPart = currentPart;
        currentPart = newPart;

        float delta = currentPart.loop.start - previousPart.loop.start;
        float targetTime = otherSource.time+ delta;

        targetTime = currentPart.loop.start + Mathf.Repeat(targetTime - currentPart.loop.start, currentPart.loop.end - currentPart.loop.start);

        audioSource.time =  targetTime;
        BeatTimeCorrection();

        isFading = false; 
    }
}
