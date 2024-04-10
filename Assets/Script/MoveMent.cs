using UnityEngine;

public class MoveMent : MonoBehaviour
{
    public EaseMaster EaseMaster;
    public ButtonMaster ButtonMaster;

    public int NeedMode;
    public Vector2 start;
    public Vector2 Target;


    private float Time;
    private Vector2 NowPos;
    private Vector2 TargetPos;

    private Vector2 Result;
    private bool IsActive;
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

        if(IsRect)
            NowPos = RectTransform.localPosition;
        else
            NowPos = transform.position;

        if(ButtonMaster.Mode != NeedMode){
            TargetPos = Target;
            IsActive = false;
        }
        else{
            TargetPos = start;
            IsActive = true;
        }
    }

    void Update()
    {
        Time +=0.01f;
        if(IsRect){
            if(!IsActive&&ButtonMaster.Mode != NeedMode){ //원래 위치
                Time = 0;
                NowPos = RectTransform.localPosition;
                TargetPos = start;
                IsActive = true;

            }else if(IsActive&&ButtonMaster.Mode == NeedMode){ //움직인 위치
                Time = 0; 
                NowPos = RectTransform.localPosition;
                TargetPos = Target;
                IsActive = false;
            }
        }else{
            if(!IsActive&&ButtonMaster.Mode != NeedMode){ //원래 위치
                Time = 0;
                NowPos = Transform.position;
                TargetPos = start;
                IsActive = true;

            }else if(IsActive&&ButtonMaster.Mode == NeedMode){ //움직인 위치
                Time = 0; 
                NowPos = Transform.position;
                TargetPos = Target;
                IsActive = false;
            }
        }

        Result = Vector2.Lerp(NowPos, TargetPos,EaseMaster.OutQuint(Time));

        if(IsRect)
            RectTransform.localPosition = Result;
        else
            transform.position = Result;
        

    }
}
