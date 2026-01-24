using UnityEngine;

public class MenuMusicManager : Managers<MenuMusicManager>
{
    public AudioSource audioSource;

    private void Awake()
    {
        Singleton(false);

        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }


}
