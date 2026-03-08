using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type.Menu.StateChange;
using Type.Menu;
using Utils;

public class DisableChange : StateChanger
{

    [SerializeField]
    private DelayedMenuStateChange<MonoBehaviour[]>[] stateChange;
    [SerializeField]
    private DelayedMenuStateDefault<MonoBehaviour[]> stateDefault;

    private List<Coroutine> coroutines = new List<Coroutine>();
    
    protected override void OnInvoke(MenuState newState)
    {
        ClearCoroutine();
        
        foreach(var stateChange in stateChange)
        {
            if(stateChange.targetState == newState)
            {
                ChangeState(stateChange.value, false, stateChange.delay);
                ChangeState(stateDefault.value, true);
                return;
            }
            else
            {
                ChangeState(stateChange.value, true);
            }
        }

        //기본값으로 변경
        ChangeState(stateDefault.value, false, stateDefault.delay);
    }

    // 코루틴 정리용
    private void ClearCoroutine()
    {
        for(int i = coroutines.Count-1; i >= 0; i--)
        {
            Coroutine coroutine = coroutines[i];
            this.SafeStopCoroutine(ref coroutine);
        }

        coroutines.Clear();
    }

    // null 감지 가능한 값 변경 함수
    private void ChangeState(MonoBehaviour[] targets, bool value, float delay = 0)
    {
        coroutines.Add(StartCoroutine(DelayedDiasble(targets, value, delay)));
    }

    private IEnumerator DelayedDiasble(MonoBehaviour[] targets, bool value, float delay)
    {
        if (delay > 0) yield return new WaitForSeconds(delay);
        foreach(MonoBehaviour target in targets)
            target.enabled = value;
    }
}
