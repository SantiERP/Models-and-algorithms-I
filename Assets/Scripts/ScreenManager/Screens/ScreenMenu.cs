using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMenu : MonoBehaviour, IScreen
{
    public void Activate()
    {
    }

    public void Deactivate()
    {
    }

    public void Free()
    {

    }

    void Start()
    {
        ScreenManager.Instance.Push(this);
    }

    void Update()
    {
        
    }

    public void BTN_Options()
    {
        ScreenManager.Instance.Push("Canvas OptionsMenu");
    }

    public void BTN_LevelSelector()
    {
        ScreenManager.Instance.Push("Canvas LevelSelector");
    }

    public void BTN_Exit()
    {
        Application.Quit();
    }
}
