using UnityEngine;
using System.Collections;

using SimpleEasing;
using Utils;
using Type.Menu.StateChange;
using Type.Menu;

public class RectPositionChanger : StateChanger
{
    [SerializeField]
    private SlowMenuStateChange<Vector2>[] stateChange;
    [SerializeField]
    private SlowMenuStateDefault<Vector2> stateDefault;

    private RectTransform rect;

    private Coroutine coroutine;

    private Vector2 currentPosition;

    private void Awake() {
        rect = GetComponent<RectTransform>();
    }

    protected override void OnInvoke(MenuState newState)
    {
        foreach(var stateChange in stateChange)
        {
            if(stateChange.targetState == newState)
            {
                ChangePosition(stateChange.value, stateChange.duration, stateChange.easeType);
                return;
            }
        }

        ChangePosition(stateDefault.value, stateDefault.duration, stateDefault.easeType);
    }

    private void ChangePosition(Vector2 targetVector, float duration, EaseType easeType)
    {
        //이동하려는 위치와 현재 위치가 같으면 무시
        if(currentPosition == targetVector) return;
        currentPosition = targetVector;

        //기본값으로 변경
        this.SafeStartCoroutine(
            ref coroutine,
            Utils.Generic.AnimationUtils.EasingChange(
                rect.anchoredPosition,
                targetVector,
                rect.SetAnchoredPosition,
                duration,
                easeType
            )
        );
    }


}