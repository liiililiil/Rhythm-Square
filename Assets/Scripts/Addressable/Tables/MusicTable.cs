using AudioManagement;
using Type.Addressable;
using Type.Enums.Addressable;
using Type.Consts.Addressable.Tag;

namespace Tables.MusicTable
{
    public class MusicTable : Managers<MusicTable>
    {
        EachLoader<Music, MusicIndex> music;
        EachLoader<MusicInfo, MusicIndex> info;
        EachLoader<PlayableMusic, MusicIndex> playable;
        EachLoader<BackGroundInfo, MusicIndex> backgroundInfo;

        private void Awake()
        {
            Singleton(true);
        }

        private void Start()
        {
            music = new EachLoader<Music, MusicIndex>(MenuAssetLoadManager.Instance.addressableLoadRecoder);
            info = new EachLoader<MusicInfo, MusicIndex>(MenuAssetLoadManager.Instance.addressableLoadRecoder);
            playable = new EachLoader<PlayableMusic, MusicIndex>(MenuAssetLoadManager.Instance.addressableLoadRecoder);
            backgroundInfo = new EachLoader<BackGroundInfo, MusicIndex>(MenuAssetLoadManager.Instance.addressableLoadRecoder);

        }

        public void Load()
        {
            music.Load(this, Audio.MUSIC);
            info.Load(this, Audio.MUSICINFO);
            playable.Load(this, Audio.PLAYERABLE);
            backgroundInfo.Load(this, Audio.BACKGROUNDINFO);
        }

        private void ClipPreload()
        {
            foreach (var music in music.GetAll())
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
            return music.Get(musicIndex);
        }

        public MusicInfo GetMusicInfo(MusicIndex musicIndex)
        {
            return info.Get(musicIndex);
        }

        public PlayableMusic GetPlayableMusic(MusicIndex musicIndex)
        {
            return playable.Get(musicIndex);
        }

        public PlayableMusic[] GetPlayableMusic()
        {
            return playable.GetAll();
        }

        public BackGroundInfo GetBackGroundInfo(MusicIndex musicIndex)
        {
            return backgroundInfo.Get(musicIndex);
        }

        public T Get<T>(MusicIndex index) where T : IndexedScriptableObject<MusicIndex>
        {
            if (typeof(T) == typeof(Music))
                return music.Get(index) as T;
            else if (typeof(T) == typeof(MusicInfo))
                return info.Get(index) as T;
            else if (typeof(T) == typeof(PlayableMusic))
                return playable.Get(index) as T;
            else if (typeof(T) == typeof(BackGroundInfo))
                return backgroundInfo.Get(index) as T;
            else
                return null;
        }
    }
}