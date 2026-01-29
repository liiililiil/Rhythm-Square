using MainMenu.StateChanger;
using Types;
using UnityEngine;

public class DisableChange : StateChanger
{

    [SerializeField]
    private InstantMenuStateChange<MonoBehaviour>[] stateChange;
    [SerializeField]
    private InstantMenuStateDefault<MonoBehaviour> stateDefault;
    
    protected override void OnInvoke(MenuState newState)
    {
        foreach(var stateChange in stateChange)
        {
            if(stateChange.targetState == newState)
            {
                stateChange.value.enabled = false;
                return;
            }
            else
            {
                stateChange.value.enabled = true;
            }
        }

        //기본값으로 변경
        stateDefault.value.enabled = false;
    }
}
