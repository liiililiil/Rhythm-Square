using AddressableManagement;
using SimpleActions;
using Type.Addressable;
using Type.Addressable.Table;

using UnityEngine;
using Utils;
namespace Tables.SpriteTable{
    public class SpriteTable : Managers<SpriteTable>
    {
        Loader<AddressableSprite, SpriteIndex> loader = new Loader<AddressableSprite, SpriteIndex>();
        private void Awake() {
            Singleton(true);
        }
        void Start()
        {
            loader.RecoderBind(AssetLoadManager.Instance.addressableLoadRecoder);
        }

        public void LoadMainMenu(string tag)
        {
            this.SafeStartCoroutine(ref loader.coroutine, loader.LoadingAsset(tag));
        }
 
        public AddressableSprite GetSprite(SpriteIndex spriteIndex)
        {
            return loader.table[spriteIndex];
        }

        //메인메뉴 텍스트 해제
        public void Release()
        {
            loader.Release();
        }
    }
}