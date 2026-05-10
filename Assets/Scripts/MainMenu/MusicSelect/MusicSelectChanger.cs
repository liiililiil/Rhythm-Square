using System.Collections.Generic;
using AudioManagement;
using UnityEngine;

public class MusicSelectChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private Vector2 sensitivity;

    private List<MusicSelectableObjectBase> musicObjects = new List<MusicSelectableObjectBase>();

    // 실제 위치
    private float result;
    private float target;

    // 정보 저장
    private float position;
    private int index;

    // 이동속도
    private const float smoothSpeed = 7f;

    private void Start()
    {
        // 이벤트 리스너 등록
        MusicSelectManager.Instance.onChangePosition.AddListener(OnChangePosition);
        MusicSelectManager.Instance.onChangeIndex.AddListener(OnChangeIndex);

        PlayableMusicSender.Instance.onLoadMusicInfo.AddListener(OnLoadInfo);
    }

    private void OnLoadInfo(MusicInfo musicInfo)
    {
        // 정보 받으면 그 정보대로 생성
        GameObject musicObject = Instantiate(prefab, transform);
        IBindable<MusicInfo> bindable = musicObject.GetComponent<IBindable<MusicInfo>>();
        if (bindable != null)
        {
            bindable.Bind(musicInfo);
        }

        musicObjects.Add(musicObject.GetComponent<MusicSelectableObjectBase>());
    }


    #region Value Update
    // 포지션은 바로 반영
    private void OnChangePosition(float position)
    {
        this.position = position;
        OnChangeValue();
    }

    private void OnChangeIndex(int index)
    {
        this.index = index;
        OnChangeValue();
    }
    private void OnChangeValue()
    {
        target = index + position;
    }
    #endregion



    private void Update()
    {
        result = Mathf.Lerp(result, target, 1 - Mathf.Exp(-smoothSpeed * Time.deltaTime));

        UpdatePosition(result);

    }

    private void UpdatePosition(float value)
    {
        for (int i = 0; i < musicObjects.Count; i++)
        {
            float x = sensitivity.x * (i - value);
            float y = sensitivity.y * (i - value);
            Vector2 vector2 = new Vector2(x, y);

            musicObjects[i].PositionUpdate(vector2);
        }
    }

    // 이 클래스의 활성화 여부에 따라 하위 오브젝트의 활성화도 같이 변경
    private void OnEnable()
    {
        foreach (var obj in musicObjects)
        {
            obj.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (var obj in musicObjects)
        {
            obj.gameObject.SetActive(false);
        }
    }




}
