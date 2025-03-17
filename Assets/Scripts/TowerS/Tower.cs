using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public List<Enemies> _enemysInRadius = new List<Enemies>();

    [Header("General Stats")]
    [SerializeField] protected TypeofShots _shotType;
    [SerializeField] protected Sprite _spriteToTowerBullet;
    public float Cost;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _attackRangeX;
    [SerializeField] protected float _attackRangeY;
    [SerializeField] protected float _countShot;
    [SerializeField] protected float _timeCorutine;
    [SerializeField] protected float _enemyImpactDistance;
    [SerializeField] protected float _lifeTime;
    [SerializeField] protected int _damage;

    protected float _count;
    protected float _angle;
    protected Bullet _bullet;

    public abstract void Shot(Enemies e);

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 center = transform.position;

        for (int i = 0; i < 360; i += 10)
        {
            float angle = i * Mathf.Deg2Rad;
            float x = _attackRangeX * Mathf.Cos(angle);
            float y = _attackRangeY * Mathf.Sin(angle);
            Gizmos.DrawSphere(new Vector3(center.x + x, center.y + y, 0), 0.1f);
        }
    }

    public void Check()
    {
        foreach (Enemies item in GameManager.Instance?.EnemiesInScene)
        {
            Vector3 dis = item.transform.position - transform.position;
            float x = item.transform.position.x - transform.position.x;
            float y = item.transform.position.y - transform.position.y;

            if (IsItInTheAttackArea(x, y))
            {
                if (!_enemysInRadius.Contains(item))
                    _enemysInRadius.Add(item);
                if (!item.gameObject.activeSelf &&
                    _enemysInRadius.Contains(item)) _enemysInRadius.Remove(item);
            }
            else _enemysInRadius.Remove(item);

        }
    }

    public virtual void SearchAndShot()
    {
        Check();
        if (_enemysInRadius.Count > 0)
        {
            if (_count < 0)
            {
                Shot(_enemysInRadius[0]);
                _count = _countShot;
            }
            _count -= Time.deltaTime;
        }
    }

    void Win()
    {
        Destroy(this);
    }

    bool IsItInTheAttackArea(float x, float y)
    {
        return (x * x) / (_attackRangeX*_attackRangeX) + 
            (y * y) /(_attackRangeY*_attackRangeY) <= 1;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnWin += Win;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnWin -= Win;

    }
}
