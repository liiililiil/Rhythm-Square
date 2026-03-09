using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Utils;
using Type.Addressable.Table;
using Tables.TextTable;
using Type.Menu;


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
        if(AssetLoadManager.Instance.loadingRecoder.IsAllComplete())
        {
            OnChangeSetting();
        }
        else
        {
            AssetLoadManager.Instance.OnMainMenuAssetLoaded.AddListener(OnChangeSetting);
        }

        SettingManager.Instance.onChangeLanguage.AddListener(OnChangeSetting);
    }

    private void OnChangeSetting()
    {
        OnChangeSetting(SettingManager.Instance.GetSetting().language);
    }
    private void OnChangeSetting(Language language)
    {
        string targetText= TextTable.Instance.GetText(index).GetString(language);

        // 같은 텍스트면 무시
        if(targetText  == textObject.text) return;

        // text가 꺼져있으면 그냥 바로 바꾸기
        if(!textObject.enabled) textObject.text = targetText;
        else this.SafeStartCoroutine(ref coroutine, SlowChangeText(textObject.text, targetText,DURATION));
    }

    IEnumerator SlowChangeText(string start, string end, float duration)
    {
        // 사라지기와 나타나기를 한 duration에서 진행하기 위해 나누기
        duration /= 2;

        int startLength = start.Length;
        int endLength = end.Length;

        float elapsed = 0f;

        //감소
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            int len = startLength - (int)(t * startLength);
            textObject.text = start.Substring(0, len);
            yield return null;
        }

        elapsed = 0;

        //증가
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            int len = (int)(t * endLength);
            textObject.text = end.Substring(0, len);
            yield return null;
        }
    }
}
