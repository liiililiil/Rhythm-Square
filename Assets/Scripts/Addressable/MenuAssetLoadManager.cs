using UnityEngine;
using SimpleActions;
using Type.Addressable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class MenuAssetLoadManager : Managers<MenuAssetLoadManager>
{

    public SimpleEvent OnMainMenuAssetLoaded = new SimpleEvent();

    public LoadingRecoder assetLoadRecoder = new LoadingRecoder();
    public LoadingRecoder addressableLoadRecoder = new LoadingRecoder();

    private Stack<int> loaderIndex = new Stack<int>();
    private Stack<Action> loaderAction = new Stack<Action>();

    private void Awake()
    {
        Singleton(true);
    }

    private void Start()
    {
        Load();
    }

    public void AssetLoaderBind(Action action)
    {
        int index;
        assetLoadRecoder.StartLoading(out index);

        loaderIndex.Push(index);
        loaderAction.Push(action);
    }

    // 천천히 로딩 하기
    private IEnumerator MenuProgressiveLoading()
    {
        Debug.Log($"Index : {loaderIndex.Count}");
        while(loaderIndex.Count > 0)
        {
            loaderAction.Pop().Invoke();

            assetLoadRecoder.CompleteLoading(loaderIndex.Pop());

            yield return null;
        }

        //완료하면 완료 invoke
        OnMainMenuAssetLoaded.Invoke();        
    }
    public void Load()
    {
        Tables.PrefabTable.PrefabTable.Instance.Load(Type.Addressable.Tag.Prefab.MAIN_MENU);
        Tables.SpriteTable.SpriteTable.Instance.Load(Type.Addressable.Tag.Sprite.MAIN_MENU);
        Tables.TextTable.TextTable.Instance.Load(Type.Addressable.Tag.Text.MAIN_MENU);
        Tables.MusicTable.MusicTable.Instance.Load();

        // 어드레서블 로딩이 끝나면 오브젝트의 에셋 불러오기를 시작
        StartCoroutine(addressableLoadRecoder.WaitForCompleteAllLoading(StartMenuLoad));
    }

    // 스택 트레이스를 조사하기 위해 코루틴 스타트를 개별 함수로 분리
    private void StartMenuLoad()
    {
        StartCoroutine(MenuProgressiveLoading());
    }
    
}
