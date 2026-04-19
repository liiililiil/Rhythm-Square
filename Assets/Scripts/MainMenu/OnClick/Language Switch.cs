using UnityEngine;
using UnityEngine.EventSystems;

using SimpleEasing;
using Type.Menu;
using Utils;
using Type;

public class LanguageSwitch : UIClickable
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

    protected override void OnStart()
    {

        SettingManager.Instance.GetConfig<Language>(ConfigType.Language).OnChangeConfig.AddListener(ChangePosition);

        //초기화
        ChangePosition(SettingManager.Instance.GetConfigValue<Language>(ConfigType.Language));


    }

    protected override void OnEnter()
    {
        ChangePosition(Language.Empty);
    }

    protected override void OnExit()
    {
        ChangePosition(SettingManager.Instance.GetConfigValue<Language>(ConfigType.Language));
    }

    protected override void OnSubmit()
    {
        switch (SettingManager.Instance.GetConfigValue<Language>(ConfigType.Language))
        {
            case Language.Korean:
                SettingManager.Instance.SetValue(Language.English, ConfigType.Language);
                break;
            case Language.English:
                SettingManager.Instance.SetValue(Language.Korean, ConfigType.Language);
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
                bar.component.anchoredPosition,
                new Vector2(value * BAR_POSITION_X, BAR_POSITION_Y),
                bar.component.SetAnchoredPosition,
                DURATION,
                EASETYPE
            )
        );

        // 2. Korean 변환
        this.SafeStartCoroutine(
            ref koreanCoroutine,
            Utils.Generic.AnimationUtils.EasingChange(
                korean.component.anchoredPosition,
                new Vector2(-200, -value * TEXT_POSITION_Y - 25),
                korean.component.SetAnchoredPosition,
                DURATION,
                EASETYPE
            )
        );

        // 3. English 변환
        this.SafeStartCoroutine(
            ref englishCoroutine,
            Utils.Generic.AnimationUtils.EasingChange(
                english.component.anchoredPosition,
                new Vector2(200, value * TEXT_POSITION_Y - 25),
                english.component.SetAnchoredPosition,
                DURATION,
                EASETYPE
            )
        );
    }
}
