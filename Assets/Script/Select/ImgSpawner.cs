using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ImgSpawner : MonoBehaviour
{
    public GameObject PreFab;
    public SoundMaster SoundMaster;
    public GameObject Master;
    private Vector2 SpawnPos;
    void Start()
    {
        for(int i = 0; i < SoundMaster.Song.Length; i++){
            GameObject Prefab = Instantiate(PreFab,new Vector2(-10000,i*250),Quaternion.identity);
            InfoImg InfoImg = Prefab.GetComponent<InfoImg>();
            InfoImg.Master = Master;
            Prefab.transform.SetParent(transform);
            Prefab.transform.localScale = new Vector3(2,2,2);
            InfoImg.i = i;
        }
    }
}
