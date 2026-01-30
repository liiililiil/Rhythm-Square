using SimpleActions;
using Types;
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
        ChangeMenuState(MenuState.Main);   
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
}
