using AudioManagement;
using UnityEngine;
using UnityEngine.UI;

public class MusicBarInfo : MonoBehaviour, IBindable<MusicInfo>
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text artist;

    public void Bind(MusicInfo musicInfo)
    {
        title.text = musicInfo.title;
        artist.text = musicInfo.artist;
    }
}
