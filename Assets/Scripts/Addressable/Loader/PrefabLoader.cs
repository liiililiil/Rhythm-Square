using UnityEngine;
using Tables.PrefabTable;
using Type.Enums.Addressable;

public class PrefabLoader : MonoBehaviour
{
    [SerializeField]
    private PrefabIndex[] prefabIndex;
    private void Start()
    {
        if (MenuAssetLoadManager.Instance.assetLoadRecoder.IsAllComplete())
        {
            OnLoadAsset();
        }
        else
        {
            MenuAssetLoadManager.Instance.AssetLoaderBind(OnLoadAsset);
        }
    }

    private void OnLoadAsset()
    {
        foreach (var index in prefabIndex)
        {
            Instantiate(PrefabTable.Instance.GetPrefab(index).prefab).GetComponent<RectTransform>().SetParent(transform, false);
        }
    }
}
