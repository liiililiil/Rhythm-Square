using Type.Addressable;
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
        LoadingRecoder assetRecoder = MenuAssetLoadManager.Instance.assetLoadRecoder;
        LoadingRecoder addressableRecoder = MenuAssetLoadManager.Instance.addressableLoadRecoder;

        assetRecoder.OnStartLoading.AddListener(AssetAdd);
        assetRecoder.OnCompleteLoading.AddListener(AssetComplete);
        addressableRecoder.OnStartLoading.AddListener(AddressableAdd);
        addressableRecoder.OnCompleteLoading.AddListener(AddressableComplete);

        //이벤트가 등록되기전에 에셋 등록이 시작된 경우를 방지하기 위해 초기화
        assetCount = assetRecoder.index;
        addressableCount = addressableRecoder.index;

        assetCompleteCount = assetRecoder.index - assetRecoder.leftPrograss;
        assetCompleteCount = addressableRecoder.index - addressableRecoder.leftPrograss;
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
    }

}
