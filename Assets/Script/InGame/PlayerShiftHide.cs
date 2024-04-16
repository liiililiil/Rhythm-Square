using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShiftHide : MonoBehaviour
{
    public PlayerMoveMent PlayerMoveMent;
    public bool IsFlip;
    private SpriteRenderer SpriteRenderer;
    

    void Start(){
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(IsFlip){
            if(PlayerMoveMent.Filp == true){
                if(SpriteRenderer.color.a < 255)
                    SpriteRenderer.color = new Color(0,0,0,255);
            }else{
                if(SpriteRenderer.color.a > 0)
                    SpriteRenderer.color = new Color(0,0,0,0);
            }
        }else{
            if(PlayerMoveMent.BeShift == true){
                SpriteRenderer.color = new Color(0,0,0,255);
            }else{
                SpriteRenderer.color = new Color(0,0,0,0);
            }
        }

    }
}
