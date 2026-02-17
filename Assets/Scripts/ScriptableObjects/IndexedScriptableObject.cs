using UnityEngine;

/// <summary>
/// 인덱싱 가능한 스크립터블 오브젝트에서 사용하는 부모
/// </summary>
/// <typeparam name="T1">사용되는 인덱스</typeparam>
public abstract class IndexedScriptableObject<T1> : ScriptableObject
{
    [Space(10), SerializeField]
    public T1 index;
}
