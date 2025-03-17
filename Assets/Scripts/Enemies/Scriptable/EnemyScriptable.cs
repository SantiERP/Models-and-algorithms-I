using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/ScriptableEnemy", order = 2)]
public class EnemyScriptable : ScriptableObject
{
    public float MaxLife;
    public int Reward;
    public float Speed;
    public Sprite _renderer;
    public SpecialEnemies IsSpecialEnemies;
}




