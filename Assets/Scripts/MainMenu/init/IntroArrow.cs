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
        
        //초기화
        rightArrow.Bind();
        leftArrow.Bind();
        upArrow.Bind();
        downArrow.Bind();
        player.Bind();

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

        upArrow.secondComponent.component.enabled = true;
        downArrow.secondComponent.component.enabled = true;

        // 이동할 위치
        Vector2 upEnd = new Vector2(0,END_POS);
        Vector2 downEnd = new Vector2(0,-END_POS);

            // 이동
            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange(
                upArrow.firstComponent.component.anchoredPosition,
                upEnd,
                val => upArrow.firstComponent.component.anchoredPosition = val,
                beat,
                ARROW_ANIMATION
            ));

            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange(
                downArrow.firstComponent.component.anchoredPosition,
                downEnd,
                val => downArrow.firstComponent.component.anchoredPosition = val,
                beat,
                ARROW_ANIMATION
            ));

            // 넓이
            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange<Vector2>(
                upArrow.firstComponent.component.localScale,
                END_SCALE,
                val => upArrow.firstComponent.component.localScale = val,
                beat,
                ARROW_ANIMATION
            ));
            
            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange<Vector2>(
                downArrow.firstComponent.component.localScale,
                END_SCALE,
                val => downArrow.firstComponent.component.localScale = val,
                beat,
                ARROW_ANIMATION
            ));
    }

    private void VerticalAppeared()
    {
        float beat = MenuMusicManager.Instance.beatPerSec * 2;

        // 표시
        rightArrow.secondComponent.component.enabled = true;
        leftArrow.secondComponent.component.enabled = true;

        // 이동할 위치
        Vector2 rightEnd = new Vector2(END_POS,0);
        Vector2 leftEnd = new Vector2(-END_POS,0);

            // 이동
            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange(
                rightArrow.firstComponent.component.anchoredPosition,
                rightEnd,
                val => rightArrow.firstComponent.component.anchoredPosition = val,
                beat,
                ARROW_ANIMATION
            ));

            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange(
                leftArrow.firstComponent.component.anchoredPosition,
                leftEnd,
                val => leftArrow.firstComponent.component.anchoredPosition = val,
                beat,
                ARROW_ANIMATION
            ));

            // 넓이
            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange<Vector2>(
                rightArrow.firstComponent.component.localScale,
                END_SCALE,
                val => rightArrow.firstComponent.component.localScale = val,
                beat,
                ARROW_ANIMATION
            ));
            
            StartCoroutine(Utils.Generic.AnimationUtils.EasingChange<Vector2>(
                leftArrow.firstComponent.component.localScale,
                END_SCALE,
                val => leftArrow.firstComponent.component.localScale = val,
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
            val => rectTransform.component.rotation = Quaternion.Euler(0,0,val),
            MenuMusicManager.Instance.beatPerSec *3,
            ARROW_ROTATION
            )
        );

    }

    private void PlayerBind()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        transform.SetParent(player.component.component);
        rectTransform.localPosition = Vector3.zero;
    }


}
