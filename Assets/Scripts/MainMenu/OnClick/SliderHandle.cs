using System.Collections;
using SimpleEasing;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

public class SliderHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private GameObject inside;

    private Coroutine coroutine;

    private RectTransform insideRectTransform;

    private bool isHolding = false;
    private bool isHover = false;
    
    private const float NOMAL_SCALE = 0.5f;
    private const float ON_HOVER_SCALE = 0.7f;
    private const float ON_CLICK_SCALE = 0.3f;

    private const float NOMAL_DURATION = 0.4f;
    private const float ONCLICK_DURATION = 0.2f;

    private const EaseType EASETYPE = EaseType.OutCubic;
    

    private void Awake()
    {
        insideRectTransform = inside.GetComponent<RectTransform>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        this.SafeStartCoroutine(ref coroutine, InsideEasingScale(insideRectTransform.localScale, Vector2Utils.FloatToVector2(ON_CLICK_SCALE), ONCLICK_DURATION, EASETYPE));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;

        float end;
        if (isHover)
        {
            end = ON_HOVER_SCALE;
        }
        else
        {
            end = NOMAL_SCALE;
        }

        this.SafeStartCoroutine(ref coroutine, InsideEasingScale(insideRectTransform.localScale, Vector2Utils.FloatToVector2(end), ONCLICK_DURATION, EASETYPE));
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHover = true;

        // 클릭 이벤트 중인 경우 무시
        if(isHolding) return;

        this.SafeStartCoroutine(ref coroutine, InsideEasingScale(insideRectTransform.localScale, Vector2Utils.FloatToVector2(ON_HOVER_SCALE), NOMAL_DURATION, EASETYPE));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;

        // 클릭 이벤트 중인 경우 무시
        if(isHolding) return;

        this.SafeStartCoroutine(ref coroutine, InsideEasingScale(insideRectTransform.localScale, Vector2Utils.FloatToVector2(NOMAL_SCALE), NOMAL_DURATION, EASETYPE));

    }

    IEnumerator InsideEasingScale(Vector2 start, Vector2 end, float duration, EaseType EASETYPE)
    {
        

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            t = Ease.Easing(t, EASETYPE);

            Vector2 newScale = Vector2.Lerp(start, end, t);
            insideRectTransform.localScale = newScale;

            yield return null;
        }

        //보정
        insideRectTransform.localScale = end;
    }
}
