using SimpleActions;
using Types;



public class MenuStateManager : Managers<MenuStateManager>
{
    public SimpleEvent<MenuState> onMenuStateChanged = new SimpleEvent<MenuState>();

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
        onMenuStateChanged.Invoke((MenuState)newState);
    }

    //메뉴 변경
    public void ChangeMenuState(MenuState newState)
    {
        //메뉴 상태 변경 처리
        onMenuStateChanged.Invoke(newState);
    }   
}
