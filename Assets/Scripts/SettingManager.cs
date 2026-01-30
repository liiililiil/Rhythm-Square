using UnityEngine;
using Types;
public class SettingManager : Managers<SettingManager>
{
    public Settings setting {get; set;} =  new Settings(); 
    private void Awake() {
        Singleton();
    }
    private void Start() {
        setting.musicVolume = 0.6f;
    }
}
