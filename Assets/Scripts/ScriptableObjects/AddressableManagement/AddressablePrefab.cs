using UnityEngine;
using UnityEngine.AddressableAssets;

using AudioManagement;
using Type.Addressable.Table;
namespace AddressableManagement
{
    /// <summary>
    /// 프리펩을 나타내는 스크립터블 오브젝트입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "EmptyPrefabAsset", menuName = "PrefabAssets/New AddressablePrefab")]
    public class AddressablePrefab : IndexedScriptableObject<PrefabIndex>
    {
        public GameObject prefab;
    }
}