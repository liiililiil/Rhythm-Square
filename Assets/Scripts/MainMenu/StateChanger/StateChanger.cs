using Types;
using UnityEngine;
using SimpleEasing;
using System;


namespace MainMenu.StateChanger
{
    [System.Serializable]
    public abstract class StateChange<T>
    {
        public T value;
    }

    [System.Serializable]
    public abstract class MenuStateChange<T> : StateChange<T>
    {
        public MenuState targetState;

    }

    public abstract class MenuStateDefault<T> : StateChange<T>
    {
    }

    // 지연후 실행되는 목록
    [System.Serializable]
    public class DelayedMenuStateChange<T> : MenuStateChange<T>
    {
        public float delay;
    }

    // 기본
    [System.Serializable]
    public class DelayedMenuStateDefault<T> : MenuStateDefault<T>
    {
        public float delay;
    }


    // 느리게 실행되는 목록
    [System.Serializable]
    public class SlowMenuStateChange<T> : MenuStateChange<T>
    {
        public float duration;
        public EaseType easeType;
    }

    // 기본
    [System.Serializable]
    public class SlowMenuStateDefault<T> : MenuStateDefault<T>
    {
        public float duration;
        public EaseType easeType;
    }



    public abstract class StateChanger : MonoBehaviour
    {    
        
        protected void Start() 
        {
            MenuStateManager.Instance.onMenuStateChanged.AddListener(OnInvoke);
            OnStart();
        }

        protected virtual void OnStart()
        {
            return;
        }

        protected virtual void OnInvoke(MenuState newState)
        {
            Debug.LogWarning("StateChanger: OnInvoke가 재정의되지 않았습니다.");   
        }
    }


}