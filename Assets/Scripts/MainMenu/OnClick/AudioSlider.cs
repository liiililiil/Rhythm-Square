using UnityEngine;
using UnityEngine.UI;
using Menu = Types.Menu;

public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    private Menu.AudioType audioType;

    [SerializeField]
    private Slider slider;

    private void OnEnable() {
        slider.onValueChanged.AddListener(OnValueChange);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnValueChange);
    }

    private void Start() {
        SettingManager.Instance.onChangeSetting.AddListener(OnChangeValue);
    }

    private void OnValueChange(float value)
    {
        SettingManager.Instance.SetVolume(value, audioType, true);
    }
    private void OnChangeValue()
    {
        slider.value = SettingManager.Instance.setting.volumes.GetMatchedAudio(audioType);
    }
}
