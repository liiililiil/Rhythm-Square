using Type;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIClickable : UIInteractable, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public override void InvokeSubmit()
    {
        OnSubmit();
    }

    public override void InvokeEnter()
    {
        OnEnter();
    }
    public override void InvokeExit()
    {
        OnExit();
    }

    public override void InvokeRight()
    {
        OnSubmit();
    }

    public override void InvokeLeft()
    {
        OnSubmit();
    }

    public override void InvokeCancel()
    {
    }

    public override bool InvokeNavigate(Vector2Byte value)
    {
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InvokeEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InvokeExit();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InvokeSubmit();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        InvokeExit();
    }

    protected abstract void OnSubmit();
    protected abstract void OnEnter();
    protected abstract void OnExit();
}
