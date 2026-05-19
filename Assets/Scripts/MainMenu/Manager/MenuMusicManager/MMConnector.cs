
using UnityEngine;

namespace MenuMusicManagers
{
    public abstract class MMConnector : MonoBehaviour
    {
        protected AudioSource audioSource = new AudioSource();
        protected AudioSource otherSource = new AudioSource();

        protected MenuMusicManager musicManager;

        // 바인딩
        public void Bind(MenuMusicManager musicManager)
        {
            this.musicManager = musicManager;

            musicManager.onUpdate.AddListener(OnUpdate);
            musicManager.onSourceChange.AddListener(SourceChange);

            OnBind();
        }

        protected virtual void OnBind() { }

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
