using System.Collections.Generic;
using UnityEngine;

public class DifficuityStarManager : MonoBehaviour
{
    [SerializeField]
    private List<DifficuityStar> difficuityStars;

    private void OnChangeDifficuity(int difficuity)
    {
        for (int i = 0; i < difficuityStars.Count; i++)
        {
            DifficuityStar difficuityStar = difficuityStars[i];
            int phase = (difficuity + difficuityStars.Count - i + 1) / difficuityStars.Count;

            difficuityStar.SetPhase(phase);
        }
    }

    //(n + m - p) / m
    // p = 별들의 각 위치
    // n = 현재 난이도
    // m = 별들의 수

}
