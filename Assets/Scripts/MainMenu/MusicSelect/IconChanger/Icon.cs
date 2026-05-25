using AudioManagement;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour, IBindable<MusicInfo>
{
    Image image;

    private void Awake()
    {

        image = GetComponent<Image>();

    }
    public void Bind(MusicInfo info)
    {
        image.sprite = info.sprite;
    }
}
