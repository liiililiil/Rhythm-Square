using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;
using SimpleEasing;

public class ButtonHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Vector2 onHoverScale;

    [SerializeField]
    private Vector2 normalScale;

    [Space(10),SerializeField]
    private Vector2 onHoverTextPos;
    
    [SerializeField]
    private Vector2 normalTextPos;

    const EaseType easeType = EaseType.OutCubic;

    [Space(10),SerializeField]
    private GameObject bar;

    [SerializeField]
    private GameObject text;

    private Coroutine barScaleCoroutine;
    private Coroutine textPositionCoroutine;
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.SafeStartCoroutine(ref barScaleCoroutine, SetBarScale(normalScale, onHoverScale, 0.2f, easeType));
        this.SafeStartCoroutine(ref textPositionCoroutine, SetTextPosition(normalTextPos, onHoverTextPos, 0.2f, easeType));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.SafeStartCoroutine(ref barScaleCoroutine, SetBarScale(onHoverScale, normalScale, 0.2f, easeType));
        this.SafeStartCoroutine(ref textPositionCoroutine, SetTextPosition(onHoverTextPos, normalTextPos, 0.2f, easeType));
    }
    
    public void OnPointerClick(PointerEventData exentData)
    {
        
    }

    IEnumerator SetBarScale(Vector2 start, Vector2 end, float duration, EaseType easeType)
    {
        RectTransform rt = bar.GetComponent<RectTransform>();

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = Ease.Easing(t, easeType);
            Vector2 newScale = Vector2.Lerp(start, end, t);
            rt.SetScale(newScale);

            yield return null;
        }

        //보정
        rt.SetScale(end);
        this.SafeStopCoroutine(ref barScaleCoroutine);
    }

    IEnumerator SetTextPosition(Vector2 start, Vector2 end, float duration, EaseType easeType)
    {
        RectTransform rt = text.GetComponent<RectTransform>();
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = Ease.Easing(t, easeType);
            Vector2 newPosition = Vector2.Lerp(start, end, t);
            rt.anchoredPosition = newPosition;
            yield return null;
        }

        // 보정
        rt.anchoredPosition = end;

        this.SafeStopCoroutine(ref textPositionCoroutine);
    }
}
