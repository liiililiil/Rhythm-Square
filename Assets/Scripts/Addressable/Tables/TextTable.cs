using System.Collections.Generic;
using ScriptManagement;

using Utils;
using Types.Addressable.Table;
using Types.Addressable;

namespace Tables.TextTable
{
    public class TextTable : Managers<TextTable>
    {
        
        Loader<MultinationalString, TextIndex> loader = new Loader<MultinationalString, TextIndex>();

        private void Awake() {
            Singleton(true);
        }

        public void Load(string tag)
        {
            this.SafeStartCoroutine(ref loader.coroutine, loader.LoadingAsset(tag));
        }

        public MultinationalString GetText(TextIndex textIndex)
        {
            return loader.table[textIndex];
        }

        //메인메뉴 텍스트 해제
        public void Release()
        {
            loader.Release();
        }
    }

}