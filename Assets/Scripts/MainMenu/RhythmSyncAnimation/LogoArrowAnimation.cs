using UnityEngine;

using SimpleEasing;
using Utils;

public class LogoArrowAnimation : MonoBehaviour
{
    RectTransform rectTransform;

    [SerializeField]
    int step = 0;

    [SerializeField]
    float elapsed = 0;

    [SerializeField]
    
    float t;

    [SerializeField]
    EaseType easeType;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        MenuMusicManager.Instance.invokeBeat.AddListener(NextStep);
    }

    private void OnDisable()
    {
        MenuMusicManager.Instance.invokeBeat.RemoveListener(NextStep);
        
        elapsed = 0;
        t = 0;

        rectTransform.eulerAngles = Vector2.zero;
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        if(elapsed >= Temps.BPM_TO_SEC)
        {
            //보정
            SetRectTranform(1);
            return;
        }

        t = elapsed / Temps.BPM_TO_SEC;
        t = Ease.Easing(t,easeType);

        SetRectTranform(t);
    }


    private void SetRectTranform(float t)
    {

        //크기
        float sizeDelta = Mathf.LerpUnclamped(1.2f,1f,t);

        rectTransform.localScale = Vector2Utils.FloatToVector2(sizeDelta);

        //각도
        Vector3 rotation = rectTransform.eulerAngles;

        rotation.z = Mathf.LerpUnclamped(step * 90, (step+1) * 90, t);
        rectTransform.eulerAngles = rotation; 
    }

    private void NextStep()
    {
        elapsed = 0;
        step += 1;

        step %= 4;
    }

}
