using System.Collections.Generic;
using UnityEngine;

namespace AddressableLoadingManagement
{
    // 에셋 로딩을 통합 관리하기위한 클래스
    public class AssetsPrograss
    {
        private int index;
        private int leftPrograss;

        private List<float> prograssList = new List<float>();
        private bool isRecode = false;
        public void StartRecode()
        {
            index = 0;
            leftPrograss = 0;
            prograssList.Clear();

            isRecode = true;
        }
        
        public void StartLoading(out int startIndex)
        {
            if(!isRecode)
            {
                Debug.LogError("기록 중이지 않습니다!");
                startIndex = -1;
                return;
            }

            startIndex = index;
            index++;
            leftPrograss++;

            prograssList.Add(0);
        }

        public void CompleteLoading(int loadIndex)
        {
            leftPrograss--;
            prograssList[loadIndex] = 1;
        }

        public float GetTotalPrograss()
        {
            float total = 0;
            foreach(var p in prograssList)
            {
                total += p;
            }

            return total / prograssList.Count;
        }

        public float GetLoadingPrograss(int loadIndex)
        {
            return prograssList[loadIndex];
        }

        public bool IsAllComplete()
        {
            return leftPrograss <= 0;
        }






    }
}