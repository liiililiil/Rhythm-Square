using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMaster : GetMasters
{
    public static Vector2 TileSize;
    public static Vector2 BorderSize;
    public static bool IsLockUi;
    public static bool IsLock;
    private void Awake(){
        Application.targetFrameRate = 60;
        TileSize = new Vector2(5,5);
        BorderSize = new Vector2(5,5);
        IsLock = true;
    }
    public void LockUi(){
        if(!IsLockUi){
            IsLockUi = true;
        } else {
            IsLockUi = false;
        }
    }

    public void Lock(){
        if(!IsLock){
            IsLock = true;
        }else{
            IsLock = false;
        }
    }
}
