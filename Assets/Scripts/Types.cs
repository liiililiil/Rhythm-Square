namespace Types
{
    // 메뉴 상태들
    public enum MenuState
    {
        //초기 설정 로딩
        InitLoading,

        //타이틀
        Main,

        //노래 선택화면
        MusicSelection,

        //설정 화면
        Setting,

        //프로그램 종료 경고
        ExitWarning,

        //제작자
        Credits
    }

    // 설정
    public struct Settings
    {
        public float musicVolume;
        public float sfxVolume;
    }

    



}