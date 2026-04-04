using System.Collections;
using SimpleEasing;
using Type;
using Type.Menu;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;

using Utils;

public class IntroArrow : MonoBehaviour
{
    [SerializeField]
    private ObjectWithComponent<RectTransform, SVGImage> rightArrow;

    [SerializeField]
    private ObjectWithComponent<RectTransform, SVGImage> leftArrow;

    [SerializeField]
    private ObjectWithComponent<RectTransform, SVGImage> upArrow;

    [SerializeField]
    private ObjectWithComponent<RectTransform, SVGImage> downArrow;

    [SerializeField]
    private ObjectWithComponent<RectTransform> player;

    [SerializeField]
    private EaseType ARROW_ANIMATION;

    [SerializeField]
    private EaseType ARROW_ROTATION;

    [Space(10),SerializeField]
    private MenuState disableMenuState;

    private int beat;

    private const int END_POS = 200;
    private readonly Vector2 END_SCALE = Vector2Utils.FloatToVector2(1);

    private InitableComponent<RectTransform> rectTransform;

    private void Start() {
        rectTransform = new InitableComponent<RectTransform>(gameObject);

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

        upArrow.component2.enabled = true;
        downArrow.component2.enabled = true;

        // 이동할 위치
        Vector2 upEnd = new Vector2(0,END_POS);
        Vector2 downEnd = new Vector2(0,-END_POS);

            // 이동
            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange(
                upArrow.component1.anchoredPosition,
                upEnd,
                upArrow.component1.SetAnchoredPosition,
                beat,
                ARROW_ANIMATION
            ));

            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange(
                downArrow.component1.anchoredPosition,
                downEnd,
                downArrow.component1.SetAnchoredPosition,
                beat,
                ARROW_ANIMATION
            ));

            // 넓이
            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange<Vector2>(
                upArrow.component1.localScale,
                END_SCALE,
                upArrow.component1.SetLocalScale,
                beat,
                ARROW_ANIMATION
            ));
            
            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange<Vector2>(
                downArrow.component1.localScale,
                END_SCALE,
                downArrow.component1.SetLocalScale,
                beat,
                ARROW_ANIMATION
            ));
    }

    private void VerticalAppeared()
    {
        float beat = MenuMusicManager.Instance.beatPerSec * 2;

        // 표시
        rightArrow.component2.enabled = true;
        leftArrow.component2.enabled = true;

        // 이동할 위치
        Vector2 rightEnd = new Vector2(END_POS,0);
        Vector2 leftEnd = new Vector2(-END_POS,0);

            // 이동
            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange(
                rightArrow.component1.anchoredPosition,
                rightEnd,
                rightArrow.component1.SetAnchoredPosition,
                beat,
                ARROW_ANIMATION
            ));

            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange(
                leftArrow.component1.anchoredPosition,
                leftEnd,
                leftArrow.component1.SetAnchoredPosition,
                beat,
                ARROW_ANIMATION
            ));

            // 넓이
            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange<Vector2>(
                rightArrow.component1.localScale,
                END_SCALE,
                rightArrow.component1.SetLocalScale,
                beat,
                ARROW_ANIMATION
            ));
            
            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange<Vector2>(
                leftArrow.component1.localScale,
                END_SCALE,
                leftArrow.component1.SetLocalScale,
                beat,
                ARROW_ANIMATION
            ));


        
    }

    private void ArrowsRotation()
    {
        StartCoroutine(
            Utils.Generic.AnimationUtils.EasingChange(
            rectTransform.component.eulerAngles.z,
            360,
            rectTransform.component.SetRotation,
            MenuMusicManager.Instance.beatPerSec *3,
            ARROW_ROTATION
            )
        );

    }

    private void PlayerBind()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        transform.SetParent(player.component);
        rectTransform.localPosition = Vector3.zero;
    }
}


