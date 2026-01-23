using UnityEngine;

public class SoundVisualize : MonoBehaviour
{
    //FFT 샘플 크기
    const int SAMPLE_SIZE = 2048;

    //갯수
    const int BAR_COUNT = 360;

    //반지름
    const float RADIAN  = 100;

    const float BAR_MAX_HEIGHT = 100f;
    const float BAR_MIN_HEIGHT = 2f;

    //증폭 배율
    const float BAR_MULTIPLIER = 50f;

    //움직이는 속도
    const float BAR_SCALE_SPEED = 5f;
    

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
            barRectTransforms[i].rotation = Quaternion.Euler(0, 0, angle);

        }
        
    }



    void Update()
    {
        AudioListener.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

        for (int i = 0; i < BAR_COUNT; i++)
        {

            float sampleValue = samples[i % SAMPLE_SIZE] * BAR_MULTIPLIER;

            float clampedValue = Mathf.Clamp(sampleValue, BAR_MIN_HEIGHT, BAR_MAX_HEIGHT);

            //사이즈 계산
            Vector2 sizeDelta = barRectTransforms[i].sizeDelta;
            sizeDelta.y = Mathf.Lerp(sizeDelta.y, clampedValue, Time.deltaTime * BAR_SCALE_SPEED);
            barRectTransforms[i].sizeDelta = sizeDelta;
        }
    }
}
