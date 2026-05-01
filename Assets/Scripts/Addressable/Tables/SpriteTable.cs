using AddressableManagement;
using SimpleActions;
using Type.Addressable;
using Type.Addressable.Table;

using UnityEngine;
using Extensions;
namespace Tables.SpriteTable
{
    public class SpriteTable : Managers<SpriteTable>
    {
        Loader<AddressableSprite, SpriteIndex> loader;
        private void Awake()
        {
            Singleton(true);
        }
        private void Start()
        {
            loader = new Loader<AddressableSprite, SpriteIndex>(MenuAssetLoadManager.Instance.addressableLoadRecoder);
        }

        public void Load(string tag)
        {
            loader.Load(this, tag);
        }

        public AddressableSprite GetSprite(SpriteIndex spriteIndex)
        {
            return loader.Get(spriteIndex);
        }

        //메인메뉴 텍스트 해제
        public void Release()
        {
            loader.Release();
        }
    }
}