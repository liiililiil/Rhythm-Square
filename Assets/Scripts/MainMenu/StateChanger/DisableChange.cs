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
        ChangeState(stateDefault, true);

        foreach(var stateChange in stateChange)
        {
            if(stateChange.targetState == newState)
            {
                ChangeState(stateChange, false);
                return;
            }
            else
            {
                ChangeState(stateChange, true);
            }
        }

        //기본값으로 변경
        ChangeState(stateDefault, false);
    }

    // null 감지 가능한 값 변경 함수
    private void ChangeState(StateChange<MonoBehaviour> targetStateChange, bool value)
    {
        if(targetStateChange.value == null)
        {
            Debug.LogWarning($"state에 지정된 Action이 없어 무시되었습니다.",this);
        }
            else
        {
            targetStateChange.value.enabled = value;
        }
    }
}
