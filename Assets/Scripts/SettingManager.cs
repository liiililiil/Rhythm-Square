using Type.Menu;
using SimpleActions;
using UnityEngine;
public class SettingManager : Managers<SettingManager>
{
    private Setting setting = new Setting();

    public SimpleEvent<Setting> onChangeSetting = new SimpleEvent<Setting>();

    private void Awake() {
        Singleton();
    }
    private void Start()
    {
        onChangeSetting.Invoke(setting);
    }

    public void SetLanguage(Language language, bool isSilence = false)
    {
        setting.language = language;
        if(!isSilence) onChangeSetting.Invoke(setting);
    }

    public void SetVolume(float value, Type.Menu.AudioType type, bool isSilence = false)
    {
        setting.volumes.SettMatchedAudio(type, value);

        if(!isSilence) onChangeSetting.Invoke(setting);
    }

    public void SetOffset(int value, bool isSilence = false)
    {
        value = Mathf.Clamp(value,-500,500);
        setting.offset = value;

        if(!isSilence) onChangeSetting.Invoke(setting);
    }

    public Setting GetSetting()
    {
        return setting;
    }

    

}
