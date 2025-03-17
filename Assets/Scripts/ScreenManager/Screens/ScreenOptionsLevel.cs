using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenOptionsLevel : MonoBehaviour, IScreen
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

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void BTN_Back()
    {
        GameManager.Instance.OnUnPause();
        ScreenManager.Instance.Pop();
    }

    public void BTN_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BTN_ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
