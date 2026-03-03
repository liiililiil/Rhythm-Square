using SimpleActions;
using Types.Menu;
using UnityEditor;
using UnityEngine;



public class MenuStateManager : Managers<MenuStateManager>
{
    public SimpleEvent<MenuState> onMenuStateChanged = new SimpleEvent<MenuState>();

    private MenuState currentMenuState;

    private void Awake() {
        Singleton(false);
    }

    private void Start() 
    {
        onMenuStateChanged.AddListener(OnChangeMenuState);
        AssetLoadManager.Instance.OnMainMenuAssetLoaded.AddListener(() => ChangeMenuState(MenuState.InitWaitng));
        
        ChangeMenuState(MenuState.InitLoading);
    }
    
    //메뉴 변경(버튼 용)
    public void ChangeMenuState(int newState)
    {
        //메뉴 상태 변경 처리
        ChangeMenuState((MenuState)newState);
    }

    //메뉴 변경
    public void ChangeMenuState(MenuState newState)
    {
        //같은 메뉴면 무시
        if(currentMenuState == newState) return;
        currentMenuState = newState;

        //메뉴 상태 변경 처리
        onMenuStateChanged.Invoke(newState);
    }   

    //wait 변경을 위한 함수
    private void OnChangeMenuState(MenuState menuState)
    {
        switch (menuState)
        {
            case MenuState.InitWaitng:
                Invoke(nameof(ChangeToMainMenu), 2.1f);
                break;
            case MenuState.ExitWating:
                Invoke(nameof(Exit), 2.1f);
                break;
        }
    }

    private void ChangeToMainMenu()
    {
        ChangeMenuState(MenuState.Main);
    }

    private void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false; // 에디터 플레이 종료
        #else
            Application.Quit(); // 실제 빌드 종료
        #endif
    }
}
