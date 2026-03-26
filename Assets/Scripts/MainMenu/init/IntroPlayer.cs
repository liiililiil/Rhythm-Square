using System.Collections;
using SimpleEasing;
using Type.Menu;
using Unity.VectorGraphics;
using UnityEngine;

using Utils;
public class IntroPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject arrows;


    [SerializeField]
    private Vector2 stayPos;

    [SerializeField]
    private Vector2 endPos;

    [Space(10), SerializeField]
    private float slidingSpeed;

    [Space(10), SerializeField]
    private MenuState disableMenuState;



    private RectTransform playerRect;
    private RectTransform arrowsRect;

    


    private int beat;


    private const int END_POS = 200;
    private readonly Vector2 END_SCALE = Vector2Utils.FloatToVector2(1);





    private void Start() {
        MenuMusicManager.Instance.OnBeat.AddListener(NextBeat);
        MenuAssetLoadManager.Instance.AssetLoaderBind(NextBeat);
        MenuStateManager.Instance.onMenuStateChanged.AddListener(DisableObject);

        playerRect = player.GetComponent<RectTransform>();
        arrowsRect = arrows.GetComponent<RectTransform>();
    }

    private void DisableObject(MenuState menuState)
    {
        // 목표 메뉴가 아니면 넘기기
        if(menuState != disableMenuState) return;
        player.SetActive(false);

        MenuStateManager.Instance.onMenuStateChanged.RemoveListener(DisableObject);
    }
    private void NextBeat()
    {
        beat++;

        switch (beat)
        {
            case 2:
                PlayerGoingToCenter();
                break;
            case 3:
                PlayerSliding();
                break;
            case 4:
                PlayerGoingToEnd();
                break;
            case 5:
                
                break;
            default:
                MenuAssetLoadManager.Instance.OnMainMenuAssetLoaded.RemoveListener(NextBeat);
                break;
        }
    }


    private void PlayerGoingToCenter()
    {

        // 랜덤 위치로 이동
        playerRect.rotation = Quaternion.Euler(new Vector3(0,0,Random.Range(0,360)));    
        playerRect.anchoredPosition -= (Vector2)playerRect.up * 1000;

        // 이미지 보이게 (조금 이따가)
        StartCoroutine(CoroutineUtils.SlowStart(PlayerEnabled, MenuMusicManager.Instance.beatPerSec / 2));

        StartCoroutine(AnimatedPosition(player, Vector2Utils.FloatToVector2(0),MenuMusicManager.Instance.beatPerSec, EaseType.InCirc));
    }

    public void PlayerEnabled()
    {
        player.GetComponent<SVGImage>().enabled = true;
    }

    private void PlayerSliding()
    {
        StartCoroutine(Slide(player, slidingSpeed, MenuMusicManager.Instance.beatPerSec));

        StartCoroutine(AnimatedRotation(player, playerRect.eulerAngles.z + 120, MenuMusicManager.Instance.beatPerSec, EaseType.Linear));
    }

    private void PlayerGoingToEnd()
    {
        StartCoroutine(AnimatedRotation(player, endPos, MenuMusicManager.Instance.beatPerSec, EaseType.OutCubic));
        StartCoroutine(AnimatedPosition(player, endPos, MenuMusicManager.Instance.beatPerSec * 1.1f, EaseType.InBack));
    }

    private IEnumerator Slide(GameObject gameObject, float startSpeed, float duration)
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        float elapsed = 0;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            float speed = Mathf.Lerp(startSpeed, 0, t);

            // Debug.Log(speed);
            
            rectTransform.Translate(Vector2.up * speed * Time.deltaTime);

            yield return null;
        }

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

    private IEnumerator AnimatedRotation(GameObject obj, float targetRotation, float duration, EaseType easeType)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        float start = rectTransform.eulerAngles.z;

        float elapsed = 0;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = Ease.Easing(t, easeType);

            float target= Mathf.LerpAngle(start, targetRotation, t);

            rectTransform.rotation = Quaternion.Euler(new Vector3(0,0,target));

            yield return null;
        }

        rectTransform.rotation = Quaternion.Euler(new Vector3(0,0,targetRotation));
        
    }

    private IEnumerator AnimatedRotation(GameObject obj, Vector2 target, float duration, EaseType easeType)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();

        //시작 각
        float start = rectTransform.eulerAngles.z;


        // 바라볼 위치
        float endRotation;
        

        float elapsed = 0;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;  
            float t = Mathf.Clamp01(elapsed / duration);
            t = Ease.Easing(t, easeType);

            // 바라볼 위치
            endRotation = Vector2Utils.LookAt2d(rectTransform.anchoredPosition, target) - 90;

            // 보간
            float targetRotation = Mathf.LerpAngle(start, endRotation, t);

            rectTransform.rotation = Quaternion.Euler(new Vector3(0,0,targetRotation ));

            yield return null;
        }

        // 바라볼 위치
        endRotation = Vector2Utils.LookAt2d(rectTransform.anchoredPosition, target);

        rectTransform.rotation = Quaternion.Euler(new Vector3(0,0,endRotation));
        
    }

}
