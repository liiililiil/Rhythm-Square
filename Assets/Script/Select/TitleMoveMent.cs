using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMoveMent : MonoBehaviour
{
    public ButtonMaster ButtonMaster;
    public EaseMaster EaseMaster;
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

        if(ButtonMaster.Mode == 4 && ButtonMaster.ModeRecord == 3 && IsActive){
            IsActive = false;
            Time = 0;
            StartPos = RectTransform.localPosition;
        }else if(ButtonMaster.Mode != 4 && !IsActive){
            IsActive = true;
            Time = 0;
            StartPos = RectTransform.localPosition;
        }

        if(ButtonMaster.Mode == 3 && BasicIsActive){
            BasicIsActive = false;
            Time = 0;
            StartPos = RectTransform.localPosition;
            EndPos = BasicEndPos;
            
        }else if(ButtonMaster.Mode != 3 && ButtonMaster.Mode != 4 && !BasicIsActive){
            BasicIsActive = true;
            Time = 0;
            StartPos = RectTransform.localPosition;
            EndPos = BasicStartPos;
            
        }

        if(!IsActive){
            if(Time <= 1)
                NowPos = Vector2.Lerp(StartPos, F1(EndPos1), EaseMaster.OutCirc(Time));
            else if(Time <= 2)
                NowPos = Vector2.Lerp(F1(EndPos1) , F1(EndPos2), EaseMaster.InCirc(Time-1));
            else
                NowPos = BasicStartPos;

        }else{
            NowPos = Vector2.Lerp(StartPos, EndPos, EaseMaster.OutQuint(Time));
        }


        if(!float.IsNaN(NowPos.x)||!float.IsNaN(NowPos.y))
            RectTransform.localPosition = NowPos;
    }

    Vector2 F1(Vector2 i){
        return i - new Vector2((Text.text.Length-1) * Text.fontSize/2,0);
    }
}
