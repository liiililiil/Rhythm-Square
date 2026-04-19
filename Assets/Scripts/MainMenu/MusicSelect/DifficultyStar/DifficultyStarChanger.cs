using System.Collections.Generic;
using Tables.MusicTable;
using UnityEngine;

public class DifficultyStarChanger : MonoBehaviour
{
    [SerializeField]
    private List<DifficultyStar> difficultyStars;

    private void Start()
    {
        MusicSelectManager.Instance.onChangeIndex.AddListener(OnChangeDifficulty);
    }
    private void OnChangeDifficulty(int index)
    {
        int difficulty = MusicTable.Instance.GetPlayableMusic()[index].difficultyLevel;
        int starCount = difficultyStars.Count;

        // 공통
        int basePhase = difficulty / starCount;

        // 추가
        int extraStars = difficulty % starCount;

        for (int i = 0; i < difficultyStars.Count; i++)
        {
            DifficultyStar difficultyStar = difficultyStars[i];

            int phase = basePhase + (i < extraStars ? 1 : 0);
            difficultyStars[i].SetPhase(phase);
        }
    }

    //(n + m - p) / m
    // p = 별들의 각 위치
    // n = 현재 난이도
    // m = 별들의 수


    // 이 오브젝트가 비활성화 되면 하위 객체도 비활성화
    // 이 클래스의 활성화 여부에 따라 하위 오브젝트의 활성화도 같이 변경
    private void OnEnable()
    {
        foreach (var difficultyStar in difficultyStars)
        {
            difficultyStar.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (var difficultyStar in difficultyStars)
        {
            difficultyStar.gameObject.SetActive(false);
        }
    }
}
