using ScriptManagement;
using Type.Addressable;
using Type.Enums.Addressable;

namespace Tables.TextTable
{
    public class TextTable : Managers<TextTable>
    {

        Loader<MultinationalString, TextIndex> loader;

        private void Awake()
        {
            Singleton(true);
        }

        private void Start()
        {
            loader = new Loader<MultinationalString, TextIndex>(MenuAssetLoadManager.Instance.addressableLoadRecoder);
        }

        public void Load(string tag)
        {
            loader.Load(this, tag);
        }

        public MultinationalString GetText(TextIndex textIndex)
        {
            return loader.Get(textIndex);
        }

        //메인메뉴 텍스트 해제
        public void Release()
        {
            loader.Release();
        }
    }

}