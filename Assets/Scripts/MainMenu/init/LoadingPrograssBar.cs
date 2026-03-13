using UnityEngine;
using UnityEngine.UI;

public class LoadingPrograssBar : MonoBehaviour
{
    [SerializeField]
    private GameObject bar;
    private Text text;

    RectTransform barRect;

    int index;
    int completeIndex;
    private void Awake()
    {
        barRect = bar.GetComponent<RectTransform>();
    }


    private void Start() {
        AssetLoadManager.Instance.loadRecoder.OnStartLoading.AddListener(add);
        AssetLoadManager.Instance.loadRecoder.OnCompleteLoading.AddListener(Complete);
        AssetLoadManager.Instance.addressableLoadRecoder.OnStartLoading.AddListener(add);
        AssetLoadManager.Instance.addressableLoadRecoder.OnCompleteLoading.AddListener(Complete);
    }

    private void add(int empty)
    {
        index++;
        BarUpdate();
    }

    private void Complete(int empty)
    {
        completeIndex++;
        BarUpdate();
    }
    private void BarUpdate()
    {
        if (index == 0 || completeIndex == 0) return;
        barRect.localScale = new Vector3((float)completeIndex / index, 1, 1);
        
    }

}
