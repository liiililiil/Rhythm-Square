using UnityEngine;
using SimpleActions;
using Types.Addressable;

public class AssetLoadManager : Managers<AssetLoadManager>
{
    public SimpleEvent invokeAssetLoad = new SimpleEvent(); 

    public AddressableLoadingRecoder LoadingRecoder = new AddressableLoadingRecoder();

    private void Awake()
    {
        Singleton(true);
        LoadingRecoder.OpenRecode();
    }
}
