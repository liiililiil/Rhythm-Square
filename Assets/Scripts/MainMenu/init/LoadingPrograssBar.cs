using UnityEngine;
using UnityEngine.UI;

public class LoadingPrograssBar : MonoBehaviour
{
    private Text text;

    [SerializeField]
    RectTransform assetLoadingBarRect;
    [SerializeField]
    RectTransform AddressableLoadingBarRect;

    int assetCount;
    int assetCompleteCount;

    int addressableCount;
    int addressableCompleteCount;

    private void Start() {
        MenuAssetLoadManager.Instance.assetLoadRecoder.OnStartLoading.AddListener(AssetAdd);
        MenuAssetLoadManager.Instance.assetLoadRecoder.OnCompleteLoading.AddListener(AssetComplete);
        MenuAssetLoadManager.Instance.addressableLoadRecoder.OnStartLoading.AddListener(AddressableAdd);
        MenuAssetLoadManager.Instance.addressableLoadRecoder.OnCompleteLoading.AddListener(AddressableComplete);
    }

    private void AddressableAdd(int empty)
    {
        addressableCount++;
    }

    private void AddressableComplete(int empty)
    {
        addressableCompleteCount++;
        BarUpdate(AddressableLoadingBarRect, addressableCount, addressableCompleteCount);
        
    }

    private void AssetAdd(int empty)
    {
        assetCount++;
    }

    private void AssetComplete(int empty)
    {
        assetCompleteCount++;
        BarUpdate(assetLoadingBarRect, assetCount, assetCompleteCount);
    }
    private void BarUpdate(RectTransform target, int count, int complete)
    {
        if (count == 0 || complete == 0) return;
        target.localScale = new Vector3((float)complete / count, 1, 1);
        
    }

}
