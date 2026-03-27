using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;



using SimpleActions;
using SimpleEasing;
using Utils;

using System.Linq;
using System.Data.SqlTypes;
using UnityEditor;

namespace Type
{
    // 실수 범위
    [Serializable]
    public class FloatRange
    {
        public float start;
        public float end;
        
    }

    // 처음 컴포넌를 겟하면 그 컴포넌트를 불러 올수 있는 클래스
    public class InitableComponent<_T1> where _T1 : Component
    {
        private _T1 _component;

        private Func<_T1> getter;
        public _T1 component
        {
            get
            {
                return getter();
            }
            private set
            {
                _component = value;
            }
        }

        public InitableComponent(GameObject gameObject) 
        {
            getter = () => ComponentInit(gameObject);
        }

        private _T1 ComponentInit(GameObject gameObject)
        {
            gameObject.TryGetComponent(out _component);
            getter = GetComponent;

            return _component;
        }

        private _T1 GetComponent()
        {
            return _component;
        }
        
    }

    // 게임 오브젝트와 함께 추가로 필요한 컴포넌트가 한번에 포함된 타입

    [Serializable]
    public class ObjectWithComponent<_T1> where _T1 : Component
    {
        [SerializeField]
        public GameObject gameObject;

        public InitableComponent<_T1> component;

        public void Bind(){
            component = new InitableComponent<_T1>(gameObject);
        }

    }

    [Serializable]
    public class ObjectWithComponent<_T1,_T2> where _T1 : Component where _T2 : Component
    {
        [SerializeField]
        public GameObject gameObject;

        public InitableComponent<_T1> firstComponent;
        public InitableComponent<_T2> secondComponent;

        public void Bind()
        {
            firstComponent = new InitableComponent<_T1>(gameObject);
            secondComponent = new InitableComponent<_T2>(gameObject);
        }
        
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ObjectWithComponent<>), true)]
    public class FirstObjectWithComponentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 내부의 'gameObject' 필드를 찾습니다.
            SerializedProperty gameObjectProperty = property.FindPropertyRelative("gameObject");
            EditorGUI.PropertyField(position, gameObjectProperty, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // 한 줄 높이만 사용하도록 설정 (접힘/펼침 공간 제거)
            return EditorGUIUtility.singleLineHeight;
        }
    }

    [CustomPropertyDrawer(typeof(ObjectWithComponent<,>), true)]
    public class SecondObjectWithComponentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 내부의 'gameObject' 필드를 찾습니다.
            SerializedProperty gameObjectProperty = property.FindPropertyRelative("gameObject");
            EditorGUI.PropertyField(position, gameObjectProperty, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // 한 줄 높이만 사용하도록 설정 (접힘/펼침 공간 제거)
            return EditorGUIUtility.singleLineHeight;
        }
    }
#endif
}

namespace Type.Audio
{
    [Serializable]
    public class MusicPart
    {
        public FloatRange startAt;
        public FloatRange loop;
        public FloatRange endAt;
    }
}
namespace Type.Menu
{
    // 메뉴 상태들
    public enum MenuState : byte
    {
        //초기 설정 로딩
        InitLoading = 0,
        InitWaitng = 1,

        //타이틀
        Main = 2,

        //노래 선택화면
        MusicSelection = 3,

        //스테이지 로딩
        MusicLoading = 4,

        //설정 화면
        Setting = 5,

        //프로그램 종료 경고
        ExitWarning = 6,

        //프로그램 종료 대기
        ExitWating = 7,

        //제작자
        Credits = 8
    }

    // 언어들
    public enum Language : byte
    {
        Empty = 0,
        Korean = 1,
        English = 2,
    }


    // 설정
    public class Setting
    {
        //볼륨 영역
        public Dictionary<AudioType,Volume> volumes{get; private set;} = new Dictionary<AudioType, Volume>();
        public float GetMatchedAudio(AudioType audioType)
        {
            return volumes[audioType].value;
        }

        public void SetMatchedAudio(AudioType audioType, float value)
        {
            volumes[audioType].value = value;
        }

        //볼륨 종료

        public Language language;

        public int offset;

        public Setting(int _offset, Language _langeuage)
        {
            //볼륨 영역
            volumes.Add(AudioType.Music, new Volume(0.6f));
            volumes.Add(AudioType.SFX, new Volume(0.6f));
            

            offset = _offset;
            language = _langeuage;
        }

        public Setting()
        {
            //볼륨 영역
            volumes.Add(AudioType.Music, new Volume(0.1f));
            volumes.Add(AudioType.SFX, new Volume(0.1f));

            offset = 0;

            //일단 보편적인 언어인 영어로 설정
            language = Language.English;
        }
    }
    
    //단일 볼륨 나열
    public class Volume
    {
        public float value;
        public SimpleEvent<float> onValueChanged = new SimpleEvent<float>();

        public void EventInvoke()
        {
            onValueChanged.Invoke(value);
        }

        public Volume(float initValue)
        {
            value = initValue;
        }

    }

    //오디오 종류 나열
    [Serializable]
    public enum AudioType : byte
    {
        Music,
        SFX,

    }

}

namespace Type.Menu.StateChange
{
    [Serializable]
    public abstract class StateChange<T>
    {
        public T value;
    }

    [Serializable]
    public abstract class MenuStateChange<T> : StateChange<T>
    {
        public MenuState targetState;
    }

    public abstract class MenuStateDefault<T> : StateChange<T>
    {
    }

    // 지연후 실행되는 목록
    [Serializable]
    public class DelayedMenuStateChange<T> : MenuStateChange<T>
    {
        public float delay;
    }

    // 기본
    [Serializable]
    public class DelayedMenuStateDefault<T> : MenuStateDefault<T>
    {
        public float delay;
    }

    // 느리게 실행되는 목록
    [Serializable]
    public class SlowMenuStateChange<T> : MenuStateChange<T>
    {
        public float duration;
        public EaseType easeType;
    }
    
    // 기본
    [Serializable]
    public class SlowMenuStateDefault<T> : MenuStateDefault<T>
    {
        public float duration;
        public EaseType easeType;
    }

}

namespace Type.Addressable
{
    // 에셋 로딩을 통합 관리하기위한 클래스
    public class LoadingRecoder
    {
        public int index {get; private set;}
        public int leftPrograss{get; private set;}

        private List<float> prograssList = new List<float>();

        
        //에셋 레코더가 시작될떄 호출되는 이벤트
        public SimpleEvent OnRecodeReset = new SimpleEvent();

        //에셋 로딩이 시작될때 마다 호출되는 이벤트
        public SimpleEvent<int> OnStartLoading = new SimpleEvent<int>();

        //에셋 로딩이 완료될때 마다 호출되는 이벤트
        public SimpleEvent<int> OnCompleteLoading = new SimpleEvent<int>();

        //에셋 로딩중 오류가 발생될떄 마다 호출되는 이벤트
        public SimpleEvent<int, Exception> OnErrorLoading = new SimpleEvent<int, Exception>();

        public void RecodeReset()
        {
            index = 0;
            leftPrograss = 0;
            prograssList.Clear();
        }

        public void CloseRecode()
        {
        }
        
        public void StartLoading(out int startIndex)
        {
            startIndex = index;
            index++;
            leftPrograss++;

            prograssList.Add(0);
            OnStartLoading.Invoke(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPrograss(int loadIndex, float value)
        {
            prograssList[loadIndex] = value;
        }

        public void CompleteLoading(int loadIndex)
        {
            if (loadIndex < 0 || loadIndex >= prograssList.Count)
            {
                Debug.LogError($"잘못된 loadIndex: {loadIndex}");
                return;
            }

            if (prograssList[loadIndex] == 1)
            {
                Debug.LogWarning($"이미 완료된 loadIndex: {loadIndex}");
            }

            
            leftPrograss--;
            prograssList[loadIndex] = 1;
            OnCompleteLoading.Invoke(loadIndex);
        }

        public void LoadingError(int loadIndex, Exception ex)
        {
            if (loadIndex < 0 || loadIndex >= prograssList.Count)
            {
                Debug.LogError($"잘못된 loadIndex: {loadIndex}");
                return;
            }

            if (prograssList[loadIndex] >= 1f)
                return; // 이미 완료된 경우 무시
            
            leftPrograss--;
            prograssList[loadIndex] = -1;
            OnErrorLoading.Invoke(loadIndex, ex);

            Debug.LogWarning($"에러가 발생하였습니다!");
        }


        public float GetTotalPrograss()
        {
            if(prograssList.Count == 0) return 0;
            
            float total = 0;
            foreach(var p in prograssList)
            {
                total += p;
            }

            return total / prograssList.Count;
        }

        public float GetLoadingPrograss(int loadIndex)
        {
            return prograssList[loadIndex];
        }

        public bool IsAllComplete()
        {
            if(GetTotalPrograss() < 1f) return false;
            return leftPrograss <= 0;
        }

        public IEnumerator WaitForCompleteAllLoading(Action callback)
        {

            while (!IsAllComplete() || GetTotalPrograss() < 1f)
            {
                yield return null;
            }
            callback.Invoke();
        }


    }
    
    //로딩될 공간이 포함된 어드레서블 에셋
    [Serializable]
    public class AssetHolder<T> where T : UnityEngine.Object
    {
        [SerializeField]
        private AssetReferenceT<T> addressableAsset;
        private AsyncOperationHandle<T> _handle;

        private LoadingRecoder loadingRecoder = null;

        private bool isloading;

        public AssetHolder(AssetReferenceT<T> asset, LoadingRecoder recoder)
        {
            addressableAsset = asset;
            loadingRecoder = recoder;
        }

        //레퍼런스를 스크립트적으로 등록할때 사용
        public void SetReference(AssetReferenceT<T> assetReference)
        {
            if(addressableAsset == assetReference) return;
            if (_handle.IsValid() && !_handle.IsDone)
            {
                Debug.LogError("에셋이 로딩되있는 상태로 레퍼런스 변경을 시도하였습니다!");
                return;
            }

            addressableAsset = assetReference;
        }

        public T GetAsset()
        {
        if (!_handle.IsValid() || !_handle.IsDone)
            throw new Exception("Not Loaded Asset");

        if (_handle.Status != AsyncOperationStatus.Succeeded)
            throw new Exception("Load Failed Asset");

            return _handle.Result;
        }

        public void Release()
        {
            if(!_handle.IsValid())
                return;

            AddressableUtils.SafeRelease(_handle);
        }

        public void Load(MonoBehaviour mono, ref Coroutine coroutine, Action callback)
        {
            mono.SafeStartCoroutine(ref coroutine, StartLoading(callback)); 
        }

        public IEnumerator StartLoading(Action InvokeLoadingComplete)
        {
            if (isloading)
            {
                Debug.LogWarning($"{addressableAsset.Asset}가 이미 로딩중입니다.");
                yield break;
            }

            isloading = true;

            //에셋 로딩 신고
            int index = -1;
            if(loadingRecoder != null) loadingRecoder.StartLoading(out index);

            //로딩
            _handle = Addressables.LoadAssetAsync<T>(addressableAsset);
            try
            {
                while (!_handle.IsDone)
                {

                    //진행도 기록
                    if(loadingRecoder != null) loadingRecoder.SetPrograss(index, _handle.PercentComplete);
                    yield return null;
                }

                if(_handle.Status == AsyncOperationStatus.Succeeded)
                {   
                    //로딩 성공시 신고 후 콜백 함수 호출 
                    if(loadingRecoder != null) loadingRecoder.CompleteLoading(index);
                    InvokeLoadingComplete.Invoke();
                }
                else
                {
                    //실패시 신고후 오류 반환
                    Debug.LogError(_handle.OperationException);
                    if(loadingRecoder != null) loadingRecoder.LoadingError(index, _handle.OperationException);
                }
            } 
            finally
            {
                isloading = false;
            }
        }

    }

    
    /// <summary>
    /// 어드레서블 에셋을 로딩하고 저장하기 위한 클래스
    /// </summary>
    /// <typeparam name="_T1">에셋의 타입</typeparam>
    /// <typeparam name="_T2">에셋의 인덱스</typeparam>
    public class Loader<_T1, _T2> where _T1 : IndexedScriptableObject<_T2> where _T2: System.Enum
    {
        public Coroutine coroutine;
        protected AsyncOperationHandle<IList<UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation>> countHandle;
        protected AsyncOperationHandle<IList<_T1>> handle;

        protected LoadingRecoder loadingRecoder;

        //값 저장을 위한 타입
        public Dictionary<_T2, _T1> table = new Dictionary<_T2, _T1>();

        public Loader(LoadingRecoder recoder){
            RecoderBind(recoder);
        }
        public Loader()
        {
            
        }

        public void RecoderBind(LoadingRecoder recoder)
        {
            loadingRecoder = recoder;
        }


        //릴리즈 함수
        public virtual void Release()
        {
            AddressableUtils.SafeRelease(countHandle);
            AddressableUtils.SafeRelease(handle);

            table.Clear();
        }

    protected Stack<int> recoderBindedIndex = new Stack<int>();

    public virtual IEnumerator LoadingAsset(string label, Action callback = null)
    {
        //에셋 갯수 확인 후 그 갯수만큼 신고
        countHandle = Addressables.LoadResourceLocationsAsync(label);

        yield return countHandle;

        for(int i = 0; i < countHandle.Result.Count; i++)
        {
            // 부여된 인덱스 저장
            int index;
            if(loadingRecoder != null){
                loadingRecoder.StartLoading(out index);
                recoderBindedIndex.Push(index);
            }
        }
        
        //에셋 로딩
        handle =
            Addressables.LoadAssetsAsync<_T1>(
                label,
                loadedAsset =>
                {
                    AssetBind(loadedAsset);  
                },
                Addressables.MergeMode.Union
            );
        yield return handle;

        //실패시 로그 출력
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError(handle.OperationException);
        }

        // 콜백
        callback?.Invoke();
    }


    protected virtual void AssetBind(_T1 asset)
    {
        //레코더에게 완료알림
        if(loadingRecoder != null) loadingRecoder.CompleteLoading(recoderBindedIndex.Pop());
        
        //바인딩
        table.Add(asset.index, asset);
    }
    }
    
    /// <summary>
    /// 에셋들을 각 핸들로 로딩해야하는 에셋을 위한 클래스
    /// </summary>
    /// <typeparam name="_T1">에셋의 타입</typeparam>
    /// <typeparam name="_T2">에셋의 인덱스</typeparam>
    public class EachLoader<_T1, _T2>: Loader<_T1,_T2> where _T1 : IndexedScriptableObject<_T2> where _T2: System.Enum
    {
        protected new Dictionary<_T2, AsyncOperationHandle<_T1>> handle = new Dictionary<_T2, AsyncOperationHandle<_T1>>();
         
        public EachLoader(LoadingRecoder recoder) : base(recoder)
        {
        }

        public EachLoader() : base()
        {
            
        }
        public new void Release()
        {
            AddressableUtils.SafeRelease(countHandle);

            //각 핸들을 삭제
            foreach(var dic in handle)
            {
                AddressableUtils.SafeRelease(dic.Value);
            }


            handle.Clear();
            table.Clear();

        }

        //릴리즈 함수
        public void Release(_T2 ignore)
        {
            //키만 뽑아서 비교하며 삭제
            List<_T2> keys = table.Keys.ToList();

            //카운트핸들은 이제 필요없으므로 바로 릴리즈
            AddressableUtils.SafeRelease(countHandle);
            
            foreach (_T2 key in keys)
            {
                if (!key.Equals(ignore) && table.ContainsKey(key))
                {
                    AddressableUtils.SafeRelease(handle[key]);

                    handle.Remove(key);
                    table.Remove(key);
                }
            }
        }
        

        public new IEnumerator LoadingAsset(string label, Action callback = null)
        {
            // 이미 로딩된 경우 예외 처리
            if(countHandle.IsValid()) throw new Exception("이미 로딩된 에셋입니다!");
            
            // 에셋 로딩 확인할 그룹화 하기 전 핸들 리스트
            List<AsyncOperationHandle> groupHandles = new List<AsyncOperationHandle>();

            // 값들을 저장하는 임시 핸들
            List<AsyncOperationHandle<_T1>> loadedHandle = new List<AsyncOperationHandle<_T1>>();

            // 에셋 갯수 확인
            countHandle = Addressables.LoadResourceLocationsAsync(label, typeof(_T1));
            yield return countHandle;

            // 갯수만큼 신고
            for(int i = 0; i < countHandle.Result.Count; i++)
            {

                // 부여된 인덱스 저장
                int index;

                if(loadingRecoder != null){
                    loadingRecoder.StartLoading(out index);
                    recoderBindedIndex.Push(index);
                }
            }
            
            //에셋 로딩 시작
            foreach (var location in countHandle.Result)
            {
                        
                var loaded= Addressables.LoadAssetAsync<_T1>(location);

                //핸들 그룹화 하기위해 추가
                groupHandles.Add(loaded);
                loadedHandle.Add(loaded);
            }

            // 에셋이 전부 로딩됬는지 확인하기 위해 그룹화
            AsyncOperationHandle groupHandle = Addressables.ResourceManager.CreateGenericGroupOperation(groupHandles);

            yield return groupHandle;
            foreach(var eachHandle in loadedHandle)
            {
                // 성공시 값 저장
                if (eachHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    //에셋 핸들에 넣기
                    handle.Add(eachHandle.Result.index, eachHandle);
                    AssetBind(eachHandle.Result);
                }
                else
                {
                    // 에러 전달
                    Debug.LogError(eachHandle.OperationException);
                    if(loadingRecoder != null) loadingRecoder.LoadingError(recoderBindedIndex.Pop(), eachHandle.OperationException);
                }
            }

            // Debug.Log("로딩 완료");

            //정리
            groupHandles.Clear();
            loadedHandle.Clear();

            //콜백
            callback?.Invoke();
        }
    }
}

namespace Type.Addressable.Table
{
    // 스프라이트 목록
    public enum SpriteIndex
    {
        // 메인메뉴용
        Diamond = 101,
        EmptyDiamond = 102,
        Square = 103,
        ModernArrows = 104,
        PlayerIcon = 105,
        SingleModernArrow = 106,
        
    }
    // 텍스트 목록
    public enum TextIndex
    {
        //버튼 텍스트
        ToMenu = 1101,
        ToSelect = 1102,
        ToSetting = 1103,
        ToExit = 1104,
        SFX = 1105,
        Music = 1106,
        Offset = 1107,

        //MainMenu 텍스트
        ExitWarning = 1201,
        Language = 1202,

        //오디오 정보
    }

    // 노래 목록
    public enum MusicIndex
    {
        // 배경용
        iluvslapbass = 101,
        
        // 플레이용
        MachRoger = 201,
        ZidandaStep = 202,
    }


    // 프리팹 목록
    public enum PrefabIndex
    {
        //메인메뉴용
        Title = 11001,
        Logo = 11002,
        SettingButton = 11003,
        SelectButton = 11004,
        ExitButton = 11005,

        //메뉴 > 설정
        LanguageSwitch = 12001,
        SettingToMenuButton = 12002,
        MusicVolumeSlider = 12003,
        SFXVolumeSlider = 12004,
        OffsetSlider = 12005,

        //메뉴 > 종료 경고
        ExitWarningText = 13001,
        ProgramExitButton = 13002,
        ExitToMenuButton = 13003,
        
    }

}
namespace Type.Addressable.Tag
{
    //어드레서블 태그 관리용 상수
    public class Text
    {
        public const string MAIN_MENU = "MainMenuText";
    }

    public class Audio
    {
        public const string MUSIC = "Music";
        public const string MUSICINFO = "MusicInfo";
        public const string PLAYERABLE = "Playerable";
        public const string BACKGROUNDINFO = "BackGroundInfo";
    }

    public class Sprite
    {
        public const string MAIN_MENU = "MainMenuSprite";
    }

    public class Prefab
    {
        public const string MAIN_MENU = "MainMenuPrefab";
    }

}


