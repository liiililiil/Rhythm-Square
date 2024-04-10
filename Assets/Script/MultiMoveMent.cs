using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiMoveMent : MonoBehaviour
{
    public ButtonMaster ButtonMaster;
    public EaseMaster EaseMaster;
    public Vector2[] Pos;
    public int[] NeedMode;
    private Vector2 Result;
    private float Time;
    private int ModeRecord;
    private Vector2 NowPos;
    private Vector2 TargetPos;
    private Vector2 SpawnPos;
    private bool IsRect;
    private RectTransform RectTransform;
    private Transform Transform;

    void Start()
    {
        if(GetComponent<RectTransform>() != null){
            RectTransform = GetComponent<RectTransform>();
            IsRect = true;

        }else{
            Transform = GetComponent<Transform>();
            IsRect = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsRect){
        for(int i = 0; i<Pos.Length; i++)
            if(NeedMode[i] == ButtonMaster.Mode && ModeRecord != NeedMode[i]){
                ModeRecord = NeedMode[i];
                NowPos = RectTransform.position;
                TargetPos = Pos[i];
                Time =0;
            }
        }else{
            for(int i = 0; i<Pos.Length; i++)
            if(NeedMode[i] == ButtonMaster.Mode && ModeRecord != NeedMode[i]){
                ModeRecord = NeedMode[i];
                NowPos = Transform.position;
                TargetPos = Pos[i];
                Time =0;
            }
        }

        


        Time += 0.01f;
        Result = Vector2.Lerp(NowPos,TargetPos,EaseMaster.OutQuint(Time));
        
        if(IsRect)
        RectTransform.position = Result;
        else
        transform.position = Result;
        }
}
