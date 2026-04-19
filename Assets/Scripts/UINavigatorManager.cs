using System.Collections.Generic;
using Type;
using Type.Menu;
using UnityEngine.InputSystem;
using UnityEngine;
using Utils;
public class UINavigatorManager : Managers<UINavigatorManager>
{
    private IUIInteractable currentInteractable;
    private Dictionary<MenuState, IUIInteractable> defaultInteractable = new Dictionary<MenuState, IUIInteractable>();

    // 이전 x, y navigate를 저장
    private Switcher<byte> prevX = new Switcher<byte>(0);
    private Switcher<byte> prevY = new Switcher<byte>(0);

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        MenuStateManager.Instance.onMenuStateChanged.AddListener(OnChangeMenuState);
    }

    // 특정 메뉴로 진입할때의 기본 포커스 인터렉터블을 등록
    public void DefaultBind(MenuState menuState, IUIInteractable interactable)
    {
        defaultInteractable.Add(menuState, interactable);
    }

    // 메뉴 변경시 포커스를 그 메뉴의 기본 인터렉터블로 변경
    private void OnChangeMenuState(MenuState menuState)
    {
        if (defaultInteractable.ContainsKey(menuState))
            FocusChange(defaultInteractable[menuState]);

        // 없으면 Null로
        else FocusChange(null);
    }

    public void UINavigate(InputAction.CallbackContext context)
    {
        // 포커싱된 UI가 없으면 종료
        if (currentInteractable == null) return;

        Vector2Byte value = Vector2Utils.CompositeToVector2Byte(context.ReadValue<Vector2>());


        // 현재 인터렉티블이 내부에서 값 변경이 진행되지 않을 경우 포커스 변경
        if (!currentInteractable.InvokeNavigate(value))
        {
            // 각 축을 따로 처리
            if (prevX.Switch(value.x))
                FocusChange(new Vector2Byte(value.x, 0));
            if (prevY.Switch(value.y))
                FocusChange(new Vector2Byte(0, value.y));

        }
    }

    public void UISubmit(InputAction.CallbackContext context)
    {
        // 누를때만 인식되게
        if (!context.started) return;

        currentInteractable?.InvokeSubmit();
    }

    public void UICancel(InputAction.CallbackContext context)
    {
        // 누를때만 인식되게
        if (!context.started) return;

        currentInteractable?.InvokeCancel();
    }
    private void FocusChange(Vector2Byte dir)
    {
        // 포커싱된 UI가 없으면 종료
        if (currentInteractable == null) return;

        // 위치에 해당하는 오브젝트를 가져와서 넘기기
        IUIInteractable target = null;

        // 인터렉터블을 가져올 오브젝트
        GameObject targetObject = null;

        // -1를 byte(255)로 처리
        if (dir.y == 1)
            targetObject = currentInteractable.up;
        else if (dir.y == 255)
            targetObject = currentInteractable.down;
        else if (dir.x == 1)
            targetObject = currentInteractable.right;
        else if (dir.x == 255)
            targetObject = currentInteractable.left;

        if (targetObject != null)
        {
            target = targetObject.GetComponent<IUIInteractable>();
        }

        // 없으면 종료
        if (target == null) return;

        FocusChange(target);
    }

    private void FocusChange(IUIInteractable newInteractable)
    {
        // 이전 인터렉션은 포커스 해제
        currentInteractable?.InvokeExit();

        // 현재 인터렉션으로 변경 및 포커스
        currentInteractable = newInteractable;
        currentInteractable?.InvokeEnter();
    }



}
