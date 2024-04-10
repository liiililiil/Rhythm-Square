using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class TextViewer : MonoBehaviour
{
    public int Type;
    public ButtonMaster ButtonMaster;
    public SoundMaster SoundMaster;
    private string TargetText = "Error";
    private string TextRecord = "Error";
    private int Time;
    private int SelectRecord = -1;
    private Text Text;
    private Coroutine ShortCoroutine;
    private Coroutine LongCoroutine;
    void Awake()
    {
        Text = GetComponent<Text>();
    }
    void Update()
    {
        if(ButtonMaster.InGameSelect+ Mathf.RoundToInt(ButtonMaster.InGameSelectDump) != SelectRecord){
    
            SelectRecord = ButtonMaster.InGameSelect+ Mathf.RoundToInt(ButtonMaster.InGameSelectDump);

            if(ShortCoroutine != null) StopCoroutine(ShortCoroutine);
            if(LongCoroutine != null) StopCoroutine(LongCoroutine);
            
            if(Type ==0)
                TargetText = SoundMaster.Song[ButtonMaster.InGameSelect+ Mathf.RoundToInt(ButtonMaster.InGameSelectDump)].Artist;
            else if(Type ==1)
                TargetText = SoundMaster.Song[ButtonMaster.InGameSelect+ Mathf.RoundToInt(ButtonMaster.InGameSelectDump)].Title;
            else if(Type ==2)
                TargetText = SoundMaster.Song[ButtonMaster.InGameSelect+ Mathf.RoundToInt(ButtonMaster.InGameSelectDump)].Bpm.ToString();
            else if(Type ==3)
                TargetText = SoundMaster.Song[ButtonMaster.InGameSelect+ Mathf.RoundToInt(ButtonMaster.InGameSelectDump)].Difficulty.ToString();

            
            ShortCoroutine = StartCoroutine("TextShort");

            
        }

    }

    IEnumerator TextShort(){
        Time = Text.text.Length;
        while(Time > 0){
            if(TextRecord == TargetText) break;
            yield return null;
            yield return null;
            Text.text = Text.text.Substring(0,Text.text.Length -1);
            Time--;
        }

        TextRecord = TargetText;

        LongCoroutine = StartCoroutine("TextLong");
    }

    IEnumerator TextLong(){
        while(Time <= TargetText.Length){
            yield return null;
            yield return null;
            Text.text = TargetText.Substring(0,Time);
            Time++;
        }
    }

}
