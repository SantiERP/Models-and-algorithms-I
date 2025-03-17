using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Horde", menuName = "ScriptableObjects/ScriptableHorde", order = 1)]
public class HordeScriptable : ScriptableObject
{
    public List<AllHorde> Hordes;
}

[System.Serializable]
public class AllHorde
{
    public List<HordeData> SubHordes;
}

[System.Serializable]
public class HordeData
{
    public int EnemyCount;
    public int SplitHorde;
    public float SpawnInterval;
    public float LongspawnInterval;
    public EnemyScriptable EnemyToSpawn;
}

