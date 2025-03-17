using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemies : MonoBehaviour
{
    Image _lifeUI;

    
    void Start()
    {
        _lifeUI = GetComponent<Image>();
    }

    public void UpdateBar()
    {
        //_lifeUI.fillAmount =Mathf.Lerp(0, 1, )
    }
}
