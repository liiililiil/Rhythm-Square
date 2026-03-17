using Type.Menu;
using UnityEngine;
using UnityEngine.UI;
using Menu = Type.Menu;

public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    private Menu.AudioType audioType;

    [SerializeField]
    private Slider slider;

    private void OnEnable() {
        slider.onValueChanged.AddListener(OnSliderChange);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnSliderChange);
    }

    private void Start() {
        SettingManager.Instance.GetSetting().volumes[audioType].onValueChanged.AddListener(OnSettingChange);

        // 초기화
        slider.SetValueWithoutNotify(SettingManager.Instance.GetSetting().volumes[audioType].value);
    }

    private void OnSettingChange(float value)
    {
        slider.SetValueWithoutNotify(value);
    }

    private void OnSliderChange(float value)
    {
        SettingManager.Instance.SetVolume(value, audioType);
    }
}
