using UnityEngine;
using SimpleActions;
using Type.Addressable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class AssetLoadManager : Managers<AssetLoadManager>
{

    public SimpleEvent OnMainMenuAssetLoaded = new SimpleEvent();

    public LoadingRecoder loadRecoder = new LoadingRecoder();
    public LoadingRecoder addressableLoadRecoder = new LoadingRecoder();

    private Stack<int> loaderIndex = new Stack<int>();
    private Stack<Action> loaderAction = new Stack<Action>();

    private void Awake()
    {
        Singleton(true);
    }

    private void Start()
    {
        LoadMainMenu();
    }

    public void LoaderBind(Action action)
    {
        int index;
        loadRecoder.StartLoading(out index);

        loaderIndex.Push(index);
        loaderAction.Push(action);
    }

    // 천천히 로딩 하기
    private IEnumerator ProgressiveLoading()
    {
        while(loaderIndex.Count > 0)
        {
            loaderAction.Pop().Invoke();

            loadRecoder.CompleteLoading(loaderIndex.Pop());

            yield return null;
        }

        //완료하면 완료 invoke
        OnMainMenuAssetLoaded.Invoke();
    }
    public void LoadMainMenu()
    {
        Tables.PrefabTable.PrefabTable.Instance.LoadMainMenu(Type.Addressable.Tag.Prefab.MAIN_MENU);
        Tables.SpriteTable.SpriteTable.Instance.LoadMainMenu(Type.Addressable.Tag.Sprite.MAIN_MENU);
        Tables.TextTable.TextTable.Instance.Load(Type.Addressable.Tag.Text.MAIN_MENU);
        Tables.MusicTable.MusicTable.Instance.Load();

        // 어드레서블 로딩이 끝나면 에셋 불러오기를 시작
        StartCoroutine(addressableLoadRecoder.WaitForCompleteAllLoading(() => StartCoroutine(ProgressiveLoading())));
    }
    
}
