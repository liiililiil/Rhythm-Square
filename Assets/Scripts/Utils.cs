using System.Collections;
using UnityEngine;

namespace Utils
{
    public static class Temps{
        /// <summary>
        /// 임시!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// 메뉴의 비트 당 시간
        /// </summary>

        public const float BPM_TO_SEC = 60f / 130f;
    }

    public static class Vector2Utils
    {
        public static Vector2 FloatToVector2(float value)
        {
            return new Vector2(value,value);
        }

        public static Vector3 FloatToVector3(float value)
        {
            return new Vector3(value, value, value);
        }
    }

    public static class RectTransformExtensions
    {
        public static void SetTopBottom(this RectTransform rt, float top, float bottom)
        {
            Vector2 min = rt.offsetMin;
            Vector2 max = rt.offsetMax;

            min.y = bottom;
            max.y = -top;

            rt.offsetMin = min;
            rt.offsetMax = max;
        }

        public static void SetPosX(this RectTransform rt, float x)
        {
            Vector2 pos = rt.anchoredPosition;
            pos.x = x;
            rt.anchoredPosition = pos;
        }
    }

    public static class CoroutineExtensions
    {
        // 이전 코루틴을 멈추고 새 코루틴 시작
        public static void SafeStartCoroutine(this MonoBehaviour mb,ref Coroutine coroutine, IEnumerator enumerator)
        {
            mb.SafeStopCoroutine(ref coroutine);

            coroutine = mb.StartCoroutine(enumerator);
        }

        // 코루틴 멈추기
        public static void SafeStopCoroutine(this MonoBehaviour mb,ref Coroutine coroutine)
        {
            if(coroutine != null)
            {
                mb.StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
    
}
