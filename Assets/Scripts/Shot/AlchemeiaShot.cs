using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class AlchemeiaShot : IShot
{
    Bullet _bullet;
    Transform _myTransform;
    Enemies _target;
    Vector3 _velocity;
    float _maxObserverDistance;
    float _distanceFactor;
    float _speed;
    float _minSpeed;
    float _timetotravel;
    float _distanceToImpact;
    float _addGravityUp;
    float _addHeight;
    float _splitGravity;
    float _addGravityDown;
    float _highestPoint;
    float _radius;
    float _damage;
    float _lifeTime;
    float _currentLifeTime;
    bool _arriveMaxpoint;


    public AlchemeiaShot(Bullet MyBullet, Transform thistransfor, 
        Vector3 gravity, float LifeTime, Enemies Enemy, 
        float speed,float MinSpeed, float duration, float DistanceToImpact, 
        float Damage, float AddOfHeight, float AddGravityUp,
        float SplitGravity, float AddGravityDown, float Radius)
    {
        _bullet = MyBullet;
        _myTransform = thistransfor;
        _target = Enemy;
        _lifeTime = LifeTime;
        _timetotravel = duration;
        _speed = speed;
        _minSpeed = MinSpeed;
        _distanceToImpact = DistanceToImpact;
        _damage = Damage;
        _addGravityDown = AddGravityDown;
        _addGravityUp = AddGravityUp;
        _splitGravity = SplitGravity;
        _addHeight = AddOfHeight;
        _highestPoint = _myTransform.position.y + _addHeight;
        _radius = Radius;
        Vector3 maxdistance = _target.transform.position - _myTransform.position;
        _maxObserverDistance = maxdistance.magnitude;
    }

    public void SearchEnemy()
    {
        
        Vector3 dis = _target.transform.position - _myTransform.position;
        if (dis.magnitude < _distanceToImpact)
        {
            Explode();
        }
        else
        {
            foreach (Enemies e in GameManager.Instance.EnemiesInScene)
            {
                Vector3 distance = e.transform.position - _myTransform.position;
                if (distance.magnitude < _distanceToImpact)
                {
                    Explode();
                    break;
                }
            }
        }

    }

    public IEnumerator CorutineShot()
    {
        float velocityx = (_target.transform.position.x - _myTransform.position.x) / _splitGravity;
        float velocityy = (_target.transform.position.y - _myTransform.position.y + 0.5f * _addGravityDown * _splitGravity * _splitGravity) / _splitGravity;
        _velocity = new Vector3(velocityx, velocityy, 0);

        while (true) 
        {
            yield return new WaitForSeconds(_timetotravel);

            while (_myTransform.position.y < _highestPoint && !_arriveMaxpoint)
            {
                yield return new WaitForSeconds(_timetotravel);
                _velocity.y += _addGravityUp * Time.fixedDeltaTime * _speed;
                _myTransform.position += _velocity * Time.fixedDeltaTime;
                if (_myTransform.position.y >= _highestPoint)  _arriveMaxpoint = true;             
            }
            Vector3 dis = _target.transform.position - _myTransform.position;
            float CurretnDistance = dis.magnitude;
            if (CurretnDistance > _maxObserverDistance) _maxObserverDistance = CurretnDistance;
            _distanceFactor = CurretnDistance / _maxObserverDistance;

            _speed = Mathf.Lerp(_minSpeed, _speed, _distanceFactor);
            _speed = Mathf.Min(_minSpeed, _speed);
            _velocity.y -= _addGravityDown * Time.fixedDeltaTime * _speed;
            _myTransform.position += _velocity * Time.fixedDeltaTime;
            _currentLifeTime += Time.deltaTime;
            if(_currentLifeTime >= _lifeTime) SearchInList();
            SearchEnemy();

        }
    }
    void SearchInList()
    {
        _currentLifeTime = 0;
        foreach (Enemies e in GameManager.Instance.EnemiesInScene)
        {
            Vector3 distance = e.transform.position - _myTransform.position;
            if (distance.magnitude < _distanceToImpact)
            {
                Explode();
                break;
            }
        }

    }
    void Explode()
    {
        _myTransform.position = _target.transform.position;
        Collider2D[] EnemiesHit = Physics2D.OverlapCircleAll(_myTransform.position, _radius);

        foreach (Collider2D item in EnemiesHit)
        {
            if(item.TryGetComponent(out Enemies enemies))
            {
                enemies.ModifyLife(_damage);
            }
        }
        BulletFactory.Instance.ReturnToPool(_bullet);
    }
}
