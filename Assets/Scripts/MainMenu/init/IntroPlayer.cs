using System.Collections;
using SimpleEasing;
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


    private int beat;


    private const int END_POS = 200;
    private readonly Vector2 END_SCALE = Vector2Utils.FloatToVector2(1);





    private void Start() {
        MenuMusicManager.Instance.OnBeat.AddListener(NextBeat);
        AssetLoadManager.Instance.LoaderBind(NextBeat);
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
                break;
            case 5:
            
                break;
            default:
                AssetLoadManager.Instance.OnMainMenuAssetLoaded.RemoveListener(NextBeat);
                break;
        }
    }


    private void PlayerGoingToCenter()
    {
        RectTransform rectTransform = player.GetComponent<RectTransform>();

        // 랜덤 위치로 이동
        rectTransform.rotation = Quaternion.Euler(new Vector3(0,0,Random.Range(0,360)));    
        rectTransform.anchoredPosition -= (Vector2)rectTransform.up * 1800;

        // 이미지 보이게
        player.GetComponent<SVGImage>().enabled = true;

        StartCoroutine(AnimatedPosition(player, Vector2Utils.FloatToVector2(0),MenuMusicManager.Instance.beatPerSec, EaseType.InCirc));
    }

    private void PlayerBind()
    {
    }


    private IEnumerator ContinuesLootAt(GameObject target, GameObject lookAt, float duration)
    {
        RectTransform rectTransform = target.GetComponent<RectTransform>();
        RectTransform rectTransformLookAt = lookAt.GetComponent<RectTransform>();

        float elapsed = 0;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;

            Vector2Utils.LookAt2d(rectTransform.anchoredPosition, rectTransformLookAt.anchoredPosition);

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
