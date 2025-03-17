using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenWin : MonoBehaviour, IScreen
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

    
    public void BTN_Continue(int ToGoNextLevel)
    {
        if(SceneManager.GetActiveScene().name != "Level_" + ToGoNextLevel)
        SceneManager.LoadScene("Level_"+ToGoNextLevel);
        else SceneManager.LoadScene("Menu");

    }
    public void BTN_Exit()
    {
        SceneManager.LoadScene("Menu");

    }
}
