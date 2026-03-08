using Type.Menu;
using UnityEngine;
using UnityEngine.UI;

public class OffsetSlider : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private InputField inputField;

    private void Awake() {
        inputField.onSubmit.AddListener(ValueChanged);
    }

    private void OnEnable() {
        slider.onValueChanged.AddListener(OnValueChange);
        SettingManager.Instance.onChangeSetting.AddListener(OnChangeValue);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnValueChange);
        SettingManager.Instance.onChangeSetting.RemoveListener(OnChangeValue);
    }

    private void Start() {
        
    }

    private void Update() {
        //인풋 포커싱 중에는 텍스트 변경 중단
        if(inputField.isFocused) return;
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

        SettingManager.Instance.SetOffset(result, false);
    }


    private void OnValueChange(float value)
    {
        SettingManager.Instance.SetOffset((int)value, false);
    }
    
    private void OnChangeValue(Setting setting)
    {

        Debug.Log("test");
        slider.value = setting.offset;
        inputField.text = setting.offset.ToString();
    }
}
