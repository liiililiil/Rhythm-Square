using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


public class Lever{
    bool IsActive = false;
    public bool Check(bool IsOn){
        if(IsOn && IsActive){
            IsActive = false;
            return true;
        }else if(!IsOn && !IsActive){
            IsActive = true;
            return true;
        }else{
            return false;
        }
    }
}
public class GetMasters : MonoBehaviour
{
    static public GameObject Master;
    static public SoundMaster SoundMaster;
    static public SkinMaster SkinMaster;
    static public ButtonMaster ButtonMaster;
    static public EaseMaster EaseMaster;
    static public InGameMaster InGameMaster;

    private void Awake(){
        Master = GameObject.Find("Master");
        SoundMaster = Master.GetComponent<SoundMaster>();
        SkinMaster = Master.GetComponent<SkinMaster>();
        ButtonMaster = Master.GetComponent<ButtonMaster>();
        EaseMaster = Master.GetComponent<EaseMaster>();
        InGameMaster = Master.GetComponent<InGameMaster>();
    }
}
