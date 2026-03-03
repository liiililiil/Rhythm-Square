using UnityEngine;

using SimpleEasing;
using Utils;

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
        // 특정 박자 마다 조금 느리게 시간 흐르기
        if((MenuMusicManager.Instance.beat - 16) % 32 >= 30)
        {
            elapsed += Time.deltaTime / 2;
        }
        else
        {    
            elapsed += Time.deltaTime;
        }

        // 재생이 완료되면
        if(elapsed >= Type.MainMenu.Const.BPM_TO_SEC)
        {
            //보정
            SetRectTranform(1);
            return;
        }

        // 정규화 후 Easing
        t = elapsed / Type.MainMenu.Const.BPM_TO_SEC;
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
        
        if((MenuMusicManager.Instance.beat - 16) % 32 != 30)
        {
            elapsed = 0;
        }
    }

}
