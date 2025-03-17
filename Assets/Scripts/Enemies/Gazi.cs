using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gazi : EnemyDecorator
{
    GaziScriptable _gazi;
    float _countCoolDown;

    public Gazi(AbstractEnemyDecorator enemies ) : base(enemies)
    {
        _gazi = GameManager.Instance.Gazi;
    }

    public override void DecoratorUpdate(Transform MyTransform)
    {
        if (_countCoolDown >= _gazi.CoolDown)
        {
            foreach (Enemies item in GameManager.Instance.EnemiesInScene)
            {
                Vector3 dis = item.transform.position - MyTransform.position;
                if (dis.magnitude < _gazi.DistanceOfHealt)
                {
                    //item.GetLife(_gazi.QuantityToCure);
                    item.ModifyLife(_gazi.QuantityToCure);
                    Debug.Log("curando:" + _gazi.QuantityToCure);
                }
            }
            _countCoolDown = 0;
        }
        _countCoolDown += Time.deltaTime;
    }
}
