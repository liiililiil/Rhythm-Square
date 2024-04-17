using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveMent : InGameMaster
{
    public SoundMaster SoundMaster;
    public SkinMaster SkinMaster;
    public ButtonMaster ButtonMaster;
    private SpriteRenderer SpriteRenderer;
    private float TargetX;
    private float TargetY;
    private Vector2 LockPos;
    static public bool BeShift;
    static public bool Filp;
    
    private bool IsActive = true;
    private float Speed; 




    
    void Start()

    {
        LockPos.x = 0;
        LockPos.y = 0;        
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        LockPosFix(); //언락 < - > 락 변환시 좌표 고정

        if(ButtonMaster.Mode == 4){
            PlayerMoveRange(); //플레이어 이동반경 제한

            if(IsLock){
                PushFilpKey(); //플레이어 락시 스킬준비감지

                if(Filp) 
                    Filp_(); //플레이어 락시 스킬발동감지
                else
                    LockMoveMent(); //플레이어 락시 이동
                    AnimatedMoveMent(); //플레이어 락시 이동 애니메이션

            }else{

                SlowMoveMent(); //플레이어 언락시 스킬키 감지
                UnLockMoveMent(); //플레이어 언락시 이동

            }

            transform.localPosition = new Vector2(TargetX,TargetY);        
            
        }

        SpriteRenderer.sprite = SkinMaster.Player[SkinMaster.PlayerSelect];
    }
    void LockPosFix(){ // 언락 < - > 락 변환시 좌표 고정
        if(IsLock && IsActive){
            BeShift = false;
            IsActive = false;
            Filp = false;
            LockPos.x = (int)Mathf.Round(TargetX);
            LockPos.y = (int)Mathf.Round(TargetY);
            transform.position = new Vector2(TargetX,TargetY);

            }else if(!IsLock && !IsActive)
                IsActive = true;
    }

    void Filp_(){ // 플립 적용
        if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow)){ // y플립
                LockPos.y = LockPos.y *-1; 
                TargetY= TargetY*-1;
                Filp = false;

            }else if(Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow)){ // x플립
                LockPos.x = LockPos.x *-1;
                TargetX= TargetX*-1;
                Filp = false;

            }
    }
    
    void PushFilpKey(){ //를립 키 적용
        if(Input.GetKeyDown(KeyCode.LeftShift)||Input.GetKeyDown(KeyCode.RightShift)||Input.GetKeyDown(KeyCode.LeftControl)||Input.GetKeyDown(KeyCode.RightControl)){
            Filp = true;
            SoundMaster.PlayShiftSound(true);
        }else if(Input.GetKeyUp(KeyCode.LeftShift)||Input.GetKeyUp(KeyCode.RightShift)||Input.GetKeyDown(KeyCode.LeftControl)||Input.GetKeyDown(KeyCode.RightControl)){
            Filp = false;
            SoundMaster.PlayShiftSound(false);
        }
    }

    void SlowMoveMent(){ //걷기 적용
        if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift)||Input.GetKeyDown(KeyCode.LeftControl)||Input.GetKeyDown(KeyCode.RightControl)){
            SoundMaster.PlayShiftSound(true);
            Speed = 0.075f;
            BeShift = true;
        }else{
            SoundMaster.PlayShiftSound(false);
            Speed = 0.125f;
            BeShift = false;
        }
    }
    
    void AnimatedMoveMent(){ //락일때 애니메이션
        if( TargetX < LockPos.x-0.25f)
            TargetX= TargetX + 0.25f; else

        if( TargetX > LockPos.x+0.25f)
            TargetX= TargetX - 0.25f; else
        TargetX=LockPos.x;

        if( TargetY < LockPos.y-0.25f)
            TargetY= TargetY+ 0.25f; else

        if( TargetY > LockPos.y+0.25f) 
            TargetY= TargetY- 0.25f; else  
        TargetY=LockPos.y;
    }

    void PlayerMoveRange(){ //움직임 한계
        if(LockPos.y > (TileSize.y-1) /2) LockPos.y = (TileSize.y-1) /2;
        if(LockPos.y < (TileSize.y-1) /2 *-1) LockPos.y = (TileSize.y-1) /2 *-1;
        if(LockPos.x < (TileSize.x-1) /2 *-1) LockPos.x = (TileSize.x-1) /2 *-1;
        if(LockPos.x > (TileSize.y-1) /2) LockPos.x = (TileSize.y-1) /2;
        if( TargetX < (BorderSize.x-1) /2 *-1)TargetX= (BorderSize.x-1) /2 *-1;
        if( TargetX > (BorderSize.x-1) /2) TargetX= (BorderSize.x-1) /2;
        if( TargetY < (BorderSize.y-1) /2 *-1) TargetY=(BorderSize.y-1) /2 *-1;
        if( TargetY > (BorderSize.y-1) /2) TargetY=(BorderSize.y-1) /2;
    }
    
    void UnLockMoveMent(){ //언락 움직임
        if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow))
            if( TargetY < (BorderSize.y-1) /2) 
                TargetY= TargetY+Speed;

        if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)) 
            if( TargetY > (BorderSize.y-1) /2 *-1)
                TargetY= TargetY-Speed;

        if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
            if( TargetX > (BorderSize.x-1) /2 *-1) 
                TargetX= TargetX-Speed;

        if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
            if( TargetX < (BorderSize.x-1) /2)
                TargetX= TargetX+Speed;
    }

    void LockMoveMent(){ //락 움직임 | wasd와 화살표 동시에 눌르면 2칸이동하기위해 분리함.
        if(Input.GetKeyDown(KeyCode.UpArrow))
            if(LockPos.y < (TileSize.y-1) /2) 
                ++LockPos.y;
        if(Input.GetKeyDown(KeyCode.W))
            if(LockPos.y < (TileSize.y-1) /2) 
                ++LockPos.y;
        

        if(Input.GetKeyDown(KeyCode.S))
            if(LockPos.y > (TileSize.y-1) /2 *-1)
                --LockPos.y;
        if(Input.GetKeyDown(KeyCode.DownArrow))
            if(LockPos.y > (TileSize.y-1) /2 *-1)
                --LockPos.y;


        if(Input.GetKeyDown(KeyCode.A))
            if(LockPos.x > (TileSize.x-1) /2 *-1) 
                --LockPos.x;
        if(Input.GetKeyDown(KeyCode.LeftArrow))
            if(LockPos.x > (TileSize.x-1) /2 *-1) 
                --LockPos.x;
        
        if(Input.GetKeyDown(KeyCode.D))
            if(LockPos.x < (TileSize.y-1) /2) 
                ++LockPos.x;
        if(Input.GetKeyDown(KeyCode.RightArrow))
            if(LockPos.x < (TileSize.y-1) /2) 
                ++LockPos.x;
    }
}
    

