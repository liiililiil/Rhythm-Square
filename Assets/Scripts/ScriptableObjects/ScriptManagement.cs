using Types;
using UnityEngine;

/// <summary>
/// 코드 내부에서 사용되는 내부 스크립터블 오브젝트를 정의합니다.
/// </summary>

namespace GameManagement
{
    /// <summary>
    /// 언어 기능을 위한 다국적 스트링 오브젝트입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "EmptyString", menuName = "StringAssets/New MultinationalString")]
    public class MultinationalString : ScriptableObject
    {
        [SerializeField]
        private string korean;

        [SerializeField]
        private string english;

        public string GetString(Language language)
        {
            switch (language)
            {
                case Language.Korean:
                    return korean;
                case Language.English:
                    return english;
                default:
                    Debug.LogError("지정되지 않은 언어입니다!");
                    return null;
            }
        }
    }

}

