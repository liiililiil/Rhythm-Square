using UnityEngine;

public class ButtonMaster : MonoBehaviour
{
    protected SoundMaster SoundMaster;
    protected SkinMaster SkinMaster;
    
    public int Mode; //현재 창
    public int ModeRecord;//창 기록
    /*
    0 : 메인화면
    1 : 종료경고
    2 : 설정메뉴
    3 : 곡선택메뉴
    4 : 인게임
    5 : 결과창
    */

    public static int InGameSelect;
    public static float InGameSelectDump; //마우스끌기로 적용되는 움직임 임시 저장
    private bool IsDrop; //마우스끌기
    private float StartY;
    private int SelectRecord;

    public void Awake(){
        SkinMaster = GetComponent<SkinMaster>();
        SoundMaster = GetComponent<SoundMaster>();
        SelectRecord = -1;
        ModeRecord = -1;
        Mode = 0;
        InGameSelect = 0;
    }

    public void Update(){
        if(ButtonMaster.InGameSelect+ Mathf.RoundToInt(ButtonMaster.InGameSelectDump) != SelectRecord){
            SoundMaster.SlowChange(SoundMaster.Song[ButtonMaster.InGameSelect+ Mathf.RoundToInt(ButtonMaster.InGameSelectDump)].Music,SoundMaster.Song[InGameSelect+ Mathf.RoundToInt(ButtonMaster.InGameSelectDump)].PreViewStart);
            SelectRecord = ButtonMaster.InGameSelect+ Mathf.RoundToInt(ButtonMaster.InGameSelectDump) ;
        }

        if(Input.GetMouseButtonDown(0) && Mode == 3 && !IsDrop ){
            IsDrop = true;
            StartY = Input.mousePosition.y;
        }else if(Input.GetMouseButtonUp(0) || Mode != 3){
            IsDrop = false;
            InGameSelect += Mathf.RoundToInt(InGameSelectDump); 
            InGameSelectDump =0;
        }

        if(IsDrop){ //노래선택끌기 감지
            if((StartY - Input.mousePosition.y)/200 + InGameSelect > -0.4 && (StartY - Input.mousePosition.y)/200 + InGameSelect < SoundMaster.Song.Length - 0.6)
                InGameSelectDump = (StartY - Input.mousePosition.y)/200;

            else if(!((StartY - Input.mousePosition.y)/200 + InGameSelect > -0.4))
                InGameSelectDump = -0.4f -InGameSelect; 

            else if(!((StartY - Input.mousePosition.y)/200 + InGameSelect < SoundMaster.Song.Length - 0.6))
                InGameSelectDump = SoundMaster.Song.Length - 0.6f - InGameSelect;
        }


        if((Input.GetKeyUp(KeyCode.Space)||Input.GetKeyUp(KeyCode.Return)) && Mode == 0)//메인화면 enter
            ChangeMode(3);

        if((Input.GetKeyUp(KeyCode.Space)||Input.GetKeyUp(KeyCode.Return)) && Mode == 1)//종료화면 enter
            Application.Quit();

        if((Input.GetKeyUp(KeyCode.Space)||Input.GetKeyUp(KeyCode.Return)) && Mode == 2)//설정메뉴 enter
            ChangeMode(0);

        if((Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.S)||Input.GetKeyUp(KeyCode.DownArrow)||Input.GetKeyUp(KeyCode.LeftArrow)) && Mode == 3)//선택화면 위
            MusicSelect(false);
        
        if((Input.GetKeyUp(KeyCode.D)||Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.UpArrow)||Input.GetKeyUp(KeyCode.RightArrow)) && Mode == 3)//선택화면 위
            MusicSelect(true);

        if(Input.GetKeyUp(KeyCode.Escape) && Mode != 0) //Esc돌아가기
            ChangeMode(ModeRecord);

        else if(Input.GetKeyUp(KeyCode.Escape) && Mode == 0)
            ChangeMode(1);
    }

    public void ChangeMode(int i){//메뉴 변경
        SoundMaster.AudioSource[1].Play();
            if(Mode != 1)
                ModeRecord = Mode; //이전 메뉴 기억
            Mode = i;
    }
    
    //버튼 이벤트

    public void QuitConfirm(){ //종료 결정
        Application.Quit();
    }

    public void ModeCancel(){ //종료 취소
        ChangeMode(ModeRecord);
    }

    public void PlayerSkin(bool IsRight){ //플레이어 스킨 선택
        if(IsRight){
            if(SkinMaster.PlayerSelect < SkinMaster.Player.Length-1){
                SkinMaster.PlayerSelect++;
            }
        }else{
            if(SkinMaster.PlayerSelect > 0){
                SkinMaster.PlayerSelect--;
            }
        }
    }

    public void TileSkin(bool IsRight){ //타일 스킨 선택
        if(IsRight){
            if(SkinMaster.TileSelect < SkinMaster.Tile.Length-1){
                SkinMaster.TileSelect++;
            }
        }else{
            if(SkinMaster.TileSelect > 0){
                SkinMaster.TileSelect--;
            }
        }

    }

    public void MusicSelect(bool IsUp){
        if(IsUp){
            if(InGameSelect < SoundMaster.Song.Length - 1){
                InGameSelect += 1;
                SoundMaster.SlowChange(SoundMaster.Song[InGameSelect].Music,SoundMaster.Song[InGameSelect].PreViewStart);
                SoundMaster.AudioSource[1].Play();
            }
        }else{
            if(InGameSelect  > 0){
                InGameSelect  -=1;
                SoundMaster.SlowChange(SoundMaster.Song[InGameSelect].Music,SoundMaster.Song[InGameSelect].PreViewStart);
                SoundMaster.AudioSource[1].Play();
            }
                

        }

    }

}
