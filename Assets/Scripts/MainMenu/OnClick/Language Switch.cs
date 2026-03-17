using UnityEngine;
using UnityEngine.EventSystems;

using SimpleEasing;
using Type.Menu;
using System.Collections;
using Utils;

public class LanguageSwitch : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private GameObject korean;
    [SerializeField]
    private GameObject english;

    [Space(10), SerializeField]
    private GameObject bar;

    private Coroutine BarCoroutine;
    private Coroutine koreanCoroutine;
    private Coroutine englishCoroutine;

    private const float BAR_POSITION_X = 200f;
    private const float BAR_POSITION_Y = -150f;
    private const float DURATION = 0.5f;
    private const EaseType EASETYPE = EaseType.OutCubic;
    private const float TEXT_POSITION_Y = 25f;

    private void Start() {
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
        this.SafeStartCoroutine(ref BarCoroutine, SlowChangePosition(bar, (bar.transform as RectTransform).anchoredPosition, new Vector2(value *BAR_POSITION_X, BAR_POSITION_Y), DURATION, EASETYPE));
        this.SafeStartCoroutine(ref koreanCoroutine, SlowChangePosition(korean, (korean.transform as RectTransform).anchoredPosition, new Vector2(-200, -value *TEXT_POSITION_Y - 25),DURATION, EASETYPE));
        this.SafeStartCoroutine(ref englishCoroutine, SlowChangePosition(english, (english.transform as RectTransform).anchoredPosition, new Vector2(200, value *TEXT_POSITION_Y - 25),DURATION, EASETYPE));
    }

    private IEnumerator SlowChangePosition(GameObject gameObject, Vector2 start, Vector2 end, float duration, EaseType easeType)
    {
        RectTransform rt = gameObject.GetComponent<RectTransform>();
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


}
