using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{

    public override void Shot(Enemies e)
    {
        _bullet = new BulletBuilder(BulletFactory.Instance.GetObjectFromPool)
            .SetPosition(transform.position)
            .SetLifeTime(_lifeTime)
            .SetSpeed(_bulletSpeed)
            .SetTarget(e)
            .SetDamage(_damage * -1)
            .SetSprite(_spriteToTowerBullet)
            .SetShot(_shotType)
            .SetTimeInCorutine(_timeCorutine)
            .SetDistanceToimpact(_enemyImpactDistance)
            .Done();
        _bullet.InstantiateBullet();
    }

    void Update()
    {
        SearchAndShot();
    }
}
