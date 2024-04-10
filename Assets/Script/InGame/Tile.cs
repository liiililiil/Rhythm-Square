using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : InGameMaster
{
    public SkinMaster SkinMaster;
    public Vector2 ThisPos;
    private SpriteRenderer SpriteRenderer;

    void Start(){
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(ThisPos.x >= (TileSize.x-1)/2*-1 && ThisPos.x <= (TileSize.x-1)/2 &&
        ThisPos.y >= (TileSize.y-1) /2*-1 && ThisPos.y <= (TileSize.y-1)/2 &&
        IsLock)
        SpriteRenderer.color = new Color(1,1,1,1);
        else
        SpriteRenderer.color = new Color(1,1,1,0);

        SpriteRenderer.sprite = SkinMaster.Tile[SkinMaster.TileSelect];
    }
}
