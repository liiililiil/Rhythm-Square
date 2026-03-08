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
    private void OnChangeValue(Setting setting)
    {
        slider.value = setting.volumes.GetMatchedAudio(audioType);
    }
}
