using System;
using UnityEngine;
using UnityEngine.UI;

using GameManagement;
using Types; 


[RequireComponent(typeof(Text))]
[AddComponentMenu("UI/MultinationalText", 100)]
public class MultinationalTextSupport : MonoBehaviour
{
    [SerializeField]
    private MultinationalString text;
    private Text textObject;

    private void Awake()
    {
        TryGetComponent<Text>(out textObject);

        //예외 검사
        try
        {
            if(text == null) throw new Exception("텍스트가 존재하지 않습니다!");

            if(textObject.text != "") Debug.LogWarning("Text에 이미 텍스트가 존재합니다! 덮어쓰기 되었습니다.", this);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex, this);
            return;
        }
    }

    private void Start()
    {
        SettingManager.Instance.onChangeSetting.AddListener(OnChangeSetting);
    }

    private void OnChangeSetting(Setting setting)
    {
        textObject.text = text.GetString(setting.language);
    }
}
