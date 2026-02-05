using System;

namespace Types
{
    // 메뉴 상태들
    public enum MenuState : byte
    {
        //초기 설정 로딩
        InitLoading = 0,
        MainWaitng = 1,

        //타이틀
        Main = 2,

        //노래 선택화면
        MusicSelection = 3,

        //스테이지 로딩
        MusicLoading = 4,

        //설정 화면
        Setting = 5,

        //프로그램 종료 경고
        ExitWarning = 6,

        //프로그램 종료 대기
        ExitWating = 7,

        //제작자
        Credits = 8
    }

    // 언어들
    public enum Language : byte
    {
        Korean = 0,
        English = 1,   
    }


    // 설정
    public class Setting
    {
        public float musicVolume;
        public float sfxVolume;
        public Language language;

        public Setting(float _musicVolume, float _sfxVolume, Language _langeuage)
        {
            musicVolume = _musicVolume;
            sfxVolume = _sfxVolume;
            language = _langeuage;
        }

        public Setting()
        {
            musicVolume = 0.6f;
            sfxVolume = 0.6f;
            
            //일단 보편적인 언어인 영어로 설정
            language = Language.English;
        }
    }



    // 실수 범위
    [Serializable]
    public struct FloatRange
    {
        public float start;
        public float end;
        
    }




}