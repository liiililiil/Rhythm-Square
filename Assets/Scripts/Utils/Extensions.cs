using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Extensions
{
    public static class TextExtensions
    {
        public static void SetText(this Text text, string value)
        {
            text.text = value;
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

        public static Vector2 GetRectInCanvas(this RectTransform rt, RectTransform canvas)
        {
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);

            Vector2 min = canvas.InverseTransformPoint(corners[0]);
            Vector2 max = canvas.InverseTransformPoint(corners[2]);

            return (min + max) * 0.5f;
        }

        public static void SetAnchoredPosition(this RectTransform rt, Vector2 value)
        {
            rt.anchoredPosition = value;
        }

        public static void SetLocalScale(this RectTransform rt, Vector2 value)
        {
            rt.localScale = value;
        }

        public static void SetRotation(this RectTransform rt, Quaternion value)
        {
            rt.rotation = value;
        }

        public static void SetRotation(this RectTransform rt, float value)
        {
            rt.rotation = Quaternion.Euler(0, 0, value);
        }
    }
    public static class MonobehaviourExtensions
    {
        public static void SafeStartCoroutine(this MonoBehaviour mb, ref Coroutine coroutine, IEnumerator enumerator)
        {
            mb.SafeStopCoroutine(ref coroutine);

            coroutine = mb.StartCoroutine(enumerator);
        }

        public static void SafeStopCoroutine(this MonoBehaviour mb, ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                mb.StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        public static bool CheckedStartCoroutine(this MonoBehaviour mb, ref Coroutine coroutine, IEnumerator enumerator)
        {
            if (coroutine != null) return false;

            coroutine = mb.StartCoroutine(enumerator);
            return true;
        }
    }

    public static class AddressableExtensions
    {
        public static void SafeRelease<_T1>(this ref AsyncOperationHandle<_T1> asyncOperationHandle)
        {
            if (asyncOperationHandle.IsValid())
            {
                asyncOperationHandle.Release();
                asyncOperationHandle = default;
            }
            else
            {
                Debug.LogError("이 핸들은 아직 로딩중입니다!");
            }
        }
    }
}

