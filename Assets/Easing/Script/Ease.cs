using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public enum EaseType
{
    InCirc, OutCirc, InOutCirc, OutInCirc,
    InCubic, OutCubic, InOutCubic, OutInCubic,
    InBack, OutBack, InOutBack, OutInBack,
    InQuint, OutQuint, InOutQuint, OutInQuint,
    InExpo, OutExpo, InOutExpo, OutInExpo,
    InSine, OutSine, InOutSine, OutInSine,
    InBounce, OutBounce, InOutBounce, OutInBounce,
    InQuad, OutQuad, InOutQuad, OutInQuad,
    InQuart, OutQuart, InOutQuart, OutInQuart,
    InElastic, OutElastic, InOutElastic, OutInElastic,
    Linear
}

public static class Ease
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Easing(EaseType type, float t)
    {
        switch (type)
        {
            case EaseType.InCirc: return InCirc(t);
            case EaseType.OutCirc: return OutCirc(t);
            case EaseType.InOutCirc: return InOut(InCirc, OutCirc, t);
            case EaseType.OutInCirc: return OutIn(OutCirc, InCirc, t);

            case EaseType.InCubic: return InCubic(t);
            case EaseType.OutCubic: return OutCubic(t);
            case EaseType.InOutCubic: return InOut(InCubic, OutCubic, t);
            case EaseType.OutInCubic: return OutIn(OutCubic, InCubic, t);

            case EaseType.InBack: return InBack(t);
            case EaseType.OutBack: return OutBack(t);
            case EaseType.InOutBack: return InOut(InBack, OutBack, t);
            case EaseType.OutInBack: return OutIn(OutBack, InBack, t);

            case EaseType.InQuint: return InQuint(t);
            case EaseType.OutQuint: return OutQuint(t);
            case EaseType.InOutQuint: return InOut(InQuint, OutQuint, t);
            case EaseType.OutInQuint: return OutIn(OutQuint, InQuint, t);

            case EaseType.InExpo: return InExpo(t);
            case EaseType.OutExpo: return OutExpo(t);
            case EaseType.InOutExpo: return InOut(InExpo, OutExpo, t);
            case EaseType.OutInExpo: return OutIn(OutExpo, InExpo, t);

            case EaseType.InSine: return InSine(t);
            case EaseType.OutSine: return OutSine(t);
            case EaseType.InOutSine: return InOut(InSine, OutSine, t);
            case EaseType.OutInSine: return OutIn(OutSine, InSine, t);

            case EaseType.InBounce: return InBounce(t);
            case EaseType.OutBounce: return OutBounce(t);
            case EaseType.InOutBounce: return InOut(InBounce, OutBounce, t);
            case EaseType.OutInBounce: return OutIn(OutBounce, InBounce, t);

            case EaseType.InQuad: return InQuad(t);
            case EaseType.OutQuad: return OutQuad(t);
            case EaseType.InOutQuad: return InOutQuad(t);
            case EaseType.OutInQuad: return OutIn(OutQuad, InQuad, t);

            case EaseType.InQuart: return InQuart(t);
            case EaseType.OutQuart: return OutQuart(t);
            case EaseType.InOutQuart: return InOutQuart(t);
            case EaseType.OutInQuart: return OutIn(OutQuart, InQuart, t);

            case EaseType.InElastic: return InElastic(t);
            case EaseType.OutElastic: return OutElastic(t);
            case EaseType.InOutElastic: return InOutElastic(t);
            case EaseType.OutInElastic: return OutIn(OutElastic, InElastic, t);

            case EaseType.Linear:
            default: return t;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InOut(Func<float, float> inFunc, Func<float, float> outFunc, float t) => t < 0.5f ? inFunc(t * 2) * 0.5f : outFunc(t * 2 - 1) * 0.5f + 0.5f;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float OutIn(Func<float, float> outFunc, Func<float, float> inFunc, float t) => t < 0.5f ? outFunc(t * 2) * 0.5f : inFunc(t * 2 - 1) * 0.5f + 0.5f;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InCirc(float t) => 1 - Mathf.Sqrt(1 - t * t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float OutCirc(float t) => Mathf.Sqrt(1 - (t - 1) * (t - 1));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InCubic(float t) => t * t * t;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float OutCubic(float t) => (t - 1) * (t - 1) * (t - 1) + 1;

    public const float BackS = 1.70158f;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InBack(float t) => t * t * ((BackS + 1) * t - BackS);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float OutBack(float t) => (--t) * t * ((BackS + 1) * t + BackS) + 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InQuint(float t) => t * t * t * t * t;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float OutQuint(float t) => 1 - Mathf.Pow(1 - t, 5);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InExpo(float t) => t == 0 ? 0 : Mathf.Pow(2, 10 * (t - 1));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float OutExpo(float t) => t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InSine(float t) => 1 - Mathf.Cos(t * Mathf.PI / 2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float OutSine(float t) => Mathf.Sin(t * Mathf.PI / 2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InBounce(float t) => 1 - OutBounce(1 - t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float OutBounce(float t)
    {
        if (t < (1 / 2.75f)) return 7.5625f * t * t;
        if (t < (2 / 2.75f)) return 7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f;
        if (t < (2.5 / 2.75f)) return 7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f;
        return 7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f;
    }
    // Quad
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InQuad(float t) => t * t;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float OutQuad(float t) => 1 - (1 - t) * (1 - t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InOutQuad(float t) =>
        t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;

    // Quart
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InQuart(float t) => t * t * t * t;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float OutQuart(float t) => 1 - Mathf.Pow(1 - t, 4);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InOutQuart(float t) =>
        t < 0.5f ? 8 * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 4) / 2;

    // Elastic
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InElastic(float t)
    {
        if (t == 0) return 0;
        if (t == 1) return 1;
        return -Mathf.Pow(2, 10 * t - 10) * Mathf.Sin((t * 10 - 10.75f) * (2 * Mathf.PI / 3));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float OutElastic(float t)
    {
        if (t == 0) return 0;
        if (t == 1) return 1;
        return Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10 - 0.75f) * (2 * Mathf.PI / 3)) + 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InOutElastic(float t)
    {
        if (t == 0) return 0;
        if (t == 1) return 1;
        t *= 2;
        float c = (2 * Mathf.PI) / 4.5f;
        if (t < 1)
            return -0.5f * Mathf.Pow(2, 10 * (t - 1)) * Mathf.Sin((t * 10 - 10.75f) * c);
        return Mathf.Pow(2, -10 * (t - 1)) * Mathf.Sin((t * 10 - 10.75f) * c) * 0.5f + 1;
    }
}
