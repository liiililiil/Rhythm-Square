using Type.Menu;
using UnityEngine;
using UnityEngine.UI;

public class OffsetSlider : UISilderable
{
    [SerializeField]
    private InputField inputField;

    private void Awake()
    {
        inputField.onSubmit.AddListener(ValueChanged);
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(OnValueChange);
        SettingManager.Instance.GetConfig<int>(ConfigType.Offset).OnChangeConfig.AddListener(OnChangeValue);
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnValueChange);
        SettingManager.Instance.GetConfig<int>(ConfigType.Offset).OnChangeConfig.RemoveListener(OnChangeValue);
    }

    protected override void OnStart()
    {
        //초기화
        OnChangeValue(SettingManager.Instance.GetConfigValue<int>(ConfigType.Offset));

    }

    private void Update()
    {
        //인풋 포커싱 중에는 텍스트 변경 중단
        if (inputField.isFocused) return;
    }



    private void ValueChanged(string str)
    {
        int result;
        bool isInt = int.TryParse(str, out result);

        if (!isInt)
        {
            Debug.LogError("숫자가 아닌 인풋을 입력받았습니다!");
            return;
        }

        SettingManager.Instance.SetValue(result, ConfigType.Offset, false);
    }


    private void OnValueChange(float value)
    {
        SettingManager.Instance.SetValue<int>((int)value, ConfigType.Offset, false);
    }

    private void OnChangeValue(int value)
    {
        slider.value = value;
        inputField.text = value.ToString();
    }

    protected override void OnLeft()
    {
        OnChangeValue((int)slider.value - 1);
    }
    protected override void OnRight()
    {
        OnChangeValue((int)slider.value + 1);
    }
}
