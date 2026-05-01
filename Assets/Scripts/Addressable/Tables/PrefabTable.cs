using AddressableManagement;
using Type.Addressable;
using Type.Enums.Addressable;
namespace Tables.PrefabTable
{
    public class PrefabTable : Managers<PrefabTable>
    {
        Loader<AddressablePrefab, PrefabIndex> loader;
        private void Awake()
        {
            Singleton(true);
        }

        private void Start()
        {
            loader = new Loader<AddressablePrefab, PrefabIndex>(MenuAssetLoadManager.Instance.addressableLoadRecoder);
        }

        public void Load(string tag)
        {
            loader.Load(this, tag);
        }

        public AddressablePrefab GetPrefab(PrefabIndex prefabIndex)
        {
            return loader.Get(prefabIndex);
        }

        //메인메뉴 해제
        public void Release()
        {
            loader.Release();
        }
    }
}