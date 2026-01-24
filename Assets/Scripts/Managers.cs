using UnityEngine;

public class Managers<T> : MonoBehaviour
{
    public static T Instance { get; private set; }

    public void Singleton(bool isDontDestroyOnLoad = true)
    {
        if(Instance == null)
        {
            Instance = (T)(object)this;

            if(isDontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
