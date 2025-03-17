using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLose : MonoBehaviour, IScreen
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

    public void BTN_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void BTN_Exit()
    {
        SceneManager.LoadScene("Menu");

    }

}
