using UnityEngine;
using Tables.SpriteTable;
using Type.Addressable.Table;
using Unity.VectorGraphics;
public class SVGSpriteLoader : MonoBehaviour
{
    [SerializeField]
    private SpriteIndex spriteIndex;

    private SVGImage sVGImage;

    private void Awake() {
        sVGImage = GetComponent<SVGImage>();
    }

    private void Start()
    {
        if (AssetLoadManager.Instance.addressableLoadRecoder.IsAllComplete())
        {
            sVGImage.sprite = SpriteTable.Instance.GetSprite(spriteIndex).sprite;
        }
        else
        {
            AssetLoadManager.Instance.LoaderBind(OnMainMenuAssetLoaded);
            
        }
    }

    private void OnMainMenuAssetLoaded()
    {
        sVGImage.sprite = SpriteTable.Instance.GetSprite(spriteIndex).sprite;
    }
}
