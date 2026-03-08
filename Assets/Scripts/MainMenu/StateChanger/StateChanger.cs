using UnityEngine;

using Type.Menu;



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