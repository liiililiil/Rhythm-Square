using UnityEngine;
using System.Collections;

using AudioManagement;
using Utils;
using Types;
using Easing;
public class MenuMusicManager : Managers<MenuMusicManager>
{
    [SerializeField]
    private AudioSource audioSourceOne;

    [SerializeField]
    private AudioSource audioSourceTwo;

    private bool isAudioSourceOne = false;

    private AudioSource audioSource
    {
        get => isAudioSourceOne ? audioSourceOne : audioSourceTwo;
        set
        {
            if (isAudioSourceOne)
                audioSourceOne = value;
            else
                audioSourceTwo = value;
        }
    }
    

    [Space(10), SerializeField]
    private Music menuMusic;

    [Space(10), SerializeField]
    private MusicPart main;
    [SerializeField]
    private MusicPart setting;
    [SerializeField]
    private MusicPart credits;
    [SerializeField]
    private MusicPart exitWarning;
    
    [Space(10), SerializeField]
    private MusicPart currentPart;
    [SerializeField]
    private MusicPart previousPart;

    private MenuState previousMenuState;

    private MenuState currentMenuState;

    private bool isFading = false;

    private Coroutine coroutine;
    private Coroutine sourceCoroutine;

    private const float SOURCE_FADE_DURATION = 0.5f;



    private void Awake()
    {
        Singleton(false);
    }

    private void Start()
    {
        MenuStateManager.Instance.onMenuStateChanged.AddListener(OnMenuStateChanged);
    }

    private void Update()
    {


    }

    private void AudioLoop()
    {
        try
        {
            if (!isFading)
            {
                if(audioSource.time >= currentPart.loop.end)
                {
                    audioSource.time = currentPart.loop.start;
                }
            } 
        } catch
        {
            return;
        }
    }

    private void SourceChange()
    {
        isAudioSourceOne = !isAudioSourceOne;

        audioSource.volume = 1;
        audioSource.Play();

        this.SafeStartCoroutine(ref sourceCoroutine, SourceSlowChange());


    }

    private IEnumerator SourceSlowChange()
    {

        AudioSource other = isAudioSourceOne ? audioSourceTwo : audioSourceOne;
        float elapsed = 0;
        float otherVolume = other.volume;


        while(elapsed <= SOURCE_FADE_DURATION)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / SOURCE_FADE_DURATION);

            t = Ease.Easing(EaseType.OutQuint, t);

            audioSource.volume = Mathf.Lerp(0, SettingManager.Instance.setting.musicVolume, t);
            other.volume = Mathf.Lerp(otherVolume, 0, t);

            yield return null;
        }

        audioSource.volume = SettingManager.Instance.setting.musicVolume;
        other.Stop();

        this.SafeStopCoroutine(ref sourceCoroutine);
    }


    

    private void OnMenuStateChanged(Types.MenuState newState)
    {
        SourceChange();

        previousMenuState = currentMenuState;
        currentMenuState = newState;
        
        bool isSkipFading = false;
        bool isSkipStart = false;

        // Debug.Log(previousMenuState);
        
        if(previousMenuState == MenuState.ExitWarning || currentMenuState == MenuState.ExitWarning) isSkipFading = true;

        switch(newState)
        {
            case Types.MenuState.Main:
                if(previousMenuState != MenuState.MainWaitng) isSkipStart = true;
                if(previousMenuState == MenuState.ExitWarning) isSkipStart = false;

                this.SafeStartCoroutine(ref coroutine, FadeToPart(main, isSkipStart, isSkipFading));
                break;
            case Types.MenuState.Setting:
                if(previousMenuState == MenuState.Main) isSkipFading = true;
                
                this.SafeStartCoroutine(ref coroutine, FadeToPart(setting, isSkipStart, isSkipFading));
                break;
            case Types.MenuState.Credits:
                this.SafeStartCoroutine(ref coroutine, FadeToPart(credits));
                break;
            case Types.MenuState.ExitWarning:
                this.SafeStartCoroutine(ref coroutine, FadeToPart(exitWarning, isSkipStart, isSkipFading));
                break;
            
        }
    }

    private IEnumerator FadeToPart(MusicPart newPart,bool isSkipStart = false, bool isSkipFade = false)
    {
        previousPart = currentPart;
        currentPart = newPart;

        if (!isSkipFade)
        {
            isFading = true;

            audioSource.time = previousPart.endAt.start; 
            while(audioSource.time <= previousPart.endAt.end)
            {
                yield return null;
            }

            isFading = false; 


        }

        if(!isSkipStart)
        {
            audioSource.time = currentPart.startAt.start;
        }
        else
        {
            audioSource.time = currentPart.loop.start;
        }
        

        this.SafeStopCoroutine(ref coroutine);


    }


}
