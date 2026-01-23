using System.Collections;
using UnityEngine;

namespace Utils
{
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
        public static void SafeStartCoroutine(this MonoBehaviour mb, Coroutine coroutine, IEnumerator enumerator)
        {
            mb.SafeStopCoroutine(coroutine);

            coroutine = mb.StartCoroutine(enumerator);
        }

        // 코루틴 멈추기
        public static void SafeStopCoroutine(this MonoBehaviour mb, Coroutine coroutine)
        {
            if(coroutine != null)
            {
                mb.StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
    
}
