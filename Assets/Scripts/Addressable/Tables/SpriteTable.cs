using AddressableManagement;
using SimpleActions;
using Types.Addressable;
using Types.Addressable.Table;

using UnityEngine;
using Utils;
namespace Tables.SpriteTable{
    public class SpriteTable : Managers<SpriteTable>
    {
        Loader<AddressableSprite, SpriteIndex> loader = new Loader<AddressableSprite, SpriteIndex>();
        private void Awake() {
            LoadMainMenu(Type.Addressable.Tag.Sprite.MAIN_MENU);
            Singleton(true);
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