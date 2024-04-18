using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Border : InGameMaster{
    private SpriteRenderer SpriteRenderer;

    void Awake() {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        transform.localScale = new Vector2(0.025f*BorderSize.x,0.025f*BorderSize.y);

        if(!IsLock){
            SpriteRenderer.color = new Color(1,1,1,1);
        }else{
            SpriteRenderer.color = new Color(1,1,1,0);
        }

    }
}
