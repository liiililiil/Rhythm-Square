using Type.Menu;
using SimpleActions;
using UnityEngine;
public class SettingManager : Managers<SettingManager>
{
    private Setting setting = new Setting();

    public SimpleEvent<Language> onChangeLanguage = new SimpleEvent<Language>();
    public SimpleEvent<int> onChangeOffset = new SimpleEvent<int>();

    private void Awake() {
        Singleton();
    }
    private void Start()
    {
    }

    private void InvokeEvents()
    {
        onChangeLanguage.Invoke(setting.language);
    }


    public void SetLanguage(Language language, bool isSilence = false)
    {
        setting.language = language;
        if(!isSilence) onChangeLanguage.Invoke(language);
    }

    public void SetVolume(float value, Type.Menu.AudioType type, bool isSilence = false)
    {
        setting.SetMatchedAudio(type, value);



        if (!isSilence)
        {
            setting.volumes[type].EventInvoke();
        } 
    }

    public void SetOffset(int value, bool isSilence = false)
    {
        value = Mathf.Clamp(value,-500,500);
        setting.offset = value;

        if(!isSilence) onChangeOffset.Invoke(value);
    }

    public Setting GetSetting()
    {
        return setting;
    }

    

}
