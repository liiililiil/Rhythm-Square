using UnityEngine;
using UnityEditor;
using System.Runtime.CompilerServices;

using SimpleEasing;
using SimpleEasingConfig;

namespace SimpleEasingDisplay
{
    [CustomPropertyDrawer(typeof(EaseType))]
    public class EasingTypeDrawer : PropertyDrawer
    {
        [SerializeField]
        private GraphConfig config;


        private Color gray = new Color(1, 1, 1, 0.25f);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float GetGraphHeight()
        {
            return (EditorGUIUtility.currentViewWidth - 40f) * 9f / 16f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + GetGraphHeight() + 6f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            //Property
            Rect fieldRect = new Rect(
                position.x,
                position.y,
                position.width,
                EditorGUIUtility.singleLineHeight
            );

            EditorGUI.PropertyField(fieldRect, property, label);

            //Config Null Check
            if (config == null)
            {
                Debug.LogError("SimpleEasing: Config is Null, Connection required.");
                return;
            }


            //SettingApply
            if (!config.enableGraph)
            {
                return;
            }


            //Graph
            Rect graphRect = new Rect(
                position.x,
                fieldRect.yMax + 4,
                position.width,
                GetGraphHeight()
            );

            DrawGraph(graphRect, (EaseType)property.enumValueIndex);
        }

        void DrawGraph(Rect rect, EaseType ease)
        {
            //BackGround
            EditorGUI.DrawRect(rect, new Color(0.15f, 0.15f, 0.15f));

            //Add Border 
            DrawLine(rect, 0f, gray);
            DrawDottedLine(rect, 0.5f, gray);
            DrawLine(rect, 1f, gray);

            //Draw Graph
            Vector3 prev = Vector3.zero;
            Handles.color = Color.white;
            for (int i = 0; i <= config.sample; i++)
            {
                float t = i / (float)config.sample;
                float v = Ease.Easing(t, ease);

                float x = Mathf.Lerp(rect.x, rect.xMax, t);
                float y = Mathf.Lerp(
                    rect.yMax,
                    rect.yMin,
                    Mathf.InverseLerp(config.yMin, config.yMax, v)
                );

                Vector3 p = new Vector3(x, y);
                if (i > 0)
                    Handles.DrawLine(prev, p);

                prev = p;
            }
        }

        //Draw Width Line
        void DrawLine(Rect rect, float value, Color color)
        {
            float y = Mathf.Lerp(
                rect.yMax,
                rect.yMin,
                Mathf.InverseLerp(config.yMin, config.yMax, value)
            );

            Handles.color = color;
            Handles.DrawLine(
                new Vector3(rect.x, y),
                new Vector3(rect.xMax, y)
            );
        }

        //Draw Width DottedLine
        void DrawDottedLine(Rect rect, float value, Color color)
        {
            float y = Mathf.Lerp(
                rect.yMax,
                rect.yMin,
                Mathf.InverseLerp(config.yMin, config.yMax, value)
            );
            
            Handles.color = color;
            Handles.DrawDottedLine(
                new Vector3(rect.x, y),
                new Vector3(rect.xMax, y),
                10
            );
        }
    }
}
