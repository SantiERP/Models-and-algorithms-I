using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarangiansShot : IShot
{
    Transform _myTransform;
    Enemies _target;

    float _speed;
    float _angle;
    float _duration;
    float _distanceToImpact;
    float _timeToChange;
    float _time=0;
    float _speedRotation;
    float _damage;


    public VarangiansShot(Transform myT, Enemies Enemy,
        float speed, float duration, 
        float DistanceToImpact, float Damage,
        float SpeedRotation, float TimeToChange)
    {
        _myTransform = myT;
        _target = Enemy;
        _speed = speed;
        _duration = duration;
        _distanceToImpact = DistanceToImpact;
        _damage = Damage;
        _timeToChange = TimeToChange;
        _speedRotation = SpeedRotation;
    }

    public IEnumerator CorutineShot()
    {
        Vector3 direction = _target.transform.position - _myTransform.position;
        while (true)
        {
            yield return new WaitForSeconds(_duration * Time.deltaTime);
            _angle += _speedRotation;
            SearchEnemy();

            _myTransform.rotation = Quaternion.Euler(0, 0, _angle);
            if (_time >= _timeToChange)
            {
                _myTransform.position -= direction.normalized * _speed * Time.deltaTime;
            }
            else _myTransform.position += direction.normalized * _speed * Time.deltaTime;
            _time += Time.deltaTime;

        }
    }

    public void SearchEnemy()
    {
        foreach (Enemies e in GameManager.Instance.EnemiesInScene)
        {
            Vector3 dis = e.transform.position - _myTransform.position;
            if (dis.magnitude < _distanceToImpact)
            {
                if(e.EnemyScriptable.IsSpecialEnemies == SpecialEnemies.HeavyInfantry)
                {
                    _damage /= e.DamageReducer; 
                }
                e.ModifyLife(_damage);
            }
        }

    }
}
