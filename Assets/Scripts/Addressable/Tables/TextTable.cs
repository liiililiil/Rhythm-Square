using System.Collections.Generic;
using ScriptManagement;

using Utils;
using Types.Addressable.Table;
using Types.Addressable;

namespace Tables.TextTable
{
    public class TextTable : Managers<TextTable>
    {
        
        Loader<MultinationalString, TextIndex> mainMenu = new Loader<MultinationalString, TextIndex>();

        private void Awake() {
            LoadMainMenu();
            Singleton(true);
        }

        public void LoadMainMenu()
        {
            this.SafeStartCoroutine(ref mainMenu.coroutine, mainMenu.LoadingAsset(Type.Addressable.Tag.Text.MAIN_MENU));
        }

        public MultinationalString GetMainMenuText(TextIndex textIndex)
        {
            return mainMenu.table[textIndex];
        }

        //메인메뉴 텍스트 해제
        public void ReleaseMainMenu()
        {
            mainMenu.Release();
        }
    }

}