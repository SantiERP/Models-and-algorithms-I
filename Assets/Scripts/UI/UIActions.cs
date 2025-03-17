using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActions : MonoBehaviour
{
    [SerializeField] RectTransform UIselectTower;
    [SerializeField] RectTransform UIdestroyTower;

    private void Start()
    {
    }

    RectTransform SelectTower()
    {
        return UIselectTower;
    }
    RectTransform DestroyTower()
    {
        return UIdestroyTower;
    }

}
