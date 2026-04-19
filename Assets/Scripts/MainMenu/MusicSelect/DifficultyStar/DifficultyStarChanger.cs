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

}
