using UnityEngine;


using AudioManagement;
using ScriptManagement;
using Utils;
using Type.Addressable.Table;
using Type.Addressable;
using System.Linq;

namespace Tables.MusicTable
{
    public class MusicTable : Managers<MusicTable>
    {
        EachLoader<Music, MusicIndex> music = new EachLoader<Music, MusicIndex>();
        EachLoader<MusicInfo, MusicIndex> info = new EachLoader<MusicInfo, MusicIndex>();
        EachLoader<PlayableMusic, MusicIndex> playable = new EachLoader<PlayableMusic, MusicIndex>();
        EachLoader<BackGroundInfo, MusicIndex> backgroundInfo = new EachLoader<BackGroundInfo, MusicIndex>();

        private void Awake()
        {
            Singleton(true);
        }

        private void Start()
        {
            music.RecoderBind(MenuAssetLoadManager.Instance.addressableLoadRecoder);
            info.RecoderBind(MenuAssetLoadManager.Instance.addressableLoadRecoder);
            playable.RecoderBind(MenuAssetLoadManager.Instance.addressableLoadRecoder);
            backgroundInfo.RecoderBind(MenuAssetLoadManager.Instance.addressableLoadRecoder);

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

        public PlayableMusic GetPlayableMusic(MusicIndex musicIndex)
        {
            return playable.table[musicIndex];
        }

        public PlayableMusic[] GetPlayableMusic()
        {
            return playable.table.Values.ToArray();
        }

        public BackGroundInfo GetBackGroundInfo(MusicIndex musicIndex)
        {
            return backgroundInfo.table[musicIndex];
        }

    }
}