using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyDecorator: AbstractEnemyDecorator
{
    protected AbstractEnemyDecorator _enemies;


    public EnemyDecorator(AbstractEnemyDecorator enemies)
    {
        _enemies = enemies;
    }

}
