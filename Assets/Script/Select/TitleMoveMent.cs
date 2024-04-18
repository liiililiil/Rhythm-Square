using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMoveMent : GetMasters
{
    public Vector2 BasicStartPos;
    public Vector2 BasicEndPos;
    
    public Vector2 EndPos1;
    public Vector2 EndPos2;
    private Vector2 NowPos;
    private float Time;
    private RectTransform RectTransform;    
    private bool BasicIsActive;
    private bool IsActive;
    private Vector2 EndPos;
    private Vector2 StartPos;
    private Text Text;

    void Start(){
        RectTransform = GetComponent<RectTransform>();
        Text = GetComponent<Text>();
    }
    void Update()
    {
        Time += 0.01f;

        if(ButtonMaster.Mode == 4 && ButtonMaster.ModeRecord == 3 && IsActive){ //게임시작일때 위치 지정
            IsActive = false;
            Time = 0;
            StartPos = RectTransform.localPosition;
            Text.alignment = TextAnchor.MiddleCenter;
        }else if(ButtonMaster.Mode != 4 && !IsActive){ //스위치
            IsActive = true;
            Time = 0;
            StartPos = RectTransform.localPosition;
            Text.alignment = TextAnchor.MiddleLeft;
        }

        if(ButtonMaster.Mode == 3 && BasicIsActive){ //일반상황에서 애니메이션 위치 지정
            BasicIsActive = false;
            Time = 0;
            StartPos = RectTransform.localPosition;
            EndPos = BasicEndPos;
            
        }else if(ButtonMaster.Mode != 3 && ButtonMaster.Mode != 4 && !BasicIsActive){ //스위치
            BasicIsActive = true;
            Time = 0;
            StartPos = RectTransform.localPosition;
            EndPos = BasicStartPos;
            
        }

        if(!IsActive){
            if(Time <= 1)
                NowPos = Vector2.Lerp(StartPos, EndPos1, EaseMaster.OutQuint(Time)); //게임시작일때 애니메이션 재생
            else if(Time <= 2)
                NowPos = Vector2.Lerp(EndPos1 ,EndPos2, EaseMaster.InCirc(Time-1)); 
            else
                NowPos = BasicStartPos; //재생완료후 위치 복구

        }else{//일반상황에서 애니메이션
            NowPos = Vector2.Lerp(StartPos, EndPos, EaseMaster.OutQuint(Time));
        }


        if(!float.IsNaN(NowPos.x)||!float.IsNaN(NowPos.y)) //최종 움직임
            RectTransform.localPosition = NowPos;
    }
}
