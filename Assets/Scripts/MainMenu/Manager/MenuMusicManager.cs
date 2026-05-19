using UnityEngine;

using Tables.MusicTable;
using SimpleActions;
using Type.Enums.Settings;
using Type.Enums.Addressable;
using MenuMusicManagers;
using Types.Utils;


public class MenuMusicManager : Managers<MenuMusicManager>
{
    private AudioSource audioSourceOne;
    private AudioSource audioSourceTwo;


    // DI
    private MMConductor conductor;
    private MMLoop loop;
    private MMSourceManager sourceManager;
    private MMStateChanger stateChanger;


    public SimpleEvent onBeat { get => conductor.OnBeat; }
    public float beatPerSec { get => conductor.beatPerSec; }
    public int beat { get => conductor.beat; }
    public SimpleEvent onUpdate { get; private set; } = new SimpleEvent();
    public SimpleEvent<float> onTimeChange { get; private set; } = new SimpleEvent<float>();
    public SimpleEvent<MusicIndex> onClipChange { get; private set; } = new SimpleEvent<MusicIndex>();
    public SimpleEvent<MusicIndex> onMusicInfoChange { get; private set; } = new SimpleEvent<MusicIndex>();
    public SimpleEvent<AudioSource, AudioSource> onSourceChange { get; private set; } = new SimpleEvent<AudioSource, AudioSource>();

    //로딩할 노래
    private const MusicIndex TARGET_MUSIC = MusicIndex.iluvslapbass;
    private void Awake()
    {
        Singleton(false);

        audioSourceOne = gameObject.AddComponent<AudioSource>();
        audioSourceTwo = gameObject.AddComponent<AudioSource>();

        conductor = GetComponent<MMConductor>();
        conductor.Bind(this);

        loop = GetComponent<MMLoop>();
        loop.Bind(this);

        sourceManager = GetComponent<MMSourceManager>();
        sourceManager.Bind(this);

        stateChanger = GetComponent<MMStateChanger>();
        stateChanger.Bind(this);

        onSourceChange.Invoke(audioSourceOne, audioSourceTwo);
    }
    private void Start()
    {
        MenuAssetLoadManager.Instance.onMainMenuAssetLoaded.AddListener(MusicInit);
        SettingManager.Instance.GetConfig<float>(ConfigType.Music).OnChangeConfig.AddListener(sourceManager.SetVolume);
    }
    private void MusicInit()
    {
        MusicLoad(TARGET_MUSIC);
    }
    private void MusicLoad(MusicIndex musicIndex, bool isloadbackgroundInfo = true)
    {
        // 클립 등록
        AudioClip audioClip = MusicTable.Instance.GetMusic(musicIndex).audioClip;
        audioSourceOne.clip = audioClip;
        audioSourceTwo.clip = audioClip;
        audioSourceOne.volume = 0;
        audioSourceTwo.volume = 0;
        sourceManager.Play();

        if (isloadbackgroundInfo)
        {
            onMusicInfoChange.Invoke(musicIndex);
        }

        onClipChange.Invoke(musicIndex);
        onTimeChange.Invoke(0);


    }
    private void Update()
    {
        onUpdate.Invoke();
    }

    public void ClipChange(MusicIndex musicIndex, bool isloadbackgroundInfo = true) => MusicLoad(musicIndex, isloadbackgroundInfo);
    public void SourceChanged() => sourceManager.AudioSourceChange();
    public void RangeLoop(FloatRange range, bool isSmoothLoop = false, bool adaptiveloop = true, bool isGoingToPart = true) => loop.RangeLoop(range, isSmoothLoop, adaptiveloop, isGoingToPart);
    public void RangeStart(FloatRange range, FloatRange nextLoop = null, bool isGoingToPart = true) => loop.RangeStart(range, nextLoop, isGoingToPart);
    public void Stop() => sourceManager.Stop();
    public void SetVolume(float value) => sourceManager.SetVolume(value);
}
