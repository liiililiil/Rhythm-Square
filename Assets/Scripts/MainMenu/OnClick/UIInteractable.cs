using Type;
using Type.Menu;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIInteractable : MonoBehaviour, IUIInteractable
{
    [SerializeField]
    protected bool isDefault;

    [SerializeField]
    protected MenuState menuState;

    protected void Start()
    {
        if (isDefault)
            UINavigatorManager.Instance.DefaultBind(menuState, this);
        OnStart();
    }
    protected virtual void OnStart()
    {

    }

    [SerializeField]
    protected GameObject _up = null;
    [SerializeField]
    protected GameObject _down = null;
    [SerializeField]
    protected GameObject _left = null;
    [SerializeField]
    protected GameObject _right = null;
    public GameObject up { get => _up; set => _up = value; }

    public GameObject down { get => _down; set => _down = value; }
    public GameObject left { get => _left; set => _left = value; }

    public GameObject right { get => _right; set => _right = value; }


    public abstract void InvokeSubmit();
    public abstract void InvokeCancel();
    public abstract void InvokeEnter();
    public abstract void InvokeExit();
    public abstract void InvokeRight();
    public abstract void InvokeLeft();
    public abstract bool InvokeNavigate(Vector2Byte value);
}
