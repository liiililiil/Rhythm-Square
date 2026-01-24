using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;
using Easing;

public class ButtonHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    float ON_HOVER_Y_SIZE = 40f;

    [SerializeField]
    float NORMAL_Y_SIZE = 80f; 

    [SerializeField]
    float ON_HOVER_TEXT_X_POS = -570f;
    
    [SerializeField]
    float NORMAL_TEXT_X_POS = -600f;

    const EaseType EASE_TYPE = EaseType.OutCubic;

    

    [SerializeField]
    private GameObject bar;

    [SerializeField]
    private GameObject text;


    private Coroutine barScaleCoroutine;
    private Coroutine textPositionCoroutine;
    



    public void OnPointerEnter(PointerEventData eventData)
    {
        //마우스가 버튼 위에 있을 때 애니메이션 재생
        this.SafeStartCoroutine(ref barScaleCoroutine, SetBarScale(NORMAL_Y_SIZE, ON_HOVER_Y_SIZE, 0.2f, EASE_TYPE));
        this.SafeStartCoroutine(ref textPositionCoroutine, SetTextPosition(NORMAL_TEXT_X_POS, ON_HOVER_TEXT_X_POS, 0.2f, EASE_TYPE));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //마우스가 버튼에서 벗어났을 때 애니메이션 재생
        this.SafeStartCoroutine(ref barScaleCoroutine, SetBarScale(ON_HOVER_Y_SIZE, NORMAL_Y_SIZE, 0.2f, EASE_TYPE));
        this.SafeStartCoroutine(ref textPositionCoroutine, SetTextPosition(ON_HOVER_TEXT_X_POS, NORMAL_TEXT_X_POS, 0.2f, EASE_TYPE));

    }


    IEnumerator SetBarScale(float start, float end, float duration, EaseType easeType)
    {
        RectTransform rt = bar.GetComponent<RectTransform>();
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = Ease.Easing(easeType, t);

            float newY = Mathf.Lerp(start, end, t);

            rt.SetTopBottom(newY, newY);

            yield return null;
        }

        rt.SetTopBottom(end, end);
        this.SafeStopCoroutine(ref barScaleCoroutine);
    }

    IEnumerator SetTextPosition(float start, float end, float duration, EaseType easeType)
    {
        RectTransform rt = text.GetComponent<RectTransform>();
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = Ease.Easing(easeType, t);

            float newX = Mathf.Lerp(start, end, t);


            rt.SetPosX(newX);

            yield return null;
        }

        rt.SetPosX(end);

        this.SafeStopCoroutine(ref textPositionCoroutine);
    }
    



}
