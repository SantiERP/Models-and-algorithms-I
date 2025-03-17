using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSelectLevel : MonoBehaviour, IScreen
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

    public void BTN_GoToLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
