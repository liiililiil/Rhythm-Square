using UnityEngine;

using SimpleEasing;
using Utils;
using AudioManagement;

public class LogoArrowAnimation : MonoBehaviour
{
    RectTransform rectTransform;

    float elapsed = 0;
    float t;


    [SerializeField]
    EaseType easeType;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        MenuMusicManager.Instance.OnBeat.AddListener(NextStep);
    }


    void Update()
    {
        int beatOffset = MenuMusicManager.Instance.backGroundInfo.beatOffset;
        int resetCycle = MenuMusicManager.Instance.backGroundInfo.arrowBeatResetCycle;
        int cycle = MenuMusicManager.Instance.backGroundInfo.arrowBeatCycle;

        // 특정 박자 마다 조금 느리게 시간 흐르기
        if((MenuMusicManager.Instance.beat - beatOffset ) % resetCycle >= cycle)
        {
            elapsed += Time.deltaTime / (resetCycle - cycle);
        }
        else
        {    
            elapsed += Time.deltaTime;
        }

        // 재생이 완료되면
        if(elapsed >= MenuMusicManager.Instance.beatPerSec)
        {
            //보정
            SetRectTranform(1);
            return;
        }

        // 정규화 후 Easing
        t = elapsed / MenuMusicManager.Instance.beatPerSec;
        t = Ease.Easing(t,easeType);

        // Eased 값을 가지고 크기와 회전 제어
        SetRectTranform(t);
    }


    private void SetRectTranform(float t)
    {

        //크기
        float sizeDelta = Mathf.LerpUnclamped(1.2f,1f,t);
        rectTransform.localScale = Vector2Utils.FloatToVector2(sizeDelta);

        //각도
        Vector3 rotation = rectTransform.eulerAngles;

        rotation.z = Mathf.LerpUnclamped(MenuMusicManager.Instance.beat * 90, (MenuMusicManager.Instance.beat +1) * 90, t);
        rectTransform.eulerAngles = rotation; 
    }

    private void NextStep()
    {
        int beatOffset = MenuMusicManager.Instance.backGroundInfo.beatOffset;
        int resetCycle = MenuMusicManager.Instance.backGroundInfo.arrowBeatResetCycle;
        int cycle = MenuMusicManager.Instance.backGroundInfo.arrowBeatCycle;

        if((MenuMusicManager.Instance.beat - beatOffset ) % resetCycle != cycle)
        {
            elapsed = 0;
        }
    }

}
