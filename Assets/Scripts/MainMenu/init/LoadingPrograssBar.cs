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
    private void BarUpdate(RectTransform target, float count, float complete)
    {
        if (count == 0 || complete == 0) return;
        target.localScale = new Vector3(complete / count, 1, 1);

        Debug.Log($"Assets => complete : {assetCompleteCount} | count : {assetCount} | result : {(float)assetCompleteCount / assetCount} \n addressable => complete : {addressableCompleteCount} | count : {addressableCount} | result : {(float)addressableCompleteCount / addressableCount}");
        
    }

}
