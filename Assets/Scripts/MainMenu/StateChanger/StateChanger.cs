using Types;
using UnityEngine;
using Easeing;


namespace MainMenu.StateChanger
{
    // 메뉴 상태에 따른 변화
    [System.Serializable]
    public struct MenuStateChange<T>
    {
        public MenuState TargetState;
        public T Value;
        public float Duration;
        public EaseType EaseType;
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