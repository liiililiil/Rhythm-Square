using UnityEngine;
using System.Collections;

using AudioManagement;
using Utils;
using Types.Menu;
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


    // 노래 관리용 코루틴
    private Coroutine coroutine;
    private Coroutine sourceCoroutine;

    // 박자 마다 실행되는 이벤트
    public SimpleEvent OnBeat = new SimpleEvent();

    //박자 변경되면 실행되는 이벤트
    public SimpleEvent<int> OnResetBeat = new SimpleEvent<int>();

    // 다음 박자
    [SerializeField]
    private float nextSec;
    public int beat {get; private set;}

    // 오디오 반복해야하는지
    private bool isLoop;

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

        AudioSetting();
        AudioLoop();
        BeatDetection();

    }

    /// <summary>
    /// 업데이트에서 사용되는 함수들
    /// </summary>
    #region Update
    private void AudioSetting()
    {   
        //노래 변경 중이라면 무시
        if(sourceCoroutine != null) return;

        audioSource.volume = SettingManager.Instance.setting.volumes.GetMatchedAudio(Types.Menu.AudioType.Music);
    }

    private void AudioLoop()
    {

        // 반복 설정 안하면 넘기기
        if(!isLoop) return;
        
        // 첫 변경시 Previous가 Null이 뜨므로 예외 처리 하기
        try
        {
            if(audioSource.time >= currentPart.loop.end)
            {
                audioSource.time -= currentPart.loop.end - currentPart.loop.start;
                BeatTimeCorrection();
            }
        } catch
        {
            return;
        }
    }

    // 비트 감지
    private void BeatDetection()
    {
        if(audioSource.time >= nextSec)
        {
            OnBeat.Invoke();
            beat++;
            nextSec +=  Temps.BPM_TO_SEC;
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
#endregion
    
    
    private IEnumerator SourceSlowChange()
    {
        // 너무 빠른 변경으로 인한 버그 방지
        float elapsed = 0;
        float otherVolume = otherSource.volume;

        //오디오 천천히 전환하기
        while(elapsed <= SOURCE_FADE_DURATION)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / SOURCE_FADE_DURATION);

            t = Ease.Easing(t, EaseType.Linear);

            audioSource.volume = Mathf.Lerp(0, SettingManager.Instance.setting.volumes.GetMatchedAudio(Types.Menu.AudioType.Music), t);
            otherSource.volume = Mathf.Lerp(otherVolume, 0, t);

            yield return null;
        }

        //전환 완료되면 보정 및 끄기
        audioSource.volume = SettingManager.Instance.setting.volumes.GetMatchedAudio(Types.Menu.AudioType.Music);
        otherSource.Stop();

        this.SafeStopCoroutine(ref sourceCoroutine);
    }
    
    // 음악 시간 변경시 그 시간에 박자와 다음 박자를 계산
    private void BeatTimeCorrection(){
        nextSec = Mathf.Floor(audioSource.time / Temps.BPM_TO_SEC + 1f) * Temps.BPM_TO_SEC;
        beat = (int)(audioSource.time / Temps.BPM_TO_SEC);
    }

    private void OnMenuStateChanged(MenuState newState)
    {
        

        switch(newState)
        {
            case MenuState.Main:
                GoToPart(main);
                break;

            case MenuState.Setting:
                GoToPart(setting);
                break;

            case MenuState.Credits:
                GoToPart(credits);
                break;

            case MenuState.ExitWarning:
                GoToPart(exitWarning);
                break;

            case MenuState.ExitWating:
                EndToPart(exitWarning);
                break;

            default:
                MusicStop();
                break;
            
        }

    }

    private void MusicStop()
    {
        SourceChange();
        audioSource.Stop();

    }

    private void StartToPart(MusicPart musicPart)
    {
        isLoop = true;
        MusicPartChange(musicPart);

        audioSource.time = musicPart.startAt.start;
        BeatTimeCorrection();
    }

    private void EndToPart(MusicPart musicPart)
    {
        isLoop = false;
        MusicPartChange(musicPart);

        audioSource.time = musicPart.endAt.start;
        BeatTimeCorrection();
    }

    private void GoToPart(MusicPart newPart)
    {
        SourceChange();

        isLoop = true;
        MusicPartChange(newPart);

        // 이동까지의 시간 계산
        float delta = currentPart.loop.start - previousPart.loop.start;
        float targetTime = otherSource.time+ delta;

        // 적응형 오디오를 위해 이전 파츠와 이어지도록 조정
        targetTime = currentPart.loop.start + Mathf.Repeat(targetTime - currentPart.loop.start, currentPart.loop.end - currentPart.loop.start);

        audioSource.time =  targetTime;
        BeatTimeCorrection();
    }


    private void MusicPartChange(MusicPart newPart)
    {
        previousPart = currentPart;
        currentPart = newPart;
    }
}
