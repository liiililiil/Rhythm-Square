
using UnityEngine;

namespace MenuMusicManagers
{
    public abstract class MMConnector
    {
        protected AudioSource audioSource = new AudioSource();
        protected AudioSource otherSource = new AudioSource();

        protected MenuMusicManager musicManager;

        // 바인딩
        public MMConnector(MenuMusicManager musicManager)
        {
            this.musicManager = musicManager;

            musicManager.onUpdate.AddListener(OnUpdate);
            musicManager.onSourceChange.AddListener(SourceChange);
        }

        protected virtual void OnUpdate()
        {

        }

        protected void SourceChange(AudioSource newSource, AudioSource oldSource)
        {
            // 소스 교체
            audioSource = newSource;
            otherSource = oldSource;
        }
    }

}
