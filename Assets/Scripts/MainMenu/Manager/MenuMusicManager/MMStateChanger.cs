using AudioManagement;
using Tables.MusicTable;
using Type.Enums.Addressable;
using Type.Enums.Menu;
namespace MenuMusicManagers
{

    public class MMStateChanger : MMConnector
    {

        private BackGroundInfo backGroundInfo;

        protected override void OnBind()
        {
            MenuStateManager.Instance.onMenuStateChanged.AddListener(OnMenuStateChanged);
            musicManager.onClipChange.AddListener(BackGroundInfoBind);
        }


        private void BackGroundInfoBind(MusicIndex musicIndex)
        {
            backGroundInfo = MusicTable.Instance.GetBackGroundInfo(musicIndex);
        }


        private void OnMenuStateChanged(MenuState newState)
        {


            switch (newState)
            {
                case MenuState.InitLoading:
                    break;

                case MenuState.InitWaitng:
                    musicManager.RangeStart(backGroundInfo.high.startAt);
                    break;

                case MenuState.Main:
                    musicManager.RangeLoop(backGroundInfo.high.loop);
                    break;

                case MenuState.Setting:
                    musicManager.RangeLoop(backGroundInfo.middle.loop);
                    break;

                case MenuState.Credits:
                    musicManager.RangeLoop(backGroundInfo.low.loop);
                    break;

                case MenuState.ExitWarning:
                    musicManager.RangeLoop(backGroundInfo.low.loop);
                    break;

                case MenuState.ExitWating:
                    musicManager.RangeStart(backGroundInfo.low.endAt);
                    break;

                default:
                    musicManager.Stop();
                    break;

            }
        }



    }
}