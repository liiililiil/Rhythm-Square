using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Utils;
using Type.Menu;
using SimpleEasing;

public class MenuStateButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector2 normalScale;
    private Vector2 normalTextPos;
    private int normalFontSize;



    [SerializeField]
    private Vector2 onHoverScale;

    [SerializeField]
    private Vector2 onHoverTextPos;
    
    [Space(10), SerializeField]
    private int onClickFontSize;


    
    [Space(10),SerializeField]
    private GameObject bar;

    [SerializeField]
    private GameObject text;

    [Space(10),SerializeField]
    private MenuState onCilckState;
    
    //애니메이션 용 코루틴들
    private Coroutine barScaleCoroutine;
    private Coroutine textPositionCoroutine;
    private Coroutine onClickCoroutine;

    const float DURATION = 0.2f;
    const EaseType EASETYPE = EaseType.OutCubic;

    private void Awake() {
        // 현재 설정된 값을 기본값으로 설정
        normalScale = bar.GetComponent<RectTransform>().localScale;
        normalTextPos = text.GetComponent<RectTransform>().anchoredPosition;
        normalFontSize = text.GetComponent<Text>().fontSize;

    }

    // 버튼 호버, 호버 해제 시 애니메이션 재생
    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeRectTranform(normalScale, onHoverScale, normalTextPos, onHoverTextPos, DURATION, EASETYPE);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeRectTranform(onHoverScale, normalScale, onHoverTextPos, normalTextPos, DURATION, EASETYPE);
    }

    // 바를 목표 크기로, 텍스트를 목표 좌표로 이동시키는 함수
    private void ChangeRectTranform(Vector2 startScale, Vector2 endScale, Vector2 startPosition, Vector2 endPosition, float duration, EaseType targetEaseType)
    {
        // 클릭 효과가 재생 중인 경우 무시
        if(onClickCoroutine != null) return;

        this.SafeStartCoroutine(ref barScaleCoroutine, BarEasingScaleChange(startScale, endScale, duration, targetEaseType));
        this.SafeStartCoroutine(ref textPositionCoroutine, TextEasingPositionChange(startPosition, endPosition, duration, targetEaseType));
    }
    
    public void OnPointerClick(PointerEventData exentData)
    {
        // 클릭 효과가 재생 중인 경우 무시
        if(onClickCoroutine != null) return;

        // 목표 메뉴로 이동
        MenuStateManager.Instance.ChangeMenuState(onCilckState);

        //위치 돌아가기
        this.SafeStartCoroutine(ref textPositionCoroutine, TextEasingPositionChange(onHoverTextPos, normalTextPos, DURATION, EASETYPE));

        //애니메이션 재생
        this.SafeStartCoroutine(ref onClickCoroutine, TextOnClick(onClickFontSize, normalFontSize, DURATION, EASETYPE));
        

    }

    IEnumerator BarEasingScaleChange(Vector2 start, Vector2 end, float duration, EaseType easeType)
    {
        RectTransform rt = bar.GetComponent<RectTransform>();

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            t = Ease.Easing(t, easeType);

            Vector2 newScale = Vector2.Lerp(start, end, t);
            rt.localScale = newScale;

            yield return null;
        }

        //보정
        rt.localScale = end;

        this.SafeStopCoroutine(ref barScaleCoroutine);
    }

    IEnumerator TextEasingPositionChange(Vector2 start, Vector2 end, float duration, EaseType easeType)
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
    }


    IEnumerator TextOnClick(int start, int end, float duration, EaseType easeType)
    {
        Text targetText = text.GetComponent<Text>();

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            t = Ease.Easing(t, easeType);

            int newSize = (int)Mathf.Lerp(start, end, t);
            targetText.fontSize = newSize;
            yield return null;
        }

        //보정
        targetText.fontSize = end;

        this.SafeStopCoroutine(ref onClickCoroutine);
    }

}
