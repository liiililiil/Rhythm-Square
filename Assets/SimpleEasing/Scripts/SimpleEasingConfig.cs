using UnityEngine;

namespace SimpleEasingConfig
{
    [CreateAssetMenu(fileName = "Simple-Easing-Graph-Config", menuName = "Simple Easing/new Simple Easing Graph Config")]
    public class GraphConfig : ScriptableObject 
    {
        // Enable/Disable graph
        [Space(10)]
        [Tooltip("Enable / Disable Graph \n Default : true")]
        public bool enableGraph = true;

        // Number of samples on the graph
        [Space(10)]
        [Tooltip("Number of samples on the graph \n Default : 100"), Range(10,1000)]
        public int sample = 100;
        
        [Space(10)]
        [Tooltip("Minimum Y of the graph \n Default : -0.5"), Range(-1f, 0f)]
        public float yMin = -0.5f;

        [Tooltip("Maximum Y of the graph \n Default : 1.5"), Range(1f, 2f)]
        public float yMax = 1.5f;
    }
}
