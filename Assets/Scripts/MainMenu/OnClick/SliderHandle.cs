using System.Collections;
using SimpleEasing;
using Type;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

public class SliderHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private ObjectWithComponent<RectTransform> inside;

    private Coroutine coroutine;

    private bool isHolding = false;
    private bool isHover = false;
    
    private const float NOMAL_SCALE = 0.5f;
    private const float ON_HOVER_SCALE = 0.7f;
    private const float ON_CLICK_SCALE = 0.3f;

    private const float NOMAL_DURATION = 0.4f;
    private const float ONCLICK_DURATION = 0.2f;

    private const EaseType EASETYPE = EaseType.OutCubic;

    private void Start() {
        //초기화
        inside.Bind();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        ScaleAnimation(Vector2Utils.FloatToVector2(ON_CLICK_SCALE), ONCLICK_DURATION);
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

        ScaleAnimation(Vector2Utils.FloatToVector2(end), NOMAL_DURATION);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHover = true;

        // 클릭 이벤트 중인 경우 무시
        if(isHolding) return;

        ScaleAnimation(Vector2Utils.FloatToVector2(ON_HOVER_SCALE), NOMAL_DURATION);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;

        ScaleAnimation(Vector2Utils.FloatToVector2(NOMAL_SCALE), NOMAL_DURATION);

        // 클릭 이벤트 중인 경우 무시
        if(isHolding) return;

    }

    private void ScaleAnimation(Vector2 target, float duration)
    {
        this.SafeStartCoroutine(
            ref coroutine,
            Utils.Generic.AnimationUtils.EasingChange<Vector2>(
                inside.component.component.localScale,
                target,
                value => inside.component.component.localScale = value,
                duration,
                EASETYPE
            )
        );
    }
}
