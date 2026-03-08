using UnityEngine;
using SimpleActions;
using Type.Addressable;

public class AssetLoadManager : Managers<AssetLoadManager>
{

    public SimpleEvent OnMainMenuAssetLoaded = new SimpleEvent();

    public AddressableLoadingRecoder loadingRecoder = new AddressableLoadingRecoder();

    private void Awake()
    {
        Singleton(true);
    }

    private void Start()
    {
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        loadingRecoder.OpenRecode();
        Tables.PrefabTable.PrefabTable.Instance.LoadMainMenu(Type.Addressable.Tag.Prefab.MAIN_MENU);
        Tables.SpriteTable.SpriteTable.Instance.LoadMainMenu(Type.Addressable.Tag.Sprite.MAIN_MENU);
        Tables.TextTable.TextTable.Instance.Load(Type.Addressable.Tag.Text.MAIN_MENU);
        Tables.MusicTable.MusicTable.Instance.Load();


        
        StartCoroutine(loadingRecoder.WaitForCompleteAllLoading(OnMainMenuAssetLoaded.Invoke));
    }
}
