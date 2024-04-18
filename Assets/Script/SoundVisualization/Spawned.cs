using System;
using Unity.Mathematics;
using UnityEngine;

public class Spawned : GetMasters
{
    public int Select;
    private float TargetSize; 
    public float SpectrumData;
    void Update()
    {
        SpectrumData = (float)Math.Pow(SoundMaster.SpectrumData[Select]*(10+Select*0.2),1.1);
        //SpectrumData = SoundMaster.SpectrumData[Select]*(5+Select*0.2f);
        if(SpectrumData <= transform.localScale.y-0.1f )
            TargetSize = TargetSize - 0.1f;
        else if(SpectrumData <= 1f)
            TargetSize = SpectrumData * 0.8f;
        else if(SpectrumData <= 4f)
            TargetSize = SpectrumData;
        else
            TargetSize = 4 +  Mathf.Log(SpectrumData - 4,100);

        if(TargetSize <= 0.01f)
        TargetSize = 0.01f; //최소 크기

        transform.localScale = new Vector2(transform.localScale.x,TargetSize);// 크기적용
    }
}
