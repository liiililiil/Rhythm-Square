using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelecter : GetMasters
{
    public bool IsPlayer;
    private Image Image;


    void Awake(){
        Image = GetComponent<Image>();
    }
    void Update(){
        if(IsPlayer)
            Image.sprite = SkinMaster.Player[SkinMaster.PlayerSelect];
        else
            Image.sprite = SkinMaster.Tile[SkinMaster.TileSelect];
    }
}
