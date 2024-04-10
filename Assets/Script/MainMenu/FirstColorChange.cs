using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstColorChange : MonoBehaviour
{
    public float Time;
    private Camera Camera;
    private Color StartColor = new Color(35f / 255f, 31f / 255f, 32f / 255f, 0f);
    private Color EndColor = new Color(29f / 255f, 29f / 255f, 29f / 255f, 0f);
    void Awake(){
        Camera = GetComponent<Camera>();
        Time = 0;
    }
    void Update()
    {
        Time += 0.01f;

        Camera.backgroundColor = Color.Lerp(StartColor,EndColor,Time);
    }
}
