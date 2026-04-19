using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Utils;
using Type.Addressable.Table;
using Tables.TextTable;
using Type.Menu;
using SimpleEasing;


[RequireComponent(typeof(Text))]
[AddComponentMenu("UI/MultinationalText", 100)]
public class MultinationalTextSupport : MonoBehaviour
{
    [SerializeField]
    private TextIndex index;
    private Text textObject;

    private Coroutine coroutine;
    private const float DURATION = 0.5f;

    private void Awake()
    {
        textObject = GetComponent<Text>();
    }

    private void Start()
    {
        if (MenuAssetLoadManager.Instance.assetLoadRecoder.IsAllComplete())
        {
            TextBind();
        }
        else
        {
            MenuAssetLoadManager.Instance.AssetLoaderBind(TextBind);
        }

        SettingManager.Instance.GetConfig<Language>(ConfigType.Language).OnChangeConfig.AddListener(OnChangeSetting);
    }

    private void TextBind()
    {
        Language language = SettingManager.Instance.GetConfig<Language>(ConfigType.Language).value;
        textObject.text = TextTable.Instance.GetText(index).GetString(language);
    }
    private void OnChangeSetting(Language language)
    {
        string targetText = TextTable.Instance.GetText(index).GetString(language);

        // 같은 텍스트면 무시
        if (targetText == textObject.text) return;

        // text가 꺼져있으면 그냥 바로 바꾸기
        if (!textObject.enabled) textObject.text = targetText;
        else this.SafeStartCoroutine(ref coroutine, Utils.Generic.AnimationUtils.EasingChange(
            textObject.text,
            targetText,
            (string value) => textObject.text = value,
            DURATION,
            EaseType.Linear
        ));
    }

}
