using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// 사용자 정의
using Extensions;
using SimpleActions;

// Unity
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;


namespace Type.Addressable
{
    // 에셋 로딩을 통합 관리하기위한 클래스
    public class LoadingRecoder
    {
        public int index { get; private set; }
        public int leftPrograss { get; private set; }

        private List<bool> prograssList = new List<bool>();


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

            prograssList.Add(false);
            OnStartLoading.Invoke(index);
        }

        public void CompleteLoading(int loadIndex)
        {
            leftPrograss--;
            prograssList[loadIndex] = true;
            OnCompleteLoading.Invoke(loadIndex);
        }

        public void LoadingError(int loadIndex, Exception ex)
        {

            if (prograssList[loadIndex] == true)
                return; // 이미 완료된 경우 무시

            leftPrograss--;
            prograssList[loadIndex] = true;
            OnErrorLoading.Invoke(loadIndex, ex);

            Debug.LogWarning($"에러가 발생하였습니다!");
        }


        public float GetTotalPrograss()
        {
            if (prograssList.Count == 0) return 0;

            float total = 0;
            foreach (var p in prograssList)
            {
                total += p ? 1 : 0;
            }

            return total / prograssList.Count;
        }

        public float GetLoadingPrograss(int loadIndex)
        {
            return prograssList[loadIndex] ? 1 : 0;
        }

        public bool IsAllComplete()
        {
            if (GetTotalPrograss() < 1f) return false;
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

    public class LoaderContainer
    {
        protected Coroutine coroutine;
        protected LoadingRecoder loadingRecoder;
        protected Stack<int> recoderBindedIndex = new Stack<int>();
        public LoaderContainer(LoadingRecoder recoder)
        {
            loadingRecoder = recoder;
        }
    }

    /// <summary>
    /// 어드레서블 에셋을 로딩하고 저장하기 위한 클래스
    /// </summary>
    /// <typeparam name="_T1">에셋의 타입</typeparam>
    /// <typeparam name="_T2">에셋의 인덱스</typeparam>
    public class Loader<_T1, _T2> : LoaderContainer where _T1 : IndexedScriptableObject<_T2> where _T2 : System.Enum
    {
        private AsyncOperationHandle<IList<_T1>> handle;

        //값 저장을 위한 타입
        private Dictionary<_T2, _T1> table = new Dictionary<_T2, _T1>();

        public _T1 Get(_T2 index)
        {
            if (table == null)
                throw new Exception("에셋이 아직 로딩되지 않았습니다!");

            return table[index];
        }

        public Loader(LoadingRecoder recoder) : base(recoder) { }


        public void Release()
        {
            // 핸들 정리
            handle.SafeRelease();

            table = null;
        }

        public void Load(MonoBehaviour mono, string tag)
        {
            mono.SafeStartCoroutine(ref coroutine, LoadingAsset(tag));
        }


        private IEnumerator LoadingAsset(string label, Action callback = null)
        {
            if (loadingRecoder != null && loadingRecoder.IsAllComplete())
            {
                Debug.LogWarning($"이 로더는 이미 모든 에셋이 로딩된 상태에서 다시 로딩을 시도하였습니다!");
                yield break;
            }


            //에셋 갯수 확인 후 그 갯수만큼 신고
            AsyncOperationHandle<IList<IResourceLocation>> countHandle = Addressables.LoadResourceLocationsAsync(label);
            yield return countHandle;

            for (int i = 0; i < countHandle.Result.Count; i++)
            {
                // 부여된 인덱스 저장
                int index;
                if (loadingRecoder != null)
                {
                    loadingRecoder.StartLoading(out index);
                    recoderBindedIndex.Push(index);
                }
            }

            // 에셋 로딩
            handle =
                Addressables.LoadAssetsAsync<_T1>(
                    label,
                    loadedAsset =>
                    {
                        AssetBind(loadedAsset);
                    }
                );
            yield return handle;

            //실패시 로그 출력
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError(handle.OperationException);
            }
            // 카운트는 바로 해제
            countHandle.SafeRelease();

            // 콜백
            callback?.Invoke();
        }


        private void AssetBind(_T1 asset)
        {
            //레코더에게 완료알림
            if (loadingRecoder != null) loadingRecoder.CompleteLoading(recoderBindedIndex.Pop());

            //바인딩
            table[asset.index] = asset;
        }
    }

    /// <summary>
    /// 에셋들을 각 핸들로 로딩해야하는 에셋을 위한 클래스
    /// </summary>
    /// <typeparam name="_T1">에셋의 타입</typeparam>
    /// <typeparam name="_T2">에셋의 인덱스</typeparam>
    public class EachLoader<_T1, _T2> : LoaderContainer where _T1 : IndexedScriptableObject<_T2> where _T2 : System.Enum
    {
        private Dictionary<_T2, KeyValuePair<AsyncOperationHandle<_T1>, _T1>> table = new Dictionary<_T2, KeyValuePair<AsyncOperationHandle<_T1>, _T1>>();

        public EachLoader(LoadingRecoder recoder) : base(recoder) { }

        public void Release()
        {
            //키를 뽑아 일괄 삭제
            List<_T2> keys = table.Keys.ToList();

            foreach (_T2 key in keys)
            {
                if (table.ContainsKey(key))
                {
                    table[key].Key.Release();
                    table.Remove(key);
                }
            }

            table.Clear();
        }

        //릴리즈 함수
        public void Release(_T2 ignore)
        {
            //키만 뽑아서 비교하며 삭제
            List<_T2> keys = table.Keys.ToList();

            foreach (_T2 key in keys)
            {
                if (!key.Equals(ignore) && table.ContainsKey(key))
                {
                    table[key].Key.Release();
                    table.Remove(key);
                }
            }
        }

        public void Load(MonoBehaviour mono, string label)
        {
            mono.SafeStartCoroutine(ref coroutine, LoadingAsset(label));
        }

        public _T1 Get(_T2 index)
        {
            if (!table.ContainsKey(index))
                throw new KeyNotFoundException($"인덱스 {index}에 해당하는 에셋이 존재하지 않습니다!");

            return table[index].Value;
        }

        public _T1[] GetAll()
        {
            return table.Values.Select(kv => kv.Value).ToArray();
        }


        public IEnumerator LoadingAsset(string label, Action callback = null)
        {
            // 에셋 로딩 확인할 그룹화 하기 전 핸들 리스트
            List<AsyncOperationHandle> groupHandles = new List<AsyncOperationHandle>();

            // 값들을 저장하는 임시 핸들
            List<AsyncOperationHandle<_T1>> loadedHandle = new List<AsyncOperationHandle<_T1>>();

            // 에셋 갯수 확인
            AsyncOperationHandle<IList<IResourceLocation>> countHandle = Addressables.LoadResourceLocationsAsync(label, typeof(_T1));
            yield return countHandle;

            // 갯수만큼 신고
            for (int i = 0; i < countHandle.Result.Count; i++)
            {
                if (loadingRecoder != null)
                {
                    loadingRecoder.StartLoading(out int index);
                    recoderBindedIndex.Push(index);
                }
            }

            //에셋 로딩 시작
            foreach (IResourceLocation location in countHandle.Result)
            {


                // 번들이 존재하는지 확인
                try
                {
                    // 로딩 시작
                    AsyncOperationHandle<_T1> loaded = Addressables.LoadAssetAsync<_T1>(location);

                    //핸들 그룹화 하기위해 추가
                    groupHandles.Add(loaded);
                    loadedHandle.Add(loaded);
                }
                catch (Exception ex)
                {

                    // 에러 전달
                    loadingRecoder.LoadingError(recoderBindedIndex.Pop(), ex);

                    Debug.Log(ex);
                }
            }

            // 에셋이 전부 로딩됬는지 확인하기 위해 그룹화
            AsyncOperationHandle groupHandle = Addressables.ResourceManager.CreateGenericGroupOperation(groupHandles);

            yield return groupHandle;

            foreach (var eachHandle in loadedHandle)
            {
                // 성공시 값 저장
                if (eachHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    //에셋 핸들에 넣기
                    table.Add(eachHandle.Result.index, new KeyValuePair<AsyncOperationHandle<_T1>, _T1>(eachHandle, eachHandle.Result));
                    loadingRecoder.CompleteLoading(recoderBindedIndex.Pop());
                }
                else
                {
                    // 에러 전달
                    Debug.LogError(eachHandle.OperationException);
                    loadingRecoder.LoadingError(recoderBindedIndex.Pop(), eachHandle.OperationException);
                }
            }

            //정리
            countHandle.SafeRelease();

            //콜백
            callback?.Invoke();
        }
    }
}