using System.Collections;
using Extensions;
using Type.Enums.Settings;
using Types.Utils;
using UnityEngine;
namespace MenuMusicManagers
{

    public class MMLoop : MMConnector
    {
        private bool isLoop;
        private FloatRange currentRange;
        private FloatRange previousRange;
        private Coroutine loopCoroutine;

        private bool smoothLooping;

        protected override void OnUpdate()
        {
            AudioLoop();
        }

        private void AudioLoop()
        {

            // 반복 설정 안하면 넘기기
            if (!isLoop) return;

            if (previousRange == null) return;

            if (audioSource.time >= currentRange.end)
            {
                // 부드럽게 이동해야하면 처리
                if (smoothLooping)
                {
                    musicManager.SourceChanged();
                    audioSource.time = currentRange.start;
                }
                else
                {
                    audioSource.time -= currentRange.end - currentRange.start;
                }



                musicManager.onTimeChange.Invoke(audioSource.time);
            }
        }

        public void RangeStart(FloatRange range, FloatRange nextLoop = null, bool isGoingToPart = true)
        {
            isLoop = false;
            RangeChange(range);

            audioSource.time = range.start;

            // 노래를 Loop 않고 재생할땐 소리를 바로 최대로
            musicManager.SetVolume(SettingManager.Instance.GetConfigValue<float>(ConfigType.Music));

            musicManager.onTimeChange.Invoke(audioSource.time);

            if (nextLoop != null) musicManager.SafeStartCoroutine(ref loopCoroutine, LoopWait(range, nextLoop, isGoingToPart));
            else musicManager.SafeStopCoroutine(ref loopCoroutine);
        }

        public void RangeLoop(FloatRange range, bool isSmoothLoop = false, bool adaptiveloop = true, bool isGoingToPart = true)
        {
            smoothLooping = isSmoothLoop;

            isLoop = true;
            musicManager.SourceChanged();

            RangeChange(range);

            //  audio의 time이 range에 없으면 그 range안으로 오도록 조정
            if (!(otherSource.time >= range.start && otherSource.time <= range.end))
            {

                // 적응형 루프면 시간 조정, 아니면 바로 range 시작점으로 이동
                if (adaptiveloop)
                    AdaptTime(isGoingToPart);
                else
                    audioSource.time = range.start;
            }
            else
            {
                // 이미 range 안이면 시간 바로 적용
                audioSource.time = otherSource.time;
            }

            musicManager.onTimeChange.Invoke(audioSource.time);
        }


        private void AdaptTime(bool isGoingToPart)
        {
            // 이동까지의 시간 계산
            float delta = currentRange.start - previousRange.start;
            float targetTime = otherSource.time + delta;

            // 만약 앞으로 이동해야할때 IsGoingToPart이 false면 이동 안함
            if (delta > 0 && !isGoingToPart)
            {
                return;
            }

            // 적응형 오디오를 위해 이전 파츠와 이어지도록 조정
            targetTime = currentRange.start + Mathf.Repeat(targetTime - currentRange.start, currentRange.end - currentRange.start);
            audioSource.time = targetTime;

        }


        private void RangeChange(FloatRange floatRange)
        {
            previousRange = currentRange;
            currentRange = floatRange;
        }

        private IEnumerator LoopWait(FloatRange end, FloatRange target, bool isGoingToPart)
        {
            //end에 도달할때 까지 대기
            while (audioSource.time < end.end)
            {
                yield return null;
            }

            //적용
            musicManager.RangeLoop(target, isGoingToPart);
        }

    }

}