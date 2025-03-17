using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOptions : MonoBehaviour, IScreen
{
    public void Activate()
    {

    }

    public void Deactivate()
    {
    }

    public void Free()
    {
        Destroy(gameObject);
    }

    public void BTN_Back()
    {
        ScreenManager.Instance.Pop();
    }
}
