using System;
using SimpleEasing;
using Type.Enums.Menu;

namespace Type.StateChanges
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