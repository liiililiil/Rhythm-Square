using UnityEngine;
using Utils;
using SimpleEasing;
using Type.Menu;
using System;

public class LogoPlayerAnimation : MonoBehaviour
{
    private Canvas canvas;
    private RectTransform rectTransform;

    [Header("RhythmSyncPart")]
    [SerializeField]
    private EaseType easeType;
    private float elapsed = 0;
    private float t;

    private float mouseValue;

    private Func<float> rotationAction;

    private void Awake() 
    {
        MenuStateManager.Instance.onMenuStateChanged.AddListener(MenuStateChange);

        rotationAction = RotationWait;

    }

    private void MenuStateChange(MenuState _menuState)
    {
        // 메뉴로 상태가 바뀌면 액션 함수 바꾸기 
        if(_menuState == MenuState.Main)
        {
            rotationAction = Rotation; 

            //어차피 한번 바꾸면 되니까 해제
            MenuStateManager.Instance.onMenuStateChanged.RemoveListener(MenuStateChange);
        } 

        
    } 

    
    private void Start()
    {        
        canvas = transform.GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();

        MenuMusicManager.Instance.OnBeat.AddListener(NextStep);
    }
    void Update()
    {   
        Vector3 rotation = new Vector3(0,0,rotationAction.Invoke());
        rectTransform.eulerAngles = rotation;
    
    }


    private float RotationWait()
    {
        //각 고정
        return 90;
    }

    private float Rotation()
    {
        float rotation = rectTransform.eulerAngles.z;
        rotation = 0;

        // 특정 박자 당 회전 값 획득
        rotation += RhythmSync();

        // 플레이어 바라보게, 위를 바라보고 있으므로 90도 회전
        rotation += MouseTracking() - 90;

        return rotation;
    }

    // 특정 박자 때마다 한바퀴 돌리기
    private float RhythmSync()
    {
        if(elapsed >= MenuMusicManager.Instance.beatPerSec * 2)
        {
            //보정
            return 0;
        }

        elapsed += Time.deltaTime;

        t = elapsed / (MenuMusicManager.Instance.beatPerSec * 2);
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
        float targetZ = Vector2Utils.LookAt2d(rectTransform.GetRectInCanvas(canvas.transform as RectTransform), mousePos);
        
        //스무딩하게
        float speed = 5f;   // 클수록 빠르게 따라감

        float t = 1f - Mathf.Exp(-speed * Time.deltaTime);

        mouseValue = Mathf.LerpAngle(mouseValue, targetZ, t);

        return mouseValue;
    }
    private void NextStep()
    {
        int beatOffset = MenuMusicManager.Instance.backGroundInfo.beatOffset;
        int resetCycle = MenuMusicManager.Instance.backGroundInfo.playerBeatResetCycle;
        int cycle = MenuMusicManager.Instance.backGroundInfo.playerBeatCycle;
        
        if((MenuMusicManager.Instance.beat - beatOffset) % resetCycle == cycle)
        {
            elapsed = 0;
        }
    }
}
