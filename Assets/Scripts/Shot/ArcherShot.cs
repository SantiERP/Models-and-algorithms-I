using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherShot : IShot
{
    Bullet _bullet;
    Transform _myTransform;
    Enemies _target;

    float _speed;
    float _angle;
    float _duration;
    float _distanceToImpact;
    float _damage;

    public ArcherShot(Bullet MyBullet, Transform myT, Enemies Enemy, 
        float speed, float duration, float DistanceToImpact, float Damage)
    {
        _bullet = MyBullet;
        _myTransform = myT;
        _speed = speed;
        _duration = duration;
        _distanceToImpact = DistanceToImpact;
        _damage = Damage;
        _target = Enemy;
    }

    public IEnumerator CorutineShot()
    {
        while (true)
        {
            yield return new WaitForSeconds(_duration * Time.deltaTime);
            Vector3 direction =  _target.transform.position - _myTransform.position;
            _myTransform.position += direction.normalized * _speed * Time.deltaTime;
            _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _myTransform.rotation = Quaternion.Euler(0, 0, _angle);
            SearchEnemy();
        }
    }

    public void SearchEnemy()
    {
        Vector3 dis = _target.transform.position - _myTransform.position;
        if (dis.magnitude < _distanceToImpact)
        {
            BulletFactory.Instance.ReturnToPool(_bullet);
            _target.ModifyLife(_damage);
        }
    }

}
