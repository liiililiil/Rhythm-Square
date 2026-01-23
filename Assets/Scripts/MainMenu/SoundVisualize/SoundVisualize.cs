using UnityEngine;

public class SoundVisualize : MonoBehaviour
{
    //FFT 샘플 크기
    const int SAMPLE_SIZE = 2048;

    //갯수
    const int BAR_COUNT = 360;

    //반지름
    const float RADIAN  = 300;

    const float BAR_High_HEIGHT = 300f;
    const float BAR_MIN_HEIGHT = 1f;

    //증폭 배율
    const float BAR_MULTIPLIER = 5000f;

    //감소 속도
    const float BAR_DECREASE_SPEED = 100f;
    

    private float[] samples = new float[SAMPLE_SIZE];
    private RectTransform[] barRectTransforms = new RectTransform[BAR_COUNT]; 

    [SerializeField]
    private GameObject barPrefab;

    private void Start() {
        for (int i = 0; i < BAR_COUNT; i++)
        {
            float angle = i * (360f / BAR_COUNT);
            float radians = angle * Mathf.Deg2Rad;

            barRectTransforms[i] = Instantiate(barPrefab, transform).GetComponent<RectTransform>();

            //위치 설정
            Vector2 position = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * RADIAN;
            barRectTransforms[i].anchoredPosition = position;

            //회전 설정
            barRectTransforms[i].rotation = Quaternion.Euler(0, 0, angle + 90);

        }
        
    }



    void Update()
    {
        AudioListener.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

        for (int i = 0; i < BAR_COUNT; i++)
        {

            float sampleValue = samples[i % SAMPLE_SIZE] * BAR_MULTIPLIER;

            //너무 크면 로그 스케일 적용
            if(sampleValue > BAR_High_HEIGHT)
            {
                sampleValue = Mathf.Log10(sampleValue - BAR_High_HEIGHT) + BAR_High_HEIGHT;
            }

            //최소값 설정
            sampleValue = Mathf.Max(sampleValue, BAR_MIN_HEIGHT);

            Vector2 sizeDelta = barRectTransforms[i].sizeDelta;

            //부드럽게 감소되도록 처리
            if(sizeDelta.y > sampleValue - BAR_DECREASE_SPEED * Time.deltaTime)
            {
                sizeDelta.y -= BAR_DECREASE_SPEED * Time.deltaTime;
            }
            else
            {
                sizeDelta.y = sampleValue;
            }

            //크기 적용
            barRectTransforms[i].sizeDelta = sizeDelta;
        }
    }
}
