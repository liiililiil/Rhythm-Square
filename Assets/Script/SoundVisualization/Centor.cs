using UnityEngine;

public class Centor : MonoBehaviour
{
    public EaseMaster EaseMaster;
    public Vector2[] Pos;
    public int[] NeedMode;
    public GameObject PreFab;
    private Vector2 NowPos;
    private Vector2 TargetPos;
    private Vector2 SpawnPos;
    private Vector2 Result;
    private float TargetScale;
    private float Time;
    private int ModeRecord;
    
    private SoundMaster SoundMaster;
    private ButtonMaster ButtonMaster;

    void Awake()
    {
        ModeRecord = -1;
        GameObject Master = GameObject.Find("Master");
        SoundMaster = Master.GetComponent<SoundMaster>();
        ButtonMaster = Master.GetComponent<ButtonMaster>();

        for(int i = 0; i < 365; i++){
            float Angle = i * 1f * Mathf.Deg2Rad;
            SpawnPos.x = 3 * Mathf.Cos(Angle);
            SpawnPos.y = 3 * Mathf.Sin(Angle);

            GameObject Prefab = Instantiate(PreFab,SpawnPos+(UnityEngine.Vector2)transform.position,Quaternion.identity);
            Spawned Spawned = Prefab.GetComponent<Spawned>();
            Spawned.SoundMaster = Master.GetComponent<SoundMaster>();
            Prefab.transform.SetParent(transform);
            Spawned.Select = i;

            Spawned.transform.rotation = Quaternion.Euler(0f,0f,1f*(float)i+90);

        }


    }
    void Update()
    {   
        for(int i = 0; i<Pos.Length; i++)
        if(NeedMode[i] == ButtonMaster.Mode && ModeRecord != NeedMode[i]){
            ModeRecord = NeedMode[i];
            NowPos = transform.position;
            TargetPos = Pos[i];
            Time =0;
        }


        Time += 0.01f;
        Result = Vector2.Lerp(NowPos,TargetPos,EaseMaster.OutQuint(Time));


        TargetScale = 1;
        for(int i = 0; i <SoundMaster.SpectrumData.Length; i++)
            if(SoundMaster.SpectrumData[i] > 0.01) TargetScale += 0.001f;
        if(TargetScale < transform.localScale.x)
            TargetScale = transform.localScale.x * 0.99f;
        
        if(TargetScale <= 1)
            TargetScale = 1;

        
        transform.position = Result;
        transform.localScale = new Vector2(TargetScale,TargetScale);

        transform.rotation = transform.rotation * Quaternion.Euler(0f,0f, (TargetScale-1)*10);
    }
}
