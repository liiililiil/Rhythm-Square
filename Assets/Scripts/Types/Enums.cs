using System;

namespace Type.Enums.Addressable
{
    // 스프라이트 목록
    public enum SpriteIndex
    {
        // 메인메뉴용
        Diamond = 101,
        EmptyDiamond = 102,
        Square = 103,
        ModernArrows = 104,
        PlayerIcon = 105,
        SingleModernArrow = 106,
        Star = 107,

        // 노래 선택시
        TwoLine = 108,
        TwoLineEnd = 109,



    }
    // 텍스트 목록
    public enum TextIndex
    {
        //버튼 텍스트
        ToMenu = 1101,
        ToSelect = 1102,
        ToSetting = 1103,
        ToExit = 1104,
        SFX = 1105,
        Music = 1106,
        Offset = 1107,

        //MainMenu 텍스트
        ExitWarning = 1201,
        Language = 1202,

        //오디오 정보
    }

    // 노래 목록
    public enum MusicIndex
    {
        // 배경용
        iluvslapbass = 101,

        // 플레이용
        MachRoger = 201,
        ZidandaStep = 202,
    }


    // 프리팹 목록
    public enum PrefabIndex
    {
        MainMenu = 10001,
        Setting = 10002,
        ExitWarning = 10003,

    }

}

namespace Type.Enums.Settings
{
    [Serializable]
    public enum ConfigType : byte
    {
        Music,
        SFX,
        Offset,
        Language
    }
}

namespace Type.Enums.Menu
{
    // 메뉴 상태들
    public enum MenuState : byte
    {
        //초기 설정 로딩
        InitLoading = 0,
        InitWaitng = 1,

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
        Credits = 8,

        // 인게임
        Ingame = 9,

        // 인게임 일시정지
        InGamePhase = 10,

        // 인게임 결과 창
        InGameResult = 11,
    }

    // 언어들
    public enum Language : byte
    {
        Empty = 0,
        Korean = 1,
        English = 2,
    }
}