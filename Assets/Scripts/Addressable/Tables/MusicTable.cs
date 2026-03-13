using UnityEngine;


using AudioManagement;
using ScriptManagement;
using Utils;
using Type.Addressable.Table;
using Type.Addressable;

namespace Tables.MusicTable
{
    public class MusicTable : Managers<MusicTable>
    {
        EachLoader<Music, MusicIndex> music = new EachLoader<Music, MusicIndex>();
        EachLoader<MusicInfo, MusicIndex> info = new EachLoader<MusicInfo, MusicIndex>();
        EachLoader<PlayableMusic, MusicIndex> playable = new EachLoader<PlayableMusic, MusicIndex>();
        EachLoader<BackGroundInfo, MusicIndex> backgroundInfo = new EachLoader<BackGroundInfo, MusicIndex>();

        private void Awake() {
            Singleton(true);
        }

        private void Start() {
            music.RecoderBind(AssetLoadManager.Instance.addressableLoadRecoder);
            info.RecoderBind(AssetLoadManager.Instance.addressableLoadRecoder);
            playable.RecoderBind(AssetLoadManager.Instance.addressableLoadRecoder);
            backgroundInfo.RecoderBind(AssetLoadManager.Instance.addressableLoadRecoder);

        }
        
        public void Load()
        {
            this.SafeStartCoroutine(ref music.coroutine, music.LoadingAsset(Type.Addressable.Tag.Audio.MUSIC, ClipPreload));
            this.SafeStartCoroutine(ref info.coroutine, info.LoadingAsset(Type.Addressable.Tag.Audio.MUSICINFO));
            this.SafeStartCoroutine(ref playable.coroutine, playable.LoadingAsset(Type.Addressable.Tag.Audio.PLAYERABLE));
            this.SafeStartCoroutine(ref backgroundInfo.coroutine, backgroundInfo.LoadingAsset(Type.Addressable.Tag.Audio.BACKGROUNDINFO));
        }

        private void ClipPreload()
        {
            foreach (var music in music.table.Values)
            {
                music.audioClip.LoadAudioData();
            }
        }
        

        public void Release(MusicIndex ignore)
        {
            music.Release(ignore);
            info.Release(ignore);
            playable.Release(ignore);
        }
        public Music GetMusic(MusicIndex musicIndex)
        {
            return music.table[musicIndex];
        }

        public MusicInfo GetMusicInfo(MusicIndex musicIndex)
        {
            return info.table[musicIndex];
        }

        public PlayableMusic GetPlaybleMusic(MusicIndex musicIndex)
        {
            return playable.table[musicIndex];
        }

        public BackGroundInfo GetBackGroundInfo(MusicIndex musicIndex)
        {
            return backgroundInfo.table[musicIndex];
        }

    }
}