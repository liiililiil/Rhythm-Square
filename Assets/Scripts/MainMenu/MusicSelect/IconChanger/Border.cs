using AudioManagement;
using Unity.VectorGraphics;
using UnityEngine;

public class Border : MonoBehaviour, IBindable<MusicInfo>
{
    SVGImage sVGImage;

    private void Awake()
    {
        sVGImage = GetComponent<SVGImage>();

    }
    public void Bind(MusicInfo info)
    {
        sVGImage.color = info.borderColor;
    }
}
