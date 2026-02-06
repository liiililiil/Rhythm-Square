using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleActions
{
    //한번만 작동하고 자동 해제
    public class OneTimeEvent
    {
        List<Action> actions = new List<Action>();

        public void AddListener(Action action)
        {
            actions.Add(action);
        }

        public void RemoveListener(Action action)
        {
            actions.Remove(action);
        }

        public void Invoke()
        {
            //임시 액션에 리스트를 복사하고 원본 리스트는 초기화
            List<Action> tempActions = new List<Action>(actions);
            actions.Clear();

            for(int i = tempActions.Count -1; i >=0; i--) {
                try
                {
                    tempActions[i].Invoke();
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
        }
    } 
    //경량화된 이벤트
    public class SimpleEvent
    {
        List<Action> actions = new List<Action>();

        public void AddListener(Action action)
        {
            actions.Add(action);
        }

        public void RemoveListener(Action action)
        {
            actions.Remove(action);
        }

        public void Invoke()
        {
            for(int i = actions.Count -1; i >=0; i--)
            {
                actions[i].Invoke();
            }
        }
    }

    public class SimpleEvent<T>
    {
        List<Action<T>> actions = new List<Action<T>>();

        public void AddListener(Action<T> action)
        {
            actions.Add(action);
        }

        public void RemoveListener(Action<T> action)
        {
            actions.Remove(action);
        }

        public void Invoke(T param)
        {
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                actions[i].Invoke(param);
            }
        }
    }

    public class SimpleEvent<T1, T2>
    {
        List<Action<T1, T2>> actions = new List<Action<T1, T2>>();

        public void AddListener(Action<T1, T2> action)
        {
            actions.Add(action);
        }

        public void RemoveListener(Action<T1, T2> action)
        {
            actions.Remove(action);
        }

        public void Invoke(T1 param1, T2 param2)
        {
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                actions[i].Invoke(param1, param2);
            }
        }
    }

    public class SimpleEvent<T1, T2, T3>
    {
        List<Action<T1, T2, T3>> actions = new List<Action<T1, T2, T3>>();

        public void AddListener(Action<T1, T2, T3> action)
        {
            actions.Add(action);
        }

        public void RemoveListener(Action<T1, T2, T3> action)
        {
            actions.Remove(action);
        }

        public void Invoke(T1 param1, T2 param2, T3 param3)
        {
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                actions[i].Invoke(param1, param2, param3);
            }
        }
    }
}
