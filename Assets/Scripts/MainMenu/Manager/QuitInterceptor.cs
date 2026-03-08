using System.Collections;
using UnityEngine;

using Type.Menu;

public class QuitInterceptor : MonoBehaviour
{
    private bool isQuittingHandled = false;
    public void RequestQuit()
    {
        if (isQuittingHandled) return;

        isQuittingHandled = true;
        StartCoroutine(QuitRoutine());
    }

    private IEnumerator QuitRoutine()
    {
        yield return StateChange();
    }

    private IEnumerator StateChange()
    {
        MenuStateManager.Instance.ChangeMenuState(MenuState.ExitWarning);
        yield return null;
    }

}
