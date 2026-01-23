using UnityEngine;
using AudioManagement;
using UnityEngine.AddressableAssets;

/// <summary>
/// 자산 관리를 위한 스크립터블 오브젝트들을 정의합니다.
/// </summary>
namespace AddressableManagement
{
    /// <summary>
    /// 노래에 맞는 오브젝트을 로드하기 위한 스크립터블 오브젝트입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "Playerable", menuName = "AddressableManagement/New Playerable Music Asset")]
    public class PlayerableMusicAsset : ScriptableObject
    {
        public AssetReferenceT<PlayableMusic> playableMusic;
        public AssetReferenceGameObject addressableGameObject;
    }
}