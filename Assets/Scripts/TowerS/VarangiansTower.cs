using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class VarangiansTower : Tower
{
    [Header("Own Stats")]
    [SerializeField] float _timeChange;
    [SerializeField] float _speedRotation;


    public override void Shot(Enemies e)
    {
       _bullet = new BulletBuilder(BulletFactory.Instance.GetObjectFromPool)
            .SetLifeTime(_lifeTime)
            .SetTarget(e)
            .SetPosition(transform.position)
            .SetSpeed(_bulletSpeed)
            .SetDamage(_damage * -1)
            .SetSprite(_spriteToTowerBullet)
            .SetShot(_shotType)
            .SetTimeInCorutine(_timeCorutine)
            .SetDistanceToimpact(_enemyImpactDistance)
            .Done();
        _bullet.VaragianStats(_speedRotation,_timeChange);
        _bullet.InstantiateBullet();
    }

    void Update()
    {
        SearchAndShot();    
    }


}
