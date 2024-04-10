using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseMaster : MonoBehaviour
{
    public float InCirc(float t){
        return 1 - Mathf.Sqrt(1 - Mathf.Pow(t, 2));
    }
    public float OutCirc(float t){
        return Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
    }

    public float InBack(float t){
        float c1 = 1.70158f;
        float c3 = c1 + 1f;
    return c3 * t * t * t - c1 * t * t;
    }

    public float InQuint(float t)
    {
        return Mathf.Pow(t, 5);
    }

    public float OutQuint(float t)
    {
        return 1 - Mathf.Pow(1 - t, 5);
    }

    public float OutExpo(float t)
    {
         return t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);
    }
}
