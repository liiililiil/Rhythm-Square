using UnityEngine;
using System.Collections;

using SimpleEasing;
using Utils;
using Types.Menu.StateChange;
using Types.Menu;

public class RectPositionChanger : StateChanger
{
    [SerializeField]
    private SlowMenuStateChange<Vector2>[] stateChange;
    [SerializeField]
    private SlowMenuStateDefault<Vector2> stateDefault;

    private RectTransform rectTransform;

    private Coroutine coroutine;

    private Vector2 currentPosition;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
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
        this.SafeStartCoroutine(ref coroutine, ChangePositionCoroutine(targetVector, duration, easeType));
    }

    IEnumerator ChangePositionCoroutine(Vector2 targetPos, float duration, EaseType easeType)
    {
        Vector2 startPos = rectTransform.anchoredPosition;
        float elapsed = 0f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = Ease.Easing(t, easeType);

            rectTransform.anchoredPosition = Vector2.LerpUnclamped(startPos, targetPos, t);

            yield return null;
        }

        rectTransform.anchoredPosition = targetPos;

        this.SafeStopCoroutine(ref coroutine);
    }

}