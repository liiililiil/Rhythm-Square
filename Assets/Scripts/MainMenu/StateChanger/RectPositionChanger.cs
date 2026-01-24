using UnityEngine;
using Types;
using MainMenu.StateChanger;
using System.Collections;
using Easing;
using Utils;


public class RectPositionChanger : StateChanger
{
    [SerializeField]
    private MenuStateChange<Vector2>[] positionState;
    [SerializeField]
    private MenuStateDefault<Vector2> defaultPosition;

    private RectTransform rectTransform;

    private Coroutine coroutine;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    protected override void OnInvoke(MenuState newState)
    {
        foreach(var stateChange in positionState)
        {
            if(stateChange.TargetState == newState)
            {
                this.SafeStartCoroutine(ref coroutine, ChangePositionCoroutine(stateChange.Value, stateChange.Duration, stateChange.EaseType));
                return;
            }
        }

        //기본값으로 변경
        this.SafeStartCoroutine(ref coroutine, ChangePositionCoroutine(defaultPosition.Value, defaultPosition.Duration, defaultPosition.EaseType));
    }

    IEnumerator ChangePositionCoroutine(Vector2 targetPos, float duration, EaseType easeType)
    {
        Vector2 startPos = rectTransform.anchoredPosition;
        float elapsed = 0f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = Ease.Easing(easeType, t);

            rectTransform.anchoredPosition = Vector2.LerpUnclamped(startPos, targetPos, t);

            yield return null;
        }

        rectTransform.anchoredPosition = targetPos;

        this.SafeStopCoroutine(ref coroutine);
    }

}