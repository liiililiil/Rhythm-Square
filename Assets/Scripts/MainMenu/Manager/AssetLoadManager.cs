using UnityEngine;
using SimpleActions;

public class AssetLoadManager : Managers<AssetLoadManager>
{
    public SimpleEvent invokeAssetLoad = new SimpleEvent();

    private void Awake()
    {
        Singleton(false);
    }
}
