using System;
using System.Runtime.CompilerServices;

using UnityEngine;

namespace SimpleEasing
{
    [Serializable]
    public enum EaseType : byte
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
        private const float BackS = 1.70158f;

        /// <summary>
        /// Eased the value with the EaseType function.
        /// </summary>
        /// <param name="value">Recommended between 0 to 1</param>
        /// <param name="easeType">Easing function Type</param>
        /// <returns>Eased value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Easing(float value, EaseType easeType)
        {
            switch (easeType)
            {
                case EaseType.InCirc: return InCirc(value);
                case EaseType.OutCirc: return OutCirc(value);
                case EaseType.InOutCirc: return InOut(InCirc, OutCirc, value);
                case EaseType.OutInCirc: return OutIn(OutCirc, InCirc, value);

                case EaseType.InCubic: return InCubic(value);
                case EaseType.OutCubic: return OutCubic(value);
                case EaseType.InOutCubic: return InOut(InCubic, OutCubic, value);
                case EaseType.OutInCubic: return OutIn(OutCubic, InCubic, value);

                case EaseType.InBack: return InBack(value);
                case EaseType.OutBack: return OutBack(value);
                case EaseType.InOutBack: return InOut(InBack, OutBack, value);
                case EaseType.OutInBack: return OutIn(OutBack, InBack, value);

                case EaseType.InQuint: return InQuint(value);
                case EaseType.OutQuint: return OutQuint(value);
                case EaseType.InOutQuint: return InOut(InQuint, OutQuint, value);
                case EaseType.OutInQuint: return OutIn(OutQuint, InQuint, value);

                case EaseType.InExpo: return InExpo(value);
                case EaseType.OutExpo: return OutExpo(value);
                case EaseType.InOutExpo: return InOut(InExpo, OutExpo, value);
                case EaseType.OutInExpo: return OutIn(OutExpo, InExpo, value);

                case EaseType.InSine: return InSine(value);
                case EaseType.OutSine: return OutSine(value);
                case EaseType.InOutSine: return InOut(InSine, OutSine, value);
                case EaseType.OutInSine: return OutIn(OutSine, InSine, value);

                case EaseType.InBounce: return InBounce(value);
                case EaseType.OutBounce: return OutBounce(value);
                case EaseType.InOutBounce: return InOut(InBounce, OutBounce, value);
                case EaseType.OutInBounce: return OutIn(OutBounce, InBounce, value);

                case EaseType.InQuad: return InQuad(value);
                case EaseType.OutQuad: return OutQuad(value);
                case EaseType.InOutQuad: return InOutQuad(value);
                case EaseType.OutInQuad: return OutIn(OutQuad, InQuad, value);

                case EaseType.InQuart: return InQuart(value);
                case EaseType.OutQuart: return OutQuart(value);
                case EaseType.InOutQuart: return InOutQuart(value);
                case EaseType.OutInQuart: return OutIn(OutQuart, InQuart, value);

                case EaseType.InElastic: return InElastic(value);
                case EaseType.OutElastic: return OutElastic(value);
                case EaseType.InOutElastic: return InOutElastic(value);
                case EaseType.OutInElastic: return OutIn(OutElastic, InElastic, value);

                case EaseType.Linear: return value;

                default: return value;
            }
        }

        #region InOut / OutIn Helpers
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float InOut(Func<float, float> inFunc, Func<float, float> outFunc, float value) =>
            value < 0.5f ? inFunc(value * 2) * 0.5f : outFunc(value * 2 - 1) * 0.5f + 0.5f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float OutIn(Func<float, float> outFunc, Func<float, float> inFunc, float value) =>
            value < 0.5f ? outFunc(value * 2) * 0.5f : inFunc(value * 2 - 1) * 0.5f + 0.5f;
        #endregion

        #region Base Easing Functions
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float InCirc(float value) => 1 - Mathf.Sqrt(1 - value * value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float OutCirc(float value) => Mathf.Sqrt(1 - (value - 1) * (value - 1));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float InCubic(float value) => value * value * value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float OutCubic(float value) => (value - 1) * (value - 1) * (value - 1) + 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float InBack(float value) => value * value * ((BackS + 1) * value - BackS);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float OutBack(float value) { value--; return value * value * ((BackS + 1) * value + BackS) + 1; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float InQuint(float value) => value * value * value * value * value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float OutQuint(float value) { float inv = 1 - value; return 1 - inv * inv * inv * inv * inv; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float InExpo(float value) => value == 0 ? 0 : Mathf.Pow(2, 10 * (value - 1));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float OutExpo(float value) => value == 1 ? 1 : 1 - Mathf.Pow(2, -10 * value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float InSine(float value) => 1 - Mathf.Cos(value * Mathf.PI * 0.5f);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float OutSine(float value) => Mathf.Sin(value * Mathf.PI * 0.5f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float InBounce(float value) => 1 - OutBounce(1 - value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float OutBounce(float value)
        {
            if (value < (1 / 2.75f)) return 7.5625f * value * value;
            if (value < (2 / 2.75f)) { value -= (1.5f / 2.75f); return 7.5625f * value * value + 0.75f; }
            if (value < (2.5f / 2.75f)) { value -= (2.25f / 2.75f); return 7.5625f * value * value + 0.9375f; }
            value -= (2.625f / 2.75f); return 7.5625f * value * value + 0.984375f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float InQuad(float value) => value * value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float OutQuad(float value) => 1 - (1 - value) * (1 - value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float InOutQuad(float value) => value < 0.5f ? 2 * value * value : 1 - Mathf.Pow(-2 * value + 2, 2) * 0.5f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float InQuart(float value) => value * value * value * value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float OutQuart(float value) { float inv = 1 - value; return 1 - inv * inv * inv * inv; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static float InOutQuart(float value) => value < 0.5f ? 8 * value * value * value * value : 1 - Mathf.Pow(-2 * value + 2, 4) * 0.5f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float InElastic(float value)
        {
            if (value == 0) return 0;
            if (value == 1) return 1;
            return -Mathf.Pow(2, 10 * value - 10) * Mathf.Sin((value * 10 - 10.75f) * (2 * Mathf.PI / 3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float OutElastic(float value)
        {
            if (value == 0) return 0;
            if (value == 1) return 1;
            return Mathf.Pow(2, -10 * value) * Mathf.Sin((value * 10 - 0.75f) * (2 * Mathf.PI / 3)) + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float InOutElastic(float value)
        {
            if (value == 0) return 0;
            if (value == 1) return 1;
            value *= 2;
            float c = (2 * Mathf.PI) / 4.5f;
            if (value < 1) return -0.5f * Mathf.Pow(2, 10 * (value - 1)) * Mathf.Sin((value * 10 - 10.75f) * c);
            return Mathf.Pow(2, -10 * (value - 1)) * Mathf.Sin((value * 10 - 10.75f) * c) * 0.5f + 1;
        }
        #endregion
    }
}
