using UnityEngine;


using AudioManagement;
using ScriptManagement;
using Utils;
using Types.Addressable.Table;
using Types.Addressable;

namespace Tables.MusicTable
{
    public class MusicTable : Managers<MusicTable>
    {
            EachLoader<Music, MusicIndex> music = new EachLoader<Music, MusicIndex>();
            EachLoader<MusicInfo, MusicIndex> info = new EachLoader<MusicInfo, MusicIndex>();
            EachLoader<PlayableMusic, MusicIndex> playerable = new EachLoader<PlayableMusic, MusicIndex>();

            private void Awake() {
                Load();
                Singleton(true);
            }
            
            public void Load()
            {
                this.SafeStartCoroutine(ref music.coroutine, music.LoadingAsset(Type.Addressable.Tag.Audio.MUSIC));
                this.SafeStartCoroutine(ref info.coroutine, info.LoadingAsset(Type.Addressable.Tag.Audio.MUSICINFO));
                this.SafeStartCoroutine(ref playerable.coroutine, playerable.LoadingAsset(Type.Addressable.Tag.Audio.PLAYERABLE));
            }

            public void Release(MusicIndex ignore)
            {
                music.Release(ignore);
                info.Release(ignore);
                playerable.Release(ignore);
            }
            public MultinationalString GetMainMenuText(TextIndex textIndex)
            {
                // return mainMenu.table[textIndex];
                throw new System.Exception();
            }

            //메인메뉴 텍스트 해제

    }
}