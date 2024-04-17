using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Song{ //인게임 곡 정보란
    public AudioClip Music;
    public Sprite TitleSprite;
    public float Bpm; 
    public float Offset;
    public float PreViewStart; //미리듣기 시작
    public float PreViewEnd; //미리듣기 종료
    public float Difficulty; //난이도
    public string Artist; //작곡가
    public string Title; //곡제목
    public UnityEngine.Vector2 LockSize;
    public UnityEngine.Vector2 UnLockSize;
    public bool IsLock; //시작상태
}




public class SoundMaster : MonoBehaviour
{
    public float ChangeSpeed;
    public Song[] Song;
    public Slider[] Slider = new Slider[2];
    public ButtonMaster ButtonMaster;
    public AudioClip[] BackGround,
            EffectSound,
            PlayerSound;
    public float[] SpectrumData = new float[1024];
    public AudioSource[] AudioSource; //사운드 집합
    /*
    0 : 노래
    1 : Ui효과음
    2 : 플레이어 스킬 효과음
    3 : 플레이어 피격 효과음
    */

    public int NowPlaying; //재생중인 곡
    public int InGamePlaying;
    public double PlayingTime; //재생시간
    private bool IsChange;
    private bool IsInGame;
    private bool IsBackGronud;
    public bool IsRightSong; //노래의 비정상 재생 감지
    private int PlayingRecord;
    private Coroutine PlayCoroutine; //시작코루틴 저장용
    private Coroutine EndCoroutine; //종료 코루틴 종료용

    public void Awake(){
        InGamePlaying =-1;
        IsBackGronud = false;
        IsInGame = false;
        PlayingRecord = -1;
        ButtonMaster = GetComponent<ButtonMaster>();
        AudioSource = GetComponents<AudioSource>();

    }

    public void Update() {
        AudioSource[0].GetSpectrumData(SpectrumData, 0, FFTWindow.Blackman);
        PlayingTime = MathF.Floor(AudioSource[0].time * 100) /  100;


        if(!IsChange) //사운드볼륨 설정 적용
            AudioSource[0].volume = Slider[0].value;
        
        for(int i = 1; i<=3; i++)
            AudioSource[i].volume = Slider[1].value;


        if((ButtonMaster.Mode == 0  || ButtonMaster.Mode == 1  || ButtonMaster.Mode == 2) && !IsBackGronud){ // 첫 배경 변경
            BackGroundRandom();
            IsBackGronud =true;

        }else if(ButtonMaster.Mode == 3 && IsBackGronud){
            IsBackGronud =false;
        }

        if(ButtonMaster.Mode == 3 && !IsInGame){ //첫 곡선택 변경
            SlowChange(Song[ButtonMaster.InGameSelect].Music,Song[ButtonMaster.InGameSelect].PreViewStart);
            IsInGame = true;

        }else if((ButtonMaster.Mode == 0  || ButtonMaster.Mode == 1  || ButtonMaster.Mode == 2) && IsInGame){
            IsInGame = false;
        }

        if(float.IsNaN(AudioSource[0].time)&&(ButtonMaster.Mode == 0  || ButtonMaster.Mode == 1  || ButtonMaster.Mode == 2 ) && AudioSource[0].time >= AudioSource[0].clip.length-1){//노래가 끝나면 노래변경
            AudioSource[0].Stop();
            BackGroundRandom();
        }

        //노래가 정상적으로 플레이중인지 감지
        IsRightSong = false;
    }

    public void PlayEffect(int i){
        AudioSource[1].clip = EffectSound[i];
        AudioSource[1].Play();
    }
    public void PlayShiftSound(bool IsDown){
        if(IsDown){
            AudioSource[2].clip = PlayerSound[0];
        }else{
            AudioSource[2].clip = PlayerSound[1];
        }
        AudioSource[2].Play();
    }
    public void BackGroundRandom(){
        NowPlaying = UnityEngine.Random.Range(0,BackGround.Length);

            while(NowPlaying == PlayingRecord){
                NowPlaying = UnityEngine.Random.Range(0,BackGround.Length);
            }

            PlayingRecord = NowPlaying;

            SlowChange(BackGround[NowPlaying]);
    }
    public void SlowChange(AudioClip AudioClip){
        if(PlayCoroutine != null) //처음바꿀때 오류방지
            StopCoroutine(PlayCoroutine);
        if(EndCoroutine != null)
            StopCoroutine(EndCoroutine);

        IsChange = false;
        EndCoroutine = StartCoroutine(SlowChangeStopCr(AudioClip));
    }

    IEnumerator SlowChangeStopCr(AudioClip AudioClip){
        IsChange = true;

        while(AudioSource[0].volume > 0){
            if(!IsChange) break;
            yield return null;
            AudioSource[0].volume -= ChangeSpeed;
        }

        AudioSource[0].clip = AudioClip;
        AudioSource[0].Stop();
        IsChange = false;

        yield return new WaitForSeconds(0.5f);
        PlayCoroutine = StartCoroutine(SlowChangeStartCr(AudioClip));

    }

    IEnumerator SlowChangeStartCr(AudioClip AudioClip){
        IsChange = true;

        AudioSource[0].volume =0;
        AudioSource[0].clip = AudioClip;
        AudioSource[0].Play();

        while(AudioSource[0].volume < Slider[0].value){
            if(!IsChange) break;
            yield return null;
            AudioSource[0].volume +=ChangeSpeed;
        }

        IsChange = false;


    }

    public void SlowChange(AudioClip AudioClip,float Time){//중간 시작 오버로딩
        if(PlayCoroutine != null) //처음바꿀때 오류방지
            StopCoroutine(PlayCoroutine);
        if(EndCoroutine != null)
            StopCoroutine(EndCoroutine);

        IsChange = false;
        EndCoroutine = StartCoroutine(SlowChangeStopCr(AudioClip, Time));
    }
    
    IEnumerator SlowChangeStopCr(AudioClip AudioClip,float Time){//중간 시작 오버로딩
        IsChange = true;

        while(AudioSource[0].volume > 0){
            if(!IsChange) break;
            yield return null;
            AudioSource[0].volume -= ChangeSpeed;
        }

        AudioSource[0].clip = AudioClip;
        AudioSource[0].Stop();
        IsChange = false;

        yield return new WaitForSeconds(0.5f);
        PlayCoroutine = StartCoroutine(SlowChangeStartCr(AudioClip, Time));

    }

    IEnumerator SlowChangeStartCr(AudioClip AudioClip,float Time){ //중간 시작 오버로딩
        IsChange = true;

        AudioSource[0].volume =0;
        AudioSource[0].clip = AudioClip;
        AudioSource[0].Play();
        AudioSource[0].time = Time;

        while(AudioSource[0].volume < Slider[0].value){
            if(!IsChange) break;
            yield return null;
            AudioSource[0].volume +=ChangeSpeed;
        }

        IsChange = false;

    }

    // IEnumerable SlowBackGroundPlayCr(int i){
    //     AudioSource[0].clip = BackGround[i];
    //     yield return new Null();
    // }

}
