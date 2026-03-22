using AddressableManagement;
using SimpleActions;
using Type.Addressable;
using Type.Addressable.Table;

using UnityEngine;
using Utils;
namespace Tables.PrefabTable{
    public class PrefabTable : Managers<PrefabTable>
    {
        Loader<AddressablePrefab, PrefabIndex> loader = new Loader<AddressablePrefab, PrefabIndex>();
        private void Awake() {
            Singleton(true);
        }

        private void Start() {
            loader.RecoderBind(MenuAssetLoadManager.Instance.addressableLoadRecoder);
        }

        public void LoadMainMenu(string tag)
        {
            this.SafeStartCoroutine(ref loader.coroutine, loader.LoadingAsset(tag));
        }
 
        public AddressablePrefab GetPrefab(PrefabIndex prefabIndex)
        {
            return loader.table[prefabIndex];
        }

        //메인메뉴 해제
        public void Release()
        {
            loader.Release();
        }
    }
}