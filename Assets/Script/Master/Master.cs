using UnityEngine;
using System;

public class Master : MonoBehaviour
{
    void Update(){ //갈비지 스파이크 해결
        if (Time.frameCount % 180 == 0)
            System.GC.Collect();
    }
}
