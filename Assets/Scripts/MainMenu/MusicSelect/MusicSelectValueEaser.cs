using System.Collections;
using UnityEngine;
using Utils;

public abstract class MusicSelectValueEaser : MonoBehaviour
{
    // 실제 위치
    private float resultPosition = 0;
    private float targetPosition = 0;

    // 받은 정보 저장용
    private float position;
    private int index;

    private const float smoothSpeed = 7f;
    private void Start()
    {
        // 이벤트 리스너 등록
        MusicSelectManager.Instance.onChangePosition.AddListener(OnChangePosition);
        MusicSelectManager.Instance.onChangeIndex.AddListener(OnChangeIndex);

        OnStart();
    }

    protected virtual void OnStart() { }

    // 포지션은 바로 반영
    private void OnChangePosition(float position)
    {
        this.position = position;
        OnChangeValue();
    }

    private void OnChangeIndex(int index)
    {
        this.index = index;
        OnChangeValue();
    }
    private void OnChangeValue()
    {
        targetPosition = index + position;
    }



    protected void Update()
    {
        resultPosition = Mathf.Lerp(resultPosition, targetPosition, 1 - Mathf.Exp(-smoothSpeed * Time.deltaTime));
        UpdatePosition(resultPosition);
    }

    protected abstract void UpdatePosition(float position);

}
