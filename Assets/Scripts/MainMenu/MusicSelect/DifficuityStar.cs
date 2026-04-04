using Type;
using Unity.VectorGraphics;
using UnityEngine;

public class DifficuityStar : MonoBehaviour
{
    [SerializeField]
    private ObjectWithComponent<RectTransform,SVGImage> insideStar;

    [SerializeField]
    private ObjectWithComponent<RectTransform,SVGImage> outsideStar;

    [SerializeField]
    private ObjectWithComponent<RectTransform,SVGImage> wing1;
    [SerializeField]
    private ObjectWithComponent<RectTransform,SVGImage> wing2;

    private void Start() {
    }


}
