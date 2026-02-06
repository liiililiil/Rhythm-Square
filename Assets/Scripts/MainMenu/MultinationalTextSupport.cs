using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Utils;
using Types.Menu;
using ScriptManagement;


[RequireComponent(typeof(Text))]
[AddComponentMenu("UI/MultinationalText", 100)]
public class MultinationalTextSupport : MonoBehaviour
{
    [SerializeField]
    private MultinationalString text;
    private Text textObject;

    private Coroutine coroutine;
    private const float DURATION = 0.5f;

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
        OnChangeSetting(SettingManager.Instance.setting);
        SettingManager.Instance.onChangeSetting.AddListener(OnChangeSetting);
    }

    private void OnChangeSetting(Setting setting)
    {
        // text가 꺼져있으면 그냥 바로 바꾸기
        if(!textObject.enabled) textObject.text = text.GetString(SettingManager.Instance.setting.language);
        else this.SafeStartCoroutine(ref coroutine, SlowChangeText(textObject.text, text.GetString(setting.language),DURATION));
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
