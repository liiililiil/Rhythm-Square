using System.Collections;
using SimpleEasing;
using Type.Menu;
using Unity.VectorGraphics;
using UnityEngine;

using Utils;

public class IntroArrow : MonoBehaviour
{
    [SerializeField]
    private GameObject rightArrow;

    [SerializeField]
    private GameObject leftArrow;

    [SerializeField]
    private GameObject upArrow;

    [SerializeField]
    private GameObject downArrow;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private EaseType ARROW_ANIMATION;

    [SerializeField]
    private EaseType ARROW_ROTATION;

    [Space(10),SerializeField]
    private MenuState disableMenuState;

    private int beat;


    private const int END_POS = 200;
    private readonly Vector2 END_SCALE = Vector2Utils.FloatToVector2(1);





    private void Start() {
        MenuMusicManager.Instance.OnBeat.AddListener(NextBeat);
        MenuAssetLoadManager.Instance.AssetLoaderBind(NextBeat);
        MenuStateManager.Instance.onMenuStateChanged.AddListener(DisableObject);
    }


    private void DisableObject(MenuState menuState)
    {
        // 목표 메뉴가 아니면 넘기기
        if(menuState != disableMenuState) return;
        leftArrow.gameObject.SetActive(false);
        rightArrow.gameObject.SetActive(false);
        upArrow.gameObject.SetActive(false);
        downArrow.gameObject.SetActive(false);

        MenuStateManager.Instance.onMenuStateChanged.RemoveListener(DisableObject);
    }
    private void NextBeat()
    {
        beat++;

        switch (beat)
        {
            case 1:
                HorizontalAppeared();
                break;
            case 2:
                VerticalAppeared();
                break;
            case 3:
                ArrowsRotation();
                PlayerBind();
                break;
            default:
                MenuAssetLoadManager.Instance.OnMainMenuAssetLoaded.RemoveListener(NextBeat);
                break;
        }
    }

    private void HorizontalAppeared()
    {
        float beat = MenuMusicManager.Instance.beatPerSec * 2;

        // 표시
        upArrow.GetComponent<SVGImage>().enabled = true;
        downArrow.GetComponent<SVGImage>().enabled = true;

        // 이동할 위치
        Vector2 upEnd = new Vector2(0,END_POS);
        Vector2 downEnd = new Vector2(0,-END_POS);

        // 이동
        StartCoroutine(AnimatedPosition(upArrow, upEnd, beat, ARROW_ANIMATION));
        StartCoroutine(AnimatedPosition(downArrow, downEnd, beat, ARROW_ANIMATION));

        // 넓이
        StartCoroutine(AnimatedScale(upArrow, END_SCALE, beat, ARROW_ANIMATION));
        StartCoroutine(AnimatedScale(downArrow, END_SCALE, beat, ARROW_ANIMATION));
    }

    private void VerticalAppeared()
    {
        float beat = MenuMusicManager.Instance.beatPerSec * 2;

        // 표시
        rightArrow.GetComponent<SVGImage>().enabled = true;
        leftArrow.GetComponent<SVGImage>().enabled = true;

        // 이동할 위치
        Vector2 rightEnd = new Vector2(END_POS,0);
        Vector2 leftEnd = new Vector2(-END_POS,0);

        // 이동
        StartCoroutine(AnimatedPosition(rightArrow, rightEnd, beat, ARROW_ANIMATION));
        StartCoroutine(AnimatedPosition(leftArrow, leftEnd, beat, ARROW_ANIMATION));

        // 넓이
        StartCoroutine(AnimatedScale(rightArrow, END_SCALE, beat, ARROW_ANIMATION));
        StartCoroutine(AnimatedScale(leftArrow, END_SCALE, beat, ARROW_ANIMATION));

        
    }

    private void ArrowsRotation()
    {
        StartCoroutine(AnimatedRotation(gameObject, 360, MenuMusicManager.Instance.beatPerSec * 3, ARROW_ROTATION));
    }

    private void PlayerBind()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        transform.SetParent(player.transform);
        rectTransform.localPosition = Vector3.zero;
    }


    private IEnumerator AnimatedPosition(GameObject obj, Vector2 targetPos, float duration, EaseType easeType)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        Vector2 startPos = rectTransform.anchoredPosition;

        float elapsed = 0;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = Ease.Easing(t, easeType);

            rectTransform.anchoredPosition = Vector2.LerpUnclamped(startPos, targetPos, t);

            yield return null;
        }

        rectTransform.anchoredPosition = targetPos;
    }

    private IEnumerator AnimatedScale(GameObject obj, Vector2 targetScale, float duration, EaseType easeType)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        Vector2 startScale = rectTransform.localScale;

        float elapsed = 0;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = Ease.Easing(t, easeType);

            rectTransform.localScale = Vector2.LerpUnclamped(startScale, targetScale, t);

            yield return null;
        }

        rectTransform.localScale = targetScale;
    }

    private IEnumerator AnimatedRotation(GameObject obj, float targetRotation, float duration, EaseType easeType)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        float start = rectTransform.rotation.z;

        float elapsed = 0;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = Ease.Easing(t, easeType);

            float target= Mathf.LerpUnclamped(start, targetRotation, t);

            rectTransform.rotation = Quaternion.Euler(new Vector3(0,0,target));

            yield return null;
        }

        rectTransform.rotation = Quaternion.Euler(new Vector3(0,0,targetRotation));
        
    }

}
