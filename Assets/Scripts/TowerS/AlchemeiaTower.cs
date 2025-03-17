using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AlchemeiaTower : Tower
{
    [Header("Own Stats")]
    [SerializeField] float _addGravityUp;
    [SerializeField] float _addHeight;
    [SerializeField] float _splitGravity;
    [SerializeField] float _addGravityDown;
    [SerializeField] float _gravityMultipler;
    [SerializeField] float _radiusExplode;
    [SerializeField] float _minSpeed;

    Vector3 _gravity = Vector2.up;
    public override void Shot(Enemies e)
    {
        _bullet = new BulletBuilder(BulletFactory.Instance.GetObjectFromPool)
            .SetPosition(transform.position)
            .SetTarget(e)
            .SetLifeTime(_lifeTime)
            .SetSpeed(_bulletSpeed)
            .SetSprite(_spriteToTowerBullet)
            .SetShot(_shotType)
            .SetDamage(_damage * -1)
            .SetTimeInCorutine(_timeCorutine)
            .SetDistanceToimpact(_enemyImpactDistance)
            .Done();
        _bullet.AlchemeiaStats(_addHeight,_addGravityUp,_splitGravity,_addGravityDown,
            _gravity * _gravityMultipler, _radiusExplode, _minSpeed);
        _bullet.InstantiateBullet();
    }


    void Update()
    {
        SearchAndShot();
    }

}
