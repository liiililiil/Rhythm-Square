using System.Collections.Generic;
using SimpleEasing;
using Type;
using Unity.VectorGraphics;
using UnityEngine;
using Utils;

public class DifficultyStar : MonoBehaviour
{
    [SerializeField]
    private ObjectWithComponent<RectTransform, SVGImage> star;

    [SerializeField]
    private ObjectWithComponent<RectTransform, SVGImage> wing1;
    [SerializeField]
    private ObjectWithComponent<RectTransform, SVGImage> wing2;

    [Space(10), SerializeField]
    private float duration;

    [SerializeField]
    private EaseType easeType;

    [Space(10), SerializeField]
    private List<Color> starColor;

    private Coroutine starCoroutine;
    private Coroutine starColorCoroutine;
    private Coroutine wingCoroutine;




    private int phase;

    public void SetPhase(int phase)
    {
        this.phase = phase;
        ObjectUpdate();
    }


    private void Start()
    {
        SetPhase(0);
    }

    private void ObjectUpdate()
    {
        // 날개
        this.SafeStartCoroutine(ref wingCoroutine, Utils.Generic.AnimationUtils.EasingChange(
            wing1.component1.rotation.eulerAngles.z,
            phase == 0 ? 0 : 45,
            WingUpdate,
            duration,
            easeType
        ));

        // 별
        this.SafeStartCoroutine(ref starCoroutine, Utils.Generic.AnimationUtils.EasingChange(
            star.component1.rotation.eulerAngles.z,
            phase * 45,
            star.component1.SetRotation,
            duration,
            easeType
        ));

        this.SafeStartCoroutine(ref starColorCoroutine, Utils.Generic.AnimationUtils.EasingChange(
            star.component2.color,
            starColor[phase],
            c => star.component2.color = c,
            duration,
            easeType
        ));
    }

    private void WingUpdate(float target)
    {
        wing1.component1.SetRotation(target);
        wing2.component1.SetRotation(-target);
    }


}
