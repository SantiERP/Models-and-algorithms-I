using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeManager : MonoBehaviour
{
    public static HordeManager Instance;
    public List<Horde> _hordeList;
    public int NumberOfHordes;

    private void Awake()
    {
        if (Instance != null & Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }


}
