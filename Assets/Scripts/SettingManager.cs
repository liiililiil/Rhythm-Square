using UnityEngine;
using Types;
using SimpleActions;
public class SettingManager : Managers<SettingManager>
{
    private Setting _setting = new Setting();
    public Setting setting {get => _setting; private set => _setting = value;}

    public SimpleEvent<Setting> onChangeSetting = new SimpleEvent<Setting>();

    private void Awake() {
        Singleton();
    }
    private void Start()
    {
        onChangeSetting.Invoke(setting);
    }
}
