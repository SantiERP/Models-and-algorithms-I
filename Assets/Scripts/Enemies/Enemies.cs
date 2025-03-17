using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : SterringAgent, IForFactory
{
    float _maxLife;
    float _life;
    bool _lived;
    [SerializeField] float _damageReducer;
    public float DamageReducer { get { return _damageReducer; } }
    public float CurrentLife { get { return _life; } set { _life = value; } }
    EnemyScriptable _enemyScriptable;
    public EnemyScriptable EnemyScriptable {  get { return _enemyScriptable; } set { _enemyScriptable = value; } }

    [SerializeField] float _viewRadius;
    [SerializeField] float _offset;

    Node _priorityNode;
    Node _firstNode;
    public Node PriorityNode {  get { return _priorityNode; } set { _priorityNode = value; } }    
    public Node FistNode { get { return _firstNode; } } 

    Vector3 _goingTo;

    [Range(0f, 100f)] public float cohesionWeight = 1;
    [Range(0f, 100f)] public float separationWeight = 1;
    [Range(0f, 100f)] public float alignmentWeight = 1;

    SpriteRenderer _renderer;
    public SpriteRenderer Renderer { get { return _renderer; } set { _renderer = value; } }
    Image _image;
    public Image Image { get { return _image; }set { _image = value; } }

    EnemyDecorator _enemyDecorator;
    public EnemyDecorator EnemyDecorator { get { return _enemyDecorator; }set { _enemyDecorator = value; } }

    void Start()
    {
        GameManager.Instance.AddList(this);
        _firstNode = _priorityNode = SearchFirstNode();
        _life = _enemyScriptable.MaxLife;
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _image = GetComponentInChildren<Image>();
        _renderer.sprite = _enemyScriptable._renderer;
        _maxLife = _life;
        _lived = true;
    }

    void Update()
    {
        _goingTo = CalculateDis(transform.position, _priorityNode.transform.position);
        Move(_goingTo);
       
        if (_goingTo.magnitude <=_offset)
        {
            _priorityNode = _priorityNode.Neighbor;
        }
        _enemyDecorator?.DecoratorUpdate(transform);
    }

    void Move(Vector3 goingto)
    {
        transform.position += goingto.normalized * _enemyScriptable.Speed * Time.deltaTime;
        AddForce(Cohesion(GameManager.Instance.EnemiesInScene) * cohesionWeight + Separation(GameManager.Instance.EnemiesInScene) * separationWeight);
    }

    Node SearchFirstNode()
    {

        foreach (var item in GameManager.Instance.Nodes)
        {

            Vector3 dis = item.transform.position - transform.position;
            if (dis.magnitude <= _viewRadius)
            {
                return item;
            }
        }
        return null;
    }
    Vector3 CalculateDis(Vector3 posI, Vector3 posF)
    {
        return posF - posI;
    }
    
    public static void TurnOff(Enemies e)
    {
        e.gameObject.SetActive(false);
    }

    public static void TurnOn(Enemies e)
    {
        e.gameObject.SetActive(true);
    }

    void OnDrawGizmos()
    {
        if(_enemyScriptable.IsSpecialEnemies != SpecialEnemies.None) 
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.Gazi.DistanceOfHealt);
    }

    public void RemoveEnemyList(Enemies e)
    {
        e.transform.position = Vector3.zero;
    }

    public void ModifyLife(float mod)
    {
        _life += mod;
        _life = Mathf.Clamp(_life, 0, _maxLife);
        _image.fillAmount = Mathf.Lerp(0, 1, _life / _maxLife);
        if (_life <= 0) Die();  
    }

    void Die()
    {
        _priorityNode = FistNode;
        _image.fillAmount = 1;
        _renderer.sprite = null;
        EnemyFactory.Instance.ReturnToPool(this);
        if(_lived) GameManager.Instance.EarnPoint(_enemyScriptable.Reward);
        _lived = false;
    }

}
