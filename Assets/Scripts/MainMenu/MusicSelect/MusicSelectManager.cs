using SimpleActions;
using UnityEngine;
using UnityEngine.InputSystem;

public class MusicSelectManager : MonoBehaviour
{
    SimpleEvent<float> onChangePosition = new SimpleEvent<float>();

    private void Awake()
    {


    }
    void Update()
    {
        // if (Input.GetAxis())
    }
}
