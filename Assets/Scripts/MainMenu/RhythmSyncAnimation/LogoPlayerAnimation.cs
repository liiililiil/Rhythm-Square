using UnityEngine;
using Utils;
using SimpleEasing;
using RhythmSqaureUtils;

public class LogoPlayerAnimation : MonoBehaviour
{
    private Canvas canvas;
    private RectTransform rectTransform;

    [Header("RhythmSyncPart")]
    [SerializeField]
    EaseType easeType;
    float elapsed = 0;
    float t;

    float mouseValue;

    private void Awake() 
    {
        canvas = transform.GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    
    private void Start()
    {
        MenuMusicManager.Instance.OnBeat.AddListener(NextStep);
    }
    void Update()
    {
        Vector3 rotation = rectTransform.eulerAngles;
        rotation.z = 0;

        // 회전 값 획득
        rotation.z += RhythmSync();

        // 위를 바라보고 있으므로 90도 회전
        rotation.z += MouseTracking() - 90;

        rectTransform.eulerAngles = rotation;
    
    }

    // 특정 박자 때마다 한바퀴 돌리기
    private float RhythmSync()
    {
        elapsed += Time.deltaTime;

        if(elapsed >= Temps.BPM_TO_SEC * 2)
        {
            //보정
            return 0;
        }

        t = elapsed / (Temps.BPM_TO_SEC * 2);
        t = Ease.Easing(t,easeType);

        return Mathf.LerpUnclamped(0,360,t);
    }

    //마우스 처다보게 하기
    private float MouseTracking()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        canvas.transform as RectTransform,
            Input.mousePosition,
            null,
            out Vector2 mousePos
        );

        //상대 좌표로 변환
        mousePos += rectTransform.anchoredPosition;
        float targetZ = FloatUtils.LookAt2d(rectTransform.GetRectInCanvas(canvas.transform as RectTransform), mousePos);
        
        //스무딩하게
        mouseValue = Mathf.Lerp(mouseValue, targetZ, 0.5f);

        return mouseValue;
    }
    private void NextStep()
    {
        if((MenuMusicManager.Instance.beat - (Consts.SIGNATURE * 4)) % (Consts.SIGNATURE * 4) == 15)
        {
            elapsed = 0;
        }
    }
}
