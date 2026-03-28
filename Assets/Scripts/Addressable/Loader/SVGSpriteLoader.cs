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
        if (MenuAssetLoadManager.Instance.assetLoadRecoder.IsAllComplete())
        {
            OnMainMenuAssetLoaded();
        }
        else
        {
            MenuAssetLoadManager.Instance.AssetLoaderBind(OnMainMenuAssetLoaded);
            
        }
    }

    private void OnMainMenuAssetLoaded()
    {
        sVGImage.sprite = SpriteTable.Instance.GetSprite(spriteIndex).sprite;
    }
}
