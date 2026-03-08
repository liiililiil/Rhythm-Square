using UnityEngine;
using UnityEngine.AddressableAssets;

using AudioManagement;
using Type.Addressable.Table;
namespace AddressableManagement
{
    /// <summary>
    /// 스프라이트을 나타내는 스크립터블 오브젝트입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "EmptySpriteAsset", menuName = "SpriteAssets/New AddressableSprite")]
    public class AddressableSprite : IndexedScriptableObject<SpriteIndex>
    {
        public Sprite sprite;
    }
}