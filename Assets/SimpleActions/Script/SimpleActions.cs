using System;
using System.Collections.Generic;

namespace SimpleActions
{
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
                try
                {
                    actions[i].Invoke();
                }
                catch(NullReferenceException)
                {
                    actions.RemoveAt(i);
                }
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
                try
                {
                    actions[i].Invoke(param);
                }
                catch (NullReferenceException ex)
                {
                    UnityEngine.Debug.LogWarning($"Some Action Was delete : {ex}");
                    actions.RemoveAt(i);
                }
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
                try
                {
                    actions[i].Invoke(param1, param2);
                }
                catch (NullReferenceException)
                {
                    actions.RemoveAt(i);
                }
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
                try
                {
                    actions[i].Invoke(param1, param2, param3);
                }
                catch (NullReferenceException)
                {
                    actions.RemoveAt(i);
                }
            }
        }
    }
}
