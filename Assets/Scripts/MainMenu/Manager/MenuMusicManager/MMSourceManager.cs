using System.Collections;
using Extensions;
using UnityEngine;
using SimpleEasing;
using Type.Enums.Settings;

namespace MenuMusicManagers
{
    public class MMSourceManager : MMConnector
    {
        private Coroutine sourceSwitchCoroutine;


        // 오디오 소스 교체 시간
        private const float SOURCE_FADE_DURATION = 1f;
        public MMSourceManager(MenuMusicManager musicManager) : base(musicManager)
        {
            musicManager.onSourceChange.AddListener(SourceChanged);

            //부모에서 상속받은 액션은 해제시키기
            musicManager.onSourceChange.RemoveListener(SourceChange);
        }

        public void AudioSourceChange()
        {
            musicManager.onSourceChange.Invoke(otherSource, audioSource);
        }
        private void SourceChanged(AudioSource newSource, AudioSource oldSource)
        {
            audioSource = newSource;
            otherSource = oldSource;
            audioSource.volume = 1;
            audioSource.Play();

            musicManager.SafeStartCoroutine(ref sourceSwitchCoroutine, SourceSlowChange());
        }

        private IEnumerator SourceSlowChange()
        {
            // 너무 빠른 변경으로 인한 버그 방지
            float elapsed = 0;
            float otherVolume = otherSource.volume;

            //오디오 천천히 전환하기
            while (elapsed <= SOURCE_FADE_DURATION)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / SOURCE_FADE_DURATION);

                t = Ease.Easing(t, EaseType.Linear);

                audioSource.volume = Mathf.Lerp(0, SettingManager.Instance.GetConfigValue<float>(ConfigType.Music), t);
                otherSource.volume = Mathf.Lerp(otherVolume, 0, t);

                yield return null;
            }

            //전환 완료되면 보정 및 끄기
            audioSource.volume = SettingManager.Instance.GetConfigValue<float>(ConfigType.Music);
            otherSource.Stop();

            musicManager.SafeStopCoroutine(ref sourceSwitchCoroutine);
        }
        public void SetVolume(float value)
        {
            //노래 변경 중이라면 무시
            if (sourceSwitchCoroutine != null) return;

            audioSource.volume = value;
        }
        public void Stop()
        {
            musicManager.SourceChanged();
            audioSource.Stop();
        }

        public void Play()
        {
            audioSource.Play();
        }

    }
}