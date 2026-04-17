using System.Collections;
using SimpleActions;
using Tables.MusicTable;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class MusicSelectManager : Managers<MusicSelectManager>
{
    public SimpleEvent<float> onChangePosition = new SimpleEvent<float>();

    public SimpleEvent<int> onChangeIndex = new SimpleEvent<int>();

    [SerializeField]
    private float position;
    [SerializeField]
    private int index;

    private int maxIndex;

    private Coroutine mouseInputCoroutine;

    private void Awake()
    {
        Singleton(false);
    }

    private void Start()
    {
        if (MenuAssetLoadManager.Instance.assetLoadRecoder.IsAllComplete())
        {
            GetMax();
        }
        else
        {
            MenuAssetLoadManager.Instance.AssetLoaderBind(GetMax);
        }
    }

    private void GetMax()
    {
        maxIndex = MusicTable.Instance.GetPlayableMusic().Length - 1;
    }
    private void Update()
    {
        MouseDetection();
    }



    public void KeyDetection(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();

        // 누를 때만
        if (value == Vector2.zero)
            return;

        Debug.Log(context.ReadValue<Vector2>());

        bool upInput = value.x == 1 || value.y == 1;
        bool downInput = value.x == -1 || value.y == -1;

        // 위
        if (upInput)
        {
            ChangePosition(1);
        }

        // 아래
        if (downInput)
        {
            ChangePosition(-1);
        }

        ChangePosition(-position);
    }

    private void MouseDetection()
    {
        if (mouseInputCoroutine != null) return;

        bool mouseInput = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
        if (mouseInput)
        {
            mouseInputCoroutine = StartCoroutine(MouseDetectionCoroutine());
        }
    }

    private IEnumerator MouseDetectionCoroutine()
    {
        bool mouseInput = true;

        float startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        while (mouseInput)
        {
            float currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            ChangePosition(-(startPos - currentPos));

            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            mouseInput = Input.GetMouseButton(0) || Input.GetMouseButton(1);
            yield return null;
        }

        ChangePosition(-position);
        this.SafeStopCoroutine(ref mouseInputCoroutine);
    }

    private void ChangePosition(float value)
    {
        float sum = value + index + position;
        if (sum <= -0.4f) value = Mathf.Max(sum, -0.4f) - (index + position);
        if (sum >= maxIndex + 0.4f) value = Mathf.Min(sum, maxIndex + 0.4f) - (index + position);

        position += value;
        onChangePosition.Invoke(position);

        int roundedPosition = Mathf.RoundToInt(position);
        if (roundedPosition != 0)
        {
            index += roundedPosition;
            position -= roundedPosition;

            onChangeIndex.Invoke(index);
        }
    }
}
