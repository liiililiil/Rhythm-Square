using SimpleEasing;
using UnityEngine;

public class MusicBar : MonoBehaviour
{
    private const float defaultPositionX = -400;
    private const float defaultWidth = 600;
    private const float selectedPositionX = -500;
    private const float selectedWidth = 800;

    private RectTransform rectTransform;
    [SerializeField]
    private EaseType easeType;
    [SerializeField]
    private float sensitivity;

    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void PositionUpdate(float y)
    {
        // 중앙과의 차이 비교
        float delta = Mathf.Abs(y);

        // 나누기
        if (delta != 0) delta = Mathf.Clamp01(delta * sensitivity);

        delta = Ease.Easing(delta, easeType);

        // 목표 값 지정
        float targetPositionX = Mathf.Lerp(selectedPositionX, defaultPositionX, delta);
        float targetWidth = Mathf.Lerp(selectedWidth, defaultWidth, delta);

        // 적용
        rectTransform.anchoredPosition = new Vector2(targetPositionX, y);
        rectTransform.sizeDelta = new Vector2(targetWidth, rectTransform.sizeDelta.y);
    }
}
