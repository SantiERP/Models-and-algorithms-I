using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    public static TowerFactory Instance;

    [SerializeField] Tower _towers;
    [SerializeField] int _initialAmount;


    Pool<Tower> _pool;
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

    }

    Tower CreatorMethod()
    {
        return Instantiate(_towers);
    }

    public Tower GetObjectFromPool()
    {
        return _pool.GetObject();
    }

    public void ReturnToPool(Tower obj)
    {
        _pool.ReturnObject(obj);
    }

}
