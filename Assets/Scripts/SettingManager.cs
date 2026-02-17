using Types.Menu;
using SimpleActions;
using UnityEngine;
public class SettingManager : Managers<SettingManager>
{
    private Setting _setting = new Setting();
    public Setting setting {get => _setting; private set => _setting = value;}

    public SimpleEvent onChangeSetting = new SimpleEvent();

    private void Awake() {
        Singleton();
    }
    private void Start()
    {
        onChangeSetting.Invoke();
    }

    public void SetLanguage(Language language, bool isSilence = false)
    {
        setting.language = language;
        if(!isSilence) onChangeSetting.Invoke();
    }

    public void SetVolume(float value, Types.Menu.AudioType type, bool isSilence = false)
    {
        setting.volumes.SettMatchedAudio(type, value);

        if(!isSilence) onChangeSetting.Invoke();
    }

    public void SetOffset(int value, bool isSilence = false)
    {
        value = Mathf.Clamp(value,-500,500);
        setting.offset = value;

        if(!isSilence) onChangeSetting.Invoke();
    }

    

}
