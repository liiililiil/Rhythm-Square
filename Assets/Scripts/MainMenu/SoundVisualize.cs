using UnityEngine;

public class SoundVisualize : MonoBehaviour
{
    //FFT 샘플 크기
    const int SAMPLE_SIZE = 2048;

    //갯수
    const int BAR_COUNT = 360;

    //반지름
    const float RADIAN  = 3f;

    const float BAR_High_HEIGHT = 3f;
    const float BAR_MIN_HEIGHT = 0.01f;

    //증폭 값
    const float BAR_MULTIPLIER = 10f;

    //각 샘플 별 증폭 증가량
    const float BAR_MULTIPLIER_INCREASE = 0.3f;
    //제곱 값
    const float BAR_AMPLIFY = 1.2f;


    //감소 속도
    const float BAR_DECREASE_SPEED = 3f;
    

    private float[] samples = new float[SAMPLE_SIZE];
    private Transform[] barTransforms = new Transform[BAR_COUNT]; 

    [SerializeField]
    private GameObject barPrefab;

    private void Start() {
        for (int i = 0; i < BAR_COUNT; i++)
        {
            float angle = i * (360f / BAR_COUNT);
            float radians = angle * Mathf.Deg2Rad;

            barTransforms[i] = Instantiate(barPrefab, transform).GetComponent<Transform>();

            //위치 설정
            Vector2 position = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * RADIAN;
            barTransforms[i].position = position;

            //회전 설정
            barTransforms[i].rotation = Quaternion.Euler(0, 0, angle + 90);

        }
        
    }



    void Update()
    {
        AudioListener.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

        for (int i = 0; i < BAR_COUNT; i++)
        {

            float sampleValue = Mathf.Pow(samples[i % SAMPLE_SIZE] * (BAR_MULTIPLIER + (i * BAR_MULTIPLIER_INCREASE)),BAR_AMPLIFY);
            //너무 크면 로그 스케일 적용
            if(sampleValue > BAR_High_HEIGHT)
            {
                sampleValue = Mathf.Log10(sampleValue - BAR_High_HEIGHT) + BAR_High_HEIGHT;
            }

            //최소값 설정
            sampleValue = Mathf.Max(sampleValue, BAR_MIN_HEIGHT);

            Vector2 scale = barTransforms[i].localScale;

            //부드럽게 감소되도록 처리
            if(scale.y > Mathf.Max(sampleValue - BAR_DECREASE_SPEED * Time.deltaTime, BAR_MIN_HEIGHT))
            {
                scale.y -= BAR_DECREASE_SPEED * Time.deltaTime;
            }
            else
            {
                scale.y = sampleValue;
            }

            //크기 적용
            barTransforms[i].localScale = scale;
        }
    }
}
