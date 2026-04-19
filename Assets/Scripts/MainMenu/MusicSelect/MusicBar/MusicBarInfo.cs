using UnityEngine;
using UnityEngine.UI;

public class MusicBarInfo : MonoBehaviour
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text artist;


    public void LoadInfo(string titleText, string artistText)
    {
        title.text = titleText;
        artist.text = artistText;
    }
}
