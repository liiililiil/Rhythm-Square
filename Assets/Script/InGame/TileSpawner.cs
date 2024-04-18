using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : GetMasters
{

    
    public GameObject PreFab;
    void Start()
    {
        for(int i = -8; i <=8; i++){
            for(int j = -4; j<=4; j++){
                GameObject Prefab = Instantiate(PreFab,new Vector3(i, j, 1f),Quaternion.identity);
                Tile Tile = Prefab.GetComponent<Tile>();
                Prefab.transform.SetParent(transform);
                Tile.ThisPos = new Vector2(i,j);
                
            }
        }
    }
}

