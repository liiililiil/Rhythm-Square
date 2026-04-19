using UnityEngine;
using Type;
public interface IUIInteractable
{
    public GameObject up { get; }
    public GameObject down { get; }
    public GameObject left { get; }
    public GameObject right { get; }
    public void InvokeEnter();
    public void InvokeExit();
    public void InvokeSubmit();
    public void InvokeCancel();
    public void InvokeRight();
    public void InvokeLeft();
    public bool InvokeNavigate(Vector2Byte dir);
}
