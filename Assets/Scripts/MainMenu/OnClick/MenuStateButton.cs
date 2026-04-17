using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Utils;
using Type.Menu;
using SimpleEasing;
using Type;

public class MenuStateButton : UIClickable
{
    private Vector2 normalScale;
    private Vector2 normalTextPos;
    private float normalFontSize;



    [SerializeField]
    private Vector2 onHoverScale;

    [SerializeField]
    private Vector2 onHoverTextPos;

    [Space(10), SerializeField]
    private float onClickFontSize;



    [Space(10), SerializeField]
    private ObjectWithComponent<RectTransform> bar;

    [SerializeField]
    private ObjectWithComponent<RectTransform, Text> text;

    [Space(10), SerializeField]
    private MenuState onCilckState;


    //애니메이션 용 코루틴들
    private Coroutine barScaleCoroutine;
    private Coroutine textPositionCoroutine;
    private Coroutine onClickCoroutine;

    const float DURATION = 0.2f;
    const EaseType EASETYPE = EaseType.OutCubic;

    private void Awake()
    {

        // 현재 설정된 값을 기본값으로 설정
        normalScale = bar.component.localScale;
        normalTextPos = text.component1.anchoredPosition;

        normalFontSize = text.component2.fontSize;
    }

    // 버튼 호버, 호버 해제 시 애니메이션 재생
    protected override void OnEnter()
    {
        ChangeRectTranform(normalScale, onHoverScale, normalTextPos, onHoverTextPos, DURATION, EASETYPE);
    }

    protected override void OnExit()
    {
        ChangeRectTranform(onHoverScale, normalScale, onHoverTextPos, normalTextPos, DURATION, EASETYPE);
    }

    // 바를 목표 크기로, 텍스트를 목표 좌표로 이동시키는 함수
    private void ChangeRectTranform(Vector2 startScale, Vector2 endScale, Vector2 startPosition, Vector2 endPosition, float duration, EaseType targetEaseType)
    {
        // 클릭 효과가 재생 중인 경우 무시
        if (onClickCoroutine != null) return;

        this.SafeStartCoroutine(
            ref barScaleCoroutine,
            Utils.Generic.AnimationUtils.EasingChange(
                startScale,
                endScale,
                bar.component.SetLocalScale,
                duration, targetEaseType,
                () => this.SafeStopCoroutine(ref barScaleCoroutine)
            )
        );

        this.SafeStartCoroutine(
            ref textPositionCoroutine,
            Utils.Generic.AnimationUtils.EasingChange(
                startPosition,
                endPosition,
                text.component1.SetAnchoredPosition,
                duration,
                targetEaseType,
                () => this.SafeStopCoroutine(ref textPositionCoroutine)
            )
);
    }

    protected override void OnDown()
    {
        // 클릭 효과가 재생 중인 경우 무시
        if (onClickCoroutine != null) return;

        // 목표 메뉴로 이동
        MenuStateManager.Instance.ChangeMenuState(onCilckState);

        //위치 돌아가기
        this.SafeStartCoroutine(
            ref textPositionCoroutine,
            Utils.Generic.AnimationUtils.EasingChange(
                onHoverTextPos,
                normalTextPos,
                text.component1.SetAnchoredPosition,
                DURATION,
                EASETYPE,
                () => this.SafeStopCoroutine(ref textPositionCoroutine)
            )
        );

        // 애니메이션 재생
        this.SafeStartCoroutine(
            ref onClickCoroutine,
            Utils.Generic.AnimationUtils.EasingChange(
                onClickFontSize,
                normalFontSize,
                TextFontSizeChange,
                DURATION,
                EASETYPE,
                () => this.SafeStopCoroutine(ref onClickCoroutine)
            )
        );
    }
    private void TextFontSizeChange(float value)
    {
        text.component2.fontSize = (int)value;
    }
}
