using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBuilder 
{
    Func<Bullet> _bulletFactory;
    Enemies _target;
    TypeofShots _typeofShots;
    Vector3 _position;
    Vector3 _direction;
    float _velocity;
    float _lifetime;
    float _corutineTime;
    float _scale;
    float _distanteToImpact;
    int _damage;
    Sprite _sprite;


    public BulletBuilder(Func<Bullet> bulletFactory)
    {
        _bulletFactory = bulletFactory;
    }

    public BulletBuilder SetTarget(Enemies Target)
    {
        _target = Target;
        return this;
    }

    public BulletBuilder SetPosition(Vector3 position)
    {
        _position = position;
        return this;
    }

    public BulletBuilder SetSpeed(float velocity)
    {
        _velocity = velocity;
        return this;
    }

    public BulletBuilder SetLifeTime(float lifetime)
    {
        _lifetime = lifetime;
        return this;
    }

    public BulletBuilder SetTimeInCorutine(float duration)
    {
        _corutineTime = duration;
        return this;
    }

    public BulletBuilder SetSprite(Sprite sprite)
    {
        _sprite = sprite;
        return this;
    }
    public BulletBuilder SetDamage(int damage)
    {
        _damage = damage;
        return this;
    }

    public BulletBuilder SetShot(TypeofShots type)
    {
        _typeofShots = type;
        return this;
    }

    public BulletBuilder SetScale(float scale)
    {
        _scale = scale;
        return this;
    }
    public BulletBuilder SetDistanceToimpact(float distance)
    {
        _distanteToImpact = distance;
        return this;
    }
    public Bullet Done()
    {
        var bullet = _bulletFactory();

        bullet.transform.position = _position;
        bullet.Target = _target;
        bullet.MaxLifeTime = _lifetime;
        bullet.Speed = _velocity;
        bullet.Direction = _direction;
        bullet.SpriteBullet = _sprite;
        bullet.Damage = _damage;
        bullet.TimeInCorutine = _corutineTime;
        bullet.Shots = _typeofShots;
        bullet.Scale = _scale;
        bullet.DistanceToImpact = _distanteToImpact;

        return bullet;
    }
}
