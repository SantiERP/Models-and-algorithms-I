using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TypeofShots
{
    Archer = 1,
    Alchemeia = 2,
    Varangians = 3
}

public class Bullet : MonoBehaviour, IForFactory
{
    #region General Stast
    Vector3 _direction;

    float _lifeTime;
    float _maxLifeTime;
    float _speed;
    float _angle;
    float _timeInCorutine;
    float _scale;
    float _distanceToImpact;
    Sprite _spriteBullet;
    SpriteRenderer _renderer;
    Enemies _target;
    float _damage;
    TypeofShots _shots;
    #endregion

    #region Varagians Stats
    float _timeChange;
    float _rotationSpeed;
    #endregion

    #region Alchemeia Stats
    float _addGravityUp;
    float _addHeight;
    float _splitGravity;
    float _addGravityDown;
    float _radius;
    float _minSpeed;
    Vector3 _gravity;
    #endregion

    public Enemies Target {  get { return _target; } set { _target = value; } }
    public Vector3 Direction {  get { return _direction; } set { _direction = value; } }
    public float MaxLifeTime {  get { return _maxLifeTime; } set { _maxLifeTime = value; _lifeTime = value; } }

    public float Speed { get { return _speed; } set { _speed = value; } }
    public float Anlge { get { return _angle; } set { _angle = value; } }
    public float TimeInCorutine { get { return _timeInCorutine; } set { _timeInCorutine = value; } }
    public float Scale { get { return _scale; } set { _scale = value; } }
    public float DistanceToImpact { get { return _distanceToImpact; } set { _distanceToImpact = value; } }

    public float Damage { get { return _damage; } set { _damage = value; } }
    public Sprite SpriteBullet {  get { return _spriteBullet; } set { _spriteBullet = value; } }
    public TypeofShots Shots { get { return _shots; } set { _shots = value; } }

    IShot ArcherShot;
    IShot KimiyaShot;
    IShot VarangiansShot;

    void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();

    }

    void Update()
    {
        _lifeTime -= Time.deltaTime;
        if( _lifeTime <= 0 ) 
        {
            BulletFactory.Instance.ReturnToPool(this);
        }
    }

    public void InstantiateBullet()
    {
        _renderer.sprite = _spriteBullet;
        switch (_shots)
        {
            case TypeofShots.Archer:
                ArcherShot = new ArcherShot(this, transform, _target,_speed, _timeInCorutine, _distanceToImpact, _damage);
                StartCoroutine(ArcherShot.CorutineShot());
                break;
            case TypeofShots.Alchemeia:
                KimiyaShot = new AlchemeiaShot(this, transform, _gravity, _lifeTime,_target,_speed,_minSpeed,_timeInCorutine,
                    _distanceToImpact, _damage, _addHeight, _addGravityUp, _splitGravity, _addGravityDown, _radius);
                StartCoroutine(KimiyaShot.CorutineShot());
                break;
            case TypeofShots.Varangians:
                VarangiansShot = new VarangiansShot(transform, _target,_speed, _timeInCorutine, _distanceToImpact, _damage, _rotationSpeed, _timeChange);
                StartCoroutine(VarangiansShot.CorutineShot());
                break;
        }

    }

    public void VaragianStats(float SpeedRotation, float TimeChange)
    {
        _rotationSpeed = SpeedRotation;
        _timeChange = TimeChange;
    }

    public void AlchemeiaStats(float AddOfHeight, float AddGravityUp,
        float SplitGravity, float AddGravityDown, Vector3 Gravity, float Radius,
        float MinSpeed)
    {
        _addGravityDown = AddGravityDown;
        _addGravityUp = AddGravityUp;
        _splitGravity = SplitGravity;
        _addHeight = AddOfHeight;
        _gravity = Gravity;
        _radius = Radius;
        _minSpeed = MinSpeed;
    }

    public void Reset()
    {
        _lifeTime = _maxLifeTime;
    }

    public static void TurnOn(Bullet b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet b)
    {
        b.gameObject.SetActive(false);
    }

}
