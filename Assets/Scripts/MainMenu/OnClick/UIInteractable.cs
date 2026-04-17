using Type.Menu;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIInteractable : MonoBehaviour
{
    [SerializeField]
    protected bool isDefault;
    protected MenuState menuState;

    protected void Start()
    {
        OnStart();
    }
    protected virtual void OnStart()
    {

    }

    [SerializeField]
    public GameObject up;

    [SerializeField]
    public GameObject down;
}
