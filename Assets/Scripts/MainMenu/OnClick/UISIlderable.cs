using Type;
using UnityEngine;
using UnityEngine.UI;

public abstract class UISilderable : UIInteractable
{
    protected Switcher<bool> isFocused = new Switcher<bool>(false);


    [Space(10), SerializeField]
    protected Slider slider;

    [SerializeField]
    protected SliderHandle sliderHandle;
    private void SilderHandleUpdate()
    {
        // 포커스 상태에 따라 함수 실행
        if (isFocused)
            sliderHandle.OnSubmit();

        else
            sliderHandle.OnUp();
    }


    public override void InvokeSubmit()
    {
        if (isFocused.Switch(!isFocused.Value))
        {
            SilderHandleUpdate();
        }
    }
    public override void InvokeCancel()
    {
        if (isFocused.Switch(false))
        {
            SilderHandleUpdate();
        }
    }
    public override void InvokeEnter()
    {
        sliderHandle.OnEnter();
    }
    public override void InvokeExit()
    {
        isFocused.Switch(false);

        sliderHandle.OnExit();
        OnExit();
    }

    public override void InvokeRight()
    {
        OnRight();
    }
    public override void InvokeLeft()
    {
        OnLeft();
    }

    public override bool InvokeNavigate(Vector2Byte value)
    {
        if (isFocused.Value)
        {
            if (value.x == 1) InvokeRight();
            else if (value.x == 255) InvokeLeft();
            return true;
        }
        return false;
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
