using AudioManagement;
using SimpleActions;
using Tables.MusicTable;
using Type.Enums.Addressable;
using UnityEngine;

namespace MenuMusicManagers
{
    public class MMConductor : MMConnector
    {
        public SimpleEvent OnBeat { get; private set; } = new SimpleEvent();
        public float beatPerSec { get; private set; } = 1;
        public int beat { get; private set; }

        private float nextBeat = 999999;

        protected override void OnUpdate()
        {
            BeatDetection();
        }

        // 비트 감지
        private void BeatDetection()
        {
            if (audioSource.isPlaying && audioSource.time >= nextBeat)
            {
                OnBeat.Invoke();
                beat++;

                nextBeat += beatPerSec;
            }
        }


        private void BeatTimeCorrection(float time)
        {
            beat = (int)(time / beatPerSec);
            nextBeat = (beat + 1) * beatPerSec;
        }

        public MMConductor(MenuMusicManager musicManager) : base(musicManager)
        {
            musicManager.onTimeChange.AddListener(BeatTimeCorrection);
            musicManager.onClipChange.AddListener(LoadMusicInfo);
        }

        public void LoadMusicInfo(MusicIndex musicIndex)
        {
            MusicInfo musicInfo = MusicTable.Instance.GetMusicInfo(musicIndex);

            beatPerSec = 60 / musicInfo.bpm;
        }
    }


}
