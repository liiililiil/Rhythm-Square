using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Utils
{

    public static class CameraUtils
    {
        public static Vector2 GetMousePosition()
        {
            Vector3 mouse = Input.mousePosition;
            return Camera.main.ScreenToWorldPoint(mouse);
        }
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
        public static float LookAt2d(Vector2 a, Vector2 b)
        {
            Vector2 d = b - a;

            return Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        }
    }

    public static class FloatUtils
    {

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

    }

    public static class CoroutineExtensions
    {
        // 이전 코루틴을 멈추고 새 코루틴 시작
        public static void SafeStartCoroutine(this MonoBehaviour mb, ref Coroutine coroutine, IEnumerator enumerator)
        {
            mb.SafeStopCoroutine(ref coroutine);

            coroutine = mb.StartCoroutine(enumerator);
        }

        // 코루틴 멈추기
        public static void SafeStopCoroutine(this MonoBehaviour mb, ref Coroutine coroutine)
        {
            if(coroutine != null)
            {
                mb.StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        // 코루틴을 검사하여 코루틴이 비어있는 경우만 시작
        public static bool CheckedStartCoroutine(this MonoBehaviour mb, ref Coroutine coroutine, IEnumerator enumerator)
        {
            if(coroutine != null) return false;

            coroutine = mb.StartCoroutine(enumerator);
            return true;
        }
    }

    public static class AddressableUtils
    {
        public static void SafeRelease(AsyncOperationHandle asyncOperationHandle)
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


