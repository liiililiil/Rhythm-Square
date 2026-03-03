using UnityEngine;
using Tables.PrefabTable;
using Types.Addressable.Table;
public class PrefabLoader : MonoBehaviour
{
    [SerializeField]
    private PrefabIndex[] prefabIndex;
    private void Start() {
        if(AssetLoadManager.Instance.loadingRecoder.IsAllComplete())
        {
            OnLoadMainMenuAsset();
        }
        else
        {
            AssetLoadManager.Instance.OnMainMenuAssetLoaded.AddListener(OnLoadMainMenuAsset);
        }
    }

    private void OnLoadMainMenuAsset()
    {
        foreach (var index in prefabIndex)
        {
            Instantiate(PrefabTable.Instance.GetPrefab(index).prefab).GetComponent<RectTransform>().SetParent(transform, false);
        }
    }
}
