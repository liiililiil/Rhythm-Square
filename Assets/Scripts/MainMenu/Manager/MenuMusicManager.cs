using UnityEngine;
using System.Collections;

using AudioManagement;
using Utils;

using Type.Menu;
using Type.Addressable.Table;
using Type.Audio;

using Tables.MusicTable;

using SimpleEasing;
using SimpleActions;
using Unity.Mathematics;
using Type;

public class MenuMusicManager : Managers<MenuMusicManager>
{
    [SerializeField]
    private AudioSource audioSourceOne;

    [SerializeField]
    private AudioSource audioSourceTwo;

    private AudioSource audioSource;
    private AudioSource otherSource;

    
    //노래 부분 기록
    private FloatRange currentRange;
    private FloatRange previousRange;


    // 노래 관리용 코루틴
    private Coroutine coroutine;
    private Coroutine sourceCoroutine;

    //루프 대기용 코루틴
    private Coroutine loopCoroutine;

    // 박자 마다 실행되는 이벤트
    public SimpleEvent OnBeat = new SimpleEvent();

    //음악 정보
    private MusicInfo musicInfo;

    public float beatPerSec{get; private set;} = 1; //bpm 로드 실패시 오류 체인 방지

    //배경 정보
    public BackGroundInfo backGroundInfo {get; private set;}

    //박자 변경되면 실행되는 이벤트
    public SimpleEvent<int> OnResetBeat = new SimpleEvent<int>();

   


    // 다음 박자
    private float nextSec = 999999;
    public int beat {get; private set;}

    // 오디오 반복해야하는지
    private bool isLoop = false;

    // 오디오 소스 교체 시간
    private const float SOURCE_FADE_DURATION = 1f;


    //로딩할 노래
    private const MusicIndex TARGET_MUSIC = MusicIndex.iluvslapbass;



    private void Awake()
    {

        //기초 설정
        audioSource = audioSourceOne;
        otherSource = audioSourceTwo;

        currentRange = new FloatRange();
        previousRange = new FloatRange();

        Singleton(false);
    }

    private void Start() {
        MenuAssetLoadManager.Instance.AssetLoaderBind(MusicLoad);
        SettingManager.Instance.GetSetting().volumes[Type.Menu.AudioType.Music].onValueChanged.AddListener(VolumeUpdate);
    }

    private void MusicLoad()
    {
        // 클립 등록
        AudioClip audioClip = MusicTable.Instance.GetMusic(TARGET_MUSIC).audioClip;
        audioSourceOne.clip = audioClip;
        audioSourceTwo.clip = audioClip;

        audioSourceOne.volume = 0;
        audioSourceTwo.volume = 0;

        audioSource.Play();

        //정보 등록
        musicInfo = MusicTable.Instance.GetMusicInfo(TARGET_MUSIC);
        backGroundInfo = MusicTable.Instance.GetBackGroundInfo(TARGET_MUSIC);

        //bpm 등록
        beatPerSec = 60 / musicInfo.bpm;
    }

    private void OnEnable()
    {
        MenuStateManager.Instance.onMenuStateChanged.AddListener(OnMenuStateChanged);
    }

    private void Update()
    {
        AudioLoop();
        BeatDetection();

    }

    private void VolumeUpdate(float value)
    {   
        //노래 변경 중이라면 무시
        if(sourceCoroutine != null) return;

        audioSource.volume = value;
    }
    private void VolumeUpdate()
    {
        VolumeUpdate(SettingManager.Instance.GetSetting().GetMatchedAudio(Type.Menu.AudioType.Music));
    }


    /// <summary>
    /// 업데이트에서 사용되는 함수들
    /// </summary>
    #region Update

    private void AudioLoop()
    {

        // 반복 설정 안하면 넘기기
        if(!isLoop) return;
        
        // 첫 변경시 Previous가 Null이 뜨므로 예외 처리 하기
        try
        {
            if(audioSource.time >= currentRange.end)
            {
                
                audioSource.time -= currentRange.end - currentRange.start;
                BeatTimeCorrection(audioSource.time);
            }
        } catch
        {
            return;
        }
    }

    // 비트 감지
    private void BeatDetection()
    {
        if(audioSource.isPlaying && audioSource.time >= nextSec)
        {
            OnBeat.Invoke();
            beat++;
            nextSec +=  beatPerSec;
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

            audioSource.volume = Mathf.Lerp(0, SettingManager.Instance.GetSetting().GetMatchedAudio(Type.Menu.AudioType.Music), t);
            otherSource.volume = Mathf.Lerp(otherVolume, 0, t);

            yield return null;
        }

        //전환 완료되면 보정 및 끄기
        audioSource.volume = SettingManager.Instance.GetSetting().GetMatchedAudio(Type.Menu.AudioType.Music);
        otherSource.Stop();

        this.SafeStopCoroutine(ref sourceCoroutine);
    }
    
    // 음악 시간 변경시 그 시간에 박자와 다음 박자를 계산
    private void BeatTimeCorrection(float time)
    {

        beat = (int)(time / beatPerSec);
        nextSec = (beat+1) * beatPerSec;
    }
    private void OnMenuStateChanged(MenuState newState)
    {
        

        switch(newState)
        {
            case MenuState.InitLoading:
                break;

            case MenuState.InitWaitng:
                StartToRange(backGroundInfo.high.startAt);
                break;

            case MenuState.Main:
                LoopToRange(backGroundInfo.high.loop);
                break;

            case MenuState.Setting:
                LoopToRange(backGroundInfo.middle.loop);
                break;

            case MenuState.Credits:
                LoopToRange(backGroundInfo.low.loop);
                break;

            case MenuState.ExitWarning:
                LoopToRange(backGroundInfo.low.loop);
                break;

            case MenuState.ExitWating:
                StartToRange(backGroundInfo.low.endAt);
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

    private void StartToRange(FloatRange range, FloatRange nextLoop = null)
    {
        isLoop = false;
        RangeChange(range);

        audioSource.time = range.start;

        VolumeUpdate();
        BeatTimeCorrection(range.start);

        if(nextLoop != null) this.SafeStartCoroutine(ref loopCoroutine, LoopWait(range, nextLoop));
    }

    private void LoopToRange(FloatRange range)
    {
        isLoop = true;
        SourceChange();

        RangeChange(range);

        // audio의 time이 range에 없으면 그 range안으로 오도록 조정
        if(!(otherSource.time >= range.start && otherSource.time <= range.end))
        {
            // 이동까지의 시간 계산
            float delta = currentRange.start - previousRange.start;
            float targetTime = otherSource.time+ delta;

            // Debug.Log($"delta : {delta} | targetTime : {targetTime}, | range : {range.start} | current : {otherSource.time}");


            // 적응형 오디오를 위해 이전 파츠와 이어지도록 조정
            targetTime = currentRange.start + Mathf.Repeat(targetTime - currentRange.start, currentRange.end - currentRange.start);

            audioSource.time =  targetTime;
            BeatTimeCorrection(targetTime);
        }
        else
        {
            // 이미 range 안이면 시간 바로 적용
            audioSource.time = otherSource.time;
            BeatTimeCorrection(otherSource.time);
        }


        
    }


    private void RangeChange(FloatRange floatRange)
    {
        previousRange = currentRange;
        currentRange = floatRange;
    }

    private IEnumerator LoopWait(FloatRange end, FloatRange target)
    {
        //end에 도달할때 까지 대기
        while(audioSource.time < end.end)
        {
            yield return null;
        }

        //적용
        LoopToRange(target);

        this.SafeStopCoroutine(ref coroutine);
    }


}
