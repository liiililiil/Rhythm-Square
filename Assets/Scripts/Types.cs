using System;
using System.Collections.Generic;
using SimpleActions;
using SimpleEasing;
namespace Types
{
    // 실수 범위
    [Serializable]
    public struct FloatRange
    {
        public float start;
        public float end;
        
    }
}
namespace Types.Menu
{
    // 메뉴 상태들
    public enum MenuState : byte
    {
        //초기 설정 로딩
        InitLoading = 0,
        MainWaitng = 1,

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
        public float musicVolume;
        public float sfxVolume;
        public Language language;

        public Setting(float _musicVolume, float _sfxVolume, Language _langeuage)
        {
            musicVolume = _musicVolume;
            sfxVolume = _sfxVolume;
            language = _langeuage;
        }

        public Setting()
        {
            musicVolume = 0.6f;
            sfxVolume = 0.6f;
            
            //일단 보편적인 언어인 영어로 설정
            language = Language.English;
        }
    }

}

namespace Types.Menu.StateChange
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
    public class AddressableLoadingRecoder
    {
        private int index;
        private int leftPrograss;

        private List<float> prograssList = new List<float>();
        private bool isRecode = false;

        private SimpleEvent<float> OnStartLoading = new SimpleEvent<float>();
        public void OpenRecode()
        {
            index = 0;
            leftPrograss = 0;
            prograssList.Clear();

            isRecode = true;
        }

        public void CloseRecode()
        {
            isRecode = false;
        }
        
        public void StartLoading(out int startIndex)
        {
            if(!isRecode)
            {
                UnityEngine.Debug.LogError("기록 중이지 않습니다!");
                startIndex = -1;
                return;
            }

            startIndex = index;
            index++;
            leftPrograss++;

            prograssList.Add(0);
        }

        public void CompleteLoading(int loadIndex)
        {
            leftPrograss--;
            prograssList[loadIndex] = 1;
        }

        public float GetTotalPrograss()
        {
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
            return leftPrograss <= 0;
        }
    }
}