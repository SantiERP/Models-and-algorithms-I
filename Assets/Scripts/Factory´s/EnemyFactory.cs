using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance { get; private set; }

    [SerializeField] Enemies _enemyPrefab;
    [SerializeField] int _initialAmount;
    [SerializeField] Transform _gameplay;

    Pool<Enemies> _myEnemyPool;

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        _myEnemyPool = new Pool<Enemies>(CreatorMethod, Enemies.TurnOn, Enemies.TurnOff, _initialAmount);
    }

    Enemies CreatorMethod()
    {
        var enemy= Instantiate(_enemyPrefab);
        enemy.transform.SetParent(_gameplay);
        return enemy;
    }

    public Enemies GetObjectFromPool()
    {
        return _myEnemyPool.GetObject();
    }

    public void ReturnToPool(Enemies obj)
    {
        _myEnemyPool.ReturnObject(obj);
    }
}
