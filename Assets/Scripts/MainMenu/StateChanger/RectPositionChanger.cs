using UnityEngine;
using Types;
using MainMenu.StateChanger;


public class RectPositionChanger : StateChanger
{
    [SerializeField]
    private MenuStateChange<Vector2>[] positionState;
    
    private RectTransform rectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    protected override void OnInvoke(MenuState newState)
    {
        foreach(var stateChange in positionState)
        {
            if(stateChange.TargetState == newState)
            {
                rectTransform.anchoredPosition = stateChange.Value;
                return;
            }
        }
    }

}


// 메뉴 상태에 따른 변화
