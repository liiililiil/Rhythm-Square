using UnityEngine;
using UnityEngine.UI;

public abstract class UISilderable : UIInteractable, IUIInteractable
{
    [Space(10), SerializeField]
    protected Slider slider;

    [SerializeField]
    protected SliderHandle sliderHandle;

    public void InvokeDown()
    {

    }
    public virtual void InvokeEnter()
    {
        sliderHandle.OnEnter();
        OnEnter();
    }
    public virtual void InvokeExit()
    {
        sliderHandle.OnExit();
        OnExit();
    }

    public virtual void InvokeRight()
    {
        sliderHandle.OnDown();
        OnRight();
    }
    public virtual void InvokeLeft()
    {
        sliderHandle.OnDown();
        OnLeft();
    }
    protected virtual void OnEnter()
    {

    }
    protected virtual void OnExit()
    {

    }

    protected abstract void OnRight();
    protected abstract void OnLeft();


}
