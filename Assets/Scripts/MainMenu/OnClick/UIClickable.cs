using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIClickable : UIInteractable, IUIInteractable, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public void InvokeDown()
    {
        OnDown();
    }

    public void InvokeEnter()
    {
        OnEnter();
    }
    public void InvokeExit()
    {
        OnExit();
    }

    public void InvokeRight()
    {
        OnDown();
    }

    public void InvokeLeft()
    {
        OnDown();
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
        InvokeDown();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        InvokeExit();
    }

    protected abstract void OnDown();
    protected abstract void OnEnter();
    protected abstract void OnExit();
}
