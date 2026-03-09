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
        SettingManager.Instance.GetSetting().volumes[Menu.AudioType.Music].onValueChanged.AddListener(OnValueChange);
    }

    private void OnValueChange(float value)
    {
        SettingManager.Instance.SetVolume(value, audioType, true);
    }
}
