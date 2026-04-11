using Type.Menu;
using SimpleActions;
using UnityEngine;
public class SettingManager : Managers<SettingManager>
{
    private Setting setting = new Setting();

    private void Awake()
    {
        Singleton();
    }



    public T1 GetConfigValue<T1>(ConfigType configType)
    {
        return GetConfig<T1>(configType).Get<T1>();
    }

    public Config<T1> GetConfig<T1>(ConfigType configType)
    {
        return setting.GetConfig<T1>(configType);
    }

    public void SetValue<T1>(T1 value, ConfigType configType, bool isSilence = false)
    {
        setting.GetConfig<T1>(configType).Set(value, isSilence);
    }
}
