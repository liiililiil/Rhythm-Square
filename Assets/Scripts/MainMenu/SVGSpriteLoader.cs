using UnityEngine;
using Tables.SpriteTable;
using Types.Addressable.Table;
using Unity.VectorGraphics;
public class SVGSpriteLoader : MonoBehaviour
{
    [SerializeField]
    private SpriteIndex spriteIndex;

    private SVGImage sVGImage;

    private void Awake() {
        sVGImage = GetComponent<SVGImage>();
    }

    private void Start() {
        Invoke("Tester", 2);
    }

    private void Tester()
    {
        sVGImage.sprite = SpriteTable.Instance.GetSprite(spriteIndex).sprite;
    }
}
