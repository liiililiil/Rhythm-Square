using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class InfoImg : MonoBehaviour
{
    public GameObject Master;
    public int i;
    private bool IsMove;
    private bool IsFirstMove;
    private Image Image;
    private int SelectRecord;
    private float Time;
    private Vector2 TargetPos;
    private Vector2 StartPos;
    private Vector2 Result;
    private RectTransform RectTransform;
    private SoundMaster SoundMaster;
    private ButtonMaster ButtonMaster;
    private EaseMaster EaseMaster;

    void Start(){
        //마스터 불러오기
        SoundMaster = Master.GetComponent<SoundMaster>();
        ButtonMaster = Master.GetComponent<ButtonMaster>();
        EaseMaster = Master.GetComponent<EaseMaster>();

        RectTransform = GetComponent<RectTransform>();
        SelectRecord = -1;
        Image = GetComponent<Image>();
        Image.sprite = SoundMaster.Song[i].TitleSprite;


    }
    void Update()
    {
        if(ButtonMaster.Mode == 3){ //노래 선택시 움직임
        IsMove = false;

            if(Image.color.a <= 1f) //보이게
                    Image.color = Image.color + new Color(0,0,0,0.1f);

            if(!IsFirstMove){ //첫움직임 
                IsFirstMove = true;
                Time = 0;
                StartPos = RectTransform.localPosition;
                TargetPos = new Vector2(0,250 * (i-ButtonMaster.InGameSelect));
                SelectRecord = ButtonMaster.InGameSelect;
            }


            if(ButtonMaster.InGameSelect != SelectRecord){
                Time = 0;
                StartPos = RectTransform.localPosition;
                TargetPos = new Vector2(0,250 * (i-ButtonMaster.InGameSelect));
                SelectRecord = ButtonMaster.InGameSelect;
            }

        }else{

            if(Image.color.a >= 0 &&ButtonMaster.InGameSelect == i ) //안보이게
                    if(ButtonMaster.Mode == 4 && ButtonMaster.ModeRecord == 3 ) {
                        if(Time >= 1) Image.color = Image.color - new Color(0,0,0,0.015f);
                    }else{
                        Image.color = Image.color - new Color(0,0,0,0.1f);
                    }

            IsFirstMove = false;


            if(!IsMove &&ButtonMaster.InGameSelect == i){ //가운데있을때
                Time = 0;
                if(ButtonMaster.Mode != 4 ){
                StartPos = RectTransform.localPosition;
                TargetPos = new Vector2(-250,0);
                }else{
                StartPos = RectTransform.localPosition;
                TargetPos = new Vector2(0,0);
                }
                IsMove = true;

            }else if(!IsMove &&ButtonMaster.InGameSelect != i){ //위, 아래에 있을때
                Time = 0;
                StartPos = RectTransform.localPosition;
                if((ButtonMaster.InGameSelect - i) < 0)
                    TargetPos = new Vector2(0,250 +250* -(ButtonMaster.InGameSelect - i));
                
                if((ButtonMaster.InGameSelect - i) > 0)
                    TargetPos = new Vector2(0,-250 -(250* (ButtonMaster.InGameSelect - i)));

                IsMove = true;
            }

            if(Time == 1 && ButtonMaster.ModeRecord == 3){
                StartPos = RectTransform.localPosition;
                TargetPos = new Vector2(-250,0);
            }

        }

        Time += 0.01f; //움직임 최종 적용
        if(Time >= 1 && ButtonMaster.ModeRecord == 3 && ButtonMaster.Mode == 4 &&ButtonMaster.InGameSelect == i)Result = Vector2.Lerp(new Vector2(0,0), new Vector2(-250,0),EaseMaster.InQuint(Time-1));
        else Result = Vector2.Lerp(StartPos, TargetPos,EaseMaster.OutQuint(Time));
        RectTransform.localPosition = Result - new Vector2(0,ButtonMaster.InGameSelectDump*200);
    }
}
