using UnityEngine;

public class TextHide : GetMasters
{   
    public Vector2 Max;
    public int Speed;
    public int NeedMode;
    private RectTransform RectTransform;
    
    void Start(){
        RectTransform = GetComponent<RectTransform>();

    }
    void Update(){
        if(NeedMode == ButtonMaster.Mode)
            if(RectTransform.sizeDelta.x < Max.x)
                RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x+Speed,Max.y);
            else
                RectTransform.sizeDelta = Max;

        else
            if(RectTransform.sizeDelta.x > 10)
                RectTransform.sizeDelta -= new Vector2(Speed,0);
            else
                RectTransform.sizeDelta = new Vector2(1,10);


    }

    
}
