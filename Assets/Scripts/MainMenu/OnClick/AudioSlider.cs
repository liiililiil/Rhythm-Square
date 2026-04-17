using Type.Menu;
using UnityEngine;
using UnityEngine.UI;
using Menu = Type.Menu;

public class AudioSlider : UISilderable
{
    [SerializeField]
    private ConfigType configType;
    private void OnEnable()
    {
        slider.onValueChanged.AddListener(OnSliderChange);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnSliderChange);
    }

    protected override void OnStart()
    {
        SettingManager.Instance.GetConfig<float>(configType).OnChangeConfig.AddListener(OnSettingChange);

        // 초기화
        slider.SetValueWithoutNotify(SettingManager.Instance.GetConfigValue<float>(configType));
    }

    private void OnSettingChange(float value)
    {
        slider.SetValueWithoutNotify(value);
    }

    private void OnSliderChange(float value)
    {
        SettingManager.Instance.SetValue(value, configType);
    }
    protected override void OnLeft()
    {
        slider.value = (int)slider.value - 1;
    }
    protected override void OnRight()
    {
        slider.value = (int)slider.value + 1;
    }
}
