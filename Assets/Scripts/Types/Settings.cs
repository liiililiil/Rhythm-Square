using SimpleActions;
using Type.Enums.Menu;
using Type.Enums.Settings;

namespace Types.Settings
{
    public class Config<_T1>
    {
        public SimpleEvent<_T1> OnChangeConfig { get; private set; } = new SimpleEvent<_T1>();
        public ConfigType type { get; private set; }
        public _T1 value { get; set; }

        public Config(ConfigType targetType)
        {
            type = targetType;
        }

        public _T1 Get()
        {
            return value;
        }

        public void Set(_T1 target, bool isSilence = false)
        {
            value = target;
            if (!isSilence) OnChangeConfig.Invoke(value);
        }

        public Config<T1> GetConfig<T1>()
        {
            return this as Config<T1>;
        }
    }

    public class Setting
    {

        // 오프셋
        private Config<int> offset = new Config<int>(ConfigType.Offset);

        // 볼륨
        private Config<float> music = new Config<float>(ConfigType.Music);
        private Config<float> sfx = new Config<float>(ConfigType.SFX);

        //언어
        private Config<Language> language = new Config<Language>(ConfigType.Offset);

        public Setting()
        {
            offset.value = 0;
            music.value = 0.05f;
            sfx.value = 0.05f;
            language.value = Language.English;
        }

        public Config<T1> GetConfig<T1>(ConfigType configType)
        {
            switch (configType)
            {
                case ConfigType.Offset:
                    return offset.GetConfig<T1>();
                case ConfigType.Music:
                    return music.GetConfig<T1>();
                case ConfigType.SFX:
                    return sfx.GetConfig<T1>();
                case ConfigType.Language:
                    return language.GetConfig<T1>();
                default:
                    return null;
            }
        }
    }

    public class Volume
    {
        public float value;
        public SimpleEvent<float> onValueChanged = new SimpleEvent<float>();

        public void EventInvoke()
        {
            onValueChanged.Invoke(value);
        }

        public Volume(float initValue)
        {
            value = initValue;
        }

    }
}