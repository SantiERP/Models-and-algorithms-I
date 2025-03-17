using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventEntity : MonoBehaviour
{
    public static EventEntity Instance { get; private set; }

    public Func<RectTransform> UISelectFunc;
    public Func<RectTransform> UIDestroyFunc;

    public Action<int, int> RefeshUI;
    public Action StartHorde;
    public Action SpecialEnemy;
    public Action FinishHorde;
    public Action<GameObject> AdminUITower;
    public Func<KeyValuePair<GameObject, GameObject>> OffUITower;

    private void Awake()
    {
        if(Instance != null & Instance!=this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
}
