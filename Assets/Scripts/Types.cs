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

    // 설정
    public class Settings
    {
        public float musicVolume;
        public float sfxVolume;
    }


    // 실수 범위
    [Serializable]
    public struct FloatRange
    {
        public float start;
        public float end;
        
    }




}