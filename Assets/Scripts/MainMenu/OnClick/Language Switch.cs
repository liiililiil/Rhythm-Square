using UnityEngine;
using UnityEngine.EventSystems;

using SimpleEasing;
using Type.Menu;
using System.Collections;
using Utils;
using Type;

public class LanguageSwitch : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private ObjectWithComponent<RectTransform> korean;
    [SerializeField]
    private ObjectWithComponent<RectTransform> english;

    [Space(10), SerializeField]
    private ObjectWithComponent<RectTransform> bar;

    private Coroutine BarCoroutine;
    private Coroutine koreanCoroutine;
    private Coroutine englishCoroutine;

    private const float BAR_POSITION_X = 200f;
    private const float BAR_POSITION_Y = -150f;
    private const float DURATION = 0.5f;
    private const EaseType EASETYPE = EaseType.OutCubic;
    private const float TEXT_POSITION_Y = 25f;

    private void Start() {
        korean.Bind();
        english.Bind();
        bar.Bind();
        
        SettingManager.Instance.onChangeLanguage.AddListener(ChangePosition);

        //초기화
        ChangePosition(SettingManager.Instance.GetSetting().language);


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangePosition(Language.Empty);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangePosition(SettingManager.Instance.GetSetting().language);
    }

    public void OnPointerClick(PointerEventData exentData)
    {
        switch (SettingManager.Instance.GetSetting().language)
        {
            case Language.Korean:
                SettingManager.Instance.SetLanguage(Language.English);
                break;
            case Language.English:
                SettingManager.Instance.SetLanguage(Language.Korean);
                break;
        }
    }

    private void ChangePosition(Language language)
    {
        int value = 0;
        switch (language)
        {
            case Language.Korean:
                value = 1;
                break;

            case Language.English:
                value = -1;
                break;

            default:
                value = 0;
                break;
        }


        //적용
        // 1. Bar 변환
        this.SafeStartCoroutine(
            ref BarCoroutine,
            Utils.Generic.AnimationUtils.EasingChange(
                bar.component.component.anchoredPosition,
                new Vector2(value * BAR_POSITION_X, BAR_POSITION_Y),
                val => bar.component.component.anchoredPosition = val,
                DURATION,
                EASETYPE
            )
        );

        // 2. Korean 변환
        this.SafeStartCoroutine(
            ref koreanCoroutine,
            Utils.Generic.AnimationUtils.EasingChange(
                korean.component.component.anchoredPosition,
                new Vector2(-200, -value * TEXT_POSITION_Y - 25),
                val => korean.component.component.anchoredPosition = val,
                DURATION,
                EASETYPE
            )
        );

        // 3. English 변환
        this.SafeStartCoroutine(
            ref englishCoroutine,
            Utils.Generic.AnimationUtils.EasingChange(
                english.component.component.anchoredPosition,
                new Vector2(200, value * TEXT_POSITION_Y - 25),
                val => english.component.component.anchoredPosition = val,
                DURATION,
                EASETYPE
            )
        );
    }
}
