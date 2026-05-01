using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 개발자 편의를 위한 타입
/// </summary>
namespace Types.Utils
{
    #region ObjectWithComponent

    // 처음 컴포넌를 겟하면 그 컴포넌트를 불러 올수 있는 클래스
    public class InitableComponent<_T1> where _T1 : Component
    {
        private _T1 _component;

        private Func<_T1> getter;
        public _T1 component
        {
            get
            {
                return getter();
            }
            private set
            {
                _component = value;
            }
        }


        public InitableComponent(GameObject gameObject)
        {
            getter = () => ComponentInit(gameObject);
        }

        private _T1 ComponentInit(GameObject gameObject)
        {
            gameObject.TryGetComponent(out _component);
            getter = GetComponent;

            return _component;
        }

        private _T1 GetComponent()
        {
            return _component;
        }
    }


    // 게임 오브젝트와 함께 추가로 필요한 컴포넌트가 한번에 포함된 타입

    [Serializable]
    public class ObjectWithComponent<_T1> where _T1 : Component
    {
        [SerializeField]
        public GameObject gameObject;

        private InitableComponent<_T1> _component;
        public _T1 component
        {
            get
            {
                if (_component == null) _component = new InitableComponent<_T1>(gameObject);
                return _component.component;
            }
        }
    }
    [Serializable]
    public class ObjectWithComponent<_T1, _T2> where _T1 : Component where _T2 : Component
    {
        [SerializeField]
        public GameObject gameObject;

        private InitableComponent<_T1> firstComponent;
        private InitableComponent<_T2> secondComponent;

        public _T1 component1
        {
            get
            {
                if (firstComponent == null) firstComponent = new InitableComponent<_T1>(gameObject);
                return firstComponent.component;
            }
        }
        public _T2 component2
        {
            get
            {
                if (secondComponent == null) secondComponent = new InitableComponent<_T2>(gameObject);
                return secondComponent.component;
            }
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ObjectWithComponent<>), true)]
    public class FirstObjectWithComponentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 내부의 'gameObject' 필드를 찾습니다.
            SerializedProperty gameObjectProperty = property.FindPropertyRelative("gameObject");
            EditorGUI.PropertyField(position, gameObjectProperty, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // 한 줄 높이만 사용하도록 설정 (접힘/펼침 공간 제거)
            return EditorGUIUtility.singleLineHeight;
        }
    }

    [CustomPropertyDrawer(typeof(ObjectWithComponent<,>), true)]
    public class SecondObjectWithComponentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 내부의 'gameObject' 필드를 찾습니다.
            SerializedProperty gameObjectProperty = property.FindPropertyRelative("gameObject");
            EditorGUI.PropertyField(position, gameObjectProperty, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // 한 줄 높이만 사용하도록 설정 (접힘/펼침 공간 제거)
            return EditorGUIUtility.singleLineHeight;
        }
    }
#endif

    #endregion
    public struct Switcher<_T1> where _T1 : IEquatable<_T1>
    {
        private _T1 value;
        public _T1 Value => value;

        public bool Switch(_T1 newValue)
        {
            // 이전 값과 다르면 덮어쓰고 true반환
            if (!value.Equals(newValue))
            {
                value = newValue;
                return true;
            }

            return false;
        }

        public Switcher(_T1 value)
        {
            this.value = value;
        }

        public static implicit operator _T1(Switcher<_T1> target)
        {
            return target.value;
        }
    }

    [Serializable]
    public class FloatRange
    {
        public float start;
        public float end;

        public FloatRange(float start = 0, float end = 0)
        {
            this.start = start;
            this.end = end;
        }

    }




}