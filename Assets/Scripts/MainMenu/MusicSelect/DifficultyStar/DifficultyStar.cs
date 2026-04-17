using System.Collections.Generic;
using SimpleEasing;
using Type;
using Unity.VectorGraphics;
using UnityEngine;
using Utils;
using Utils.Generic;

public class DifficultyStar : MonoBehaviour
{

    [SerializeField]
    private List<ObjectWithComponent<RectTransform>> wings = new List<ObjectWithComponent<RectTransform>>();

    [Space(10), SerializeField]
    private float duration;

    [SerializeField]
    private EaseType easeType;

    [Space(10), SerializeField]
    private Row<int>[] rotations;

    private Coroutine[] wingCoroutines;

    int currentPhase;


    public void SetPhase(int phase)
    {
        // 변경 없으면 스킵
        if (currentPhase == phase) return;

        currentPhase = phase;
        ObjectUpdate();
    }
    private void Awake()
    {
        wingCoroutines = new Coroutine[wings.Count];

    }

    private void Start()
    {
        SetPhase(0);
    }

    private void ObjectUpdate()
    {
        List<float> startRotate = new List<float>();
        List<float> endRotate = new List<float>();

        // 시작점 저장
        for (int i = 0; i < wings.Count; i++)
        {
            startRotate.Add(wings[i].component.eulerAngles.z);
        }

        // 종료 지점 가져오기
        for (int i = 0; i < wings.Count; i++)
        {
            endRotate.Add(rotations[currentPhase].cells[i]);
        }

        // 적용
        for (int i = 0; i < wings.Count; i++)
        {
            this.SafeStartCoroutine(ref wingCoroutines[i], Utils.Generic.AnimationUtils.EasingChange(
                startRotate[i],
                endRotate[i],
                wings[i].component.SetRotation,
                duration,
                easeType
            ));
        }
    }
}
