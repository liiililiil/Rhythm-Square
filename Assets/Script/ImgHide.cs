using UnityEngine;
using UnityEngine.UI;
public class ImgHide : GetMasters
{
    private bool IsImage;
    private Image Image;
    private SpriteRenderer SpriteRenderer;
    public int NeedMode;
    void Start(){
        try{
            SpriteRenderer = GetComponent<SpriteRenderer>();
            IsImage = false;
        }catch{
            Image = GetComponent<Image>();
            IsImage = true;
            
        }
    }

    void Update()
    {
        if(NeedMode == ButtonMaster.Mode)
            if(IsImage)
                if(Image.color.a <= 1f)
                    Image.color = Image.color + new Color(1,1,1,0.1f);
            else
                if(SpriteRenderer.color.a <= 1f)
                    SpriteRenderer.color = SpriteRenderer.color + new Color(1,1,1,0.1f);

        else
            if(IsImage)
                if(Image.color.a >= 0) 
                    Image.color =  Image.color - new Color(1,1,1,0.1f);
            else
                if(SpriteRenderer.color.a >= 0) 
                    SpriteRenderer.color = Image.color - new Color(1,1,1,0.1f);
    }
}
