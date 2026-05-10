using AudioManagement;
using UnityEngine;

public interface IBindable<T> where T : ScriptableObject
{
    public void Bind(T item);
}
