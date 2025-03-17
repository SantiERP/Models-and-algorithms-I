using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    List<HordeData> _hordeS;
    Vector3 _positionToSpawn;
    Vector3 _modPos;
    float _multiplier;
    public SpecialEnemies _specialEnemies;
    bool _isPaused;
    public bool IsPaused { get { return _isPaused; } set { _isPaused = value; } }

    public EnemySpawner (Vector3 pos, List<HordeData> hordeS, float addition, Vector3 ModPos)
    {
        _positionToSpawn = pos;
        _hordeS = hordeS;
        _multiplier = addition;
        _modPos = ModPos;
    }

    void Spawn(HordeData h)
    {
        var enemy = EnemyFactory.Instance.GetObjectFromPool();
        enemy.EnemyScriptable = h.EnemyToSpawn;
        enemy.transform.position = _positionToSpawn + _modPos * Random.Range(_multiplier * -1, _multiplier);

        switch (enemy.EnemyScriptable.IsSpecialEnemies)
        {
            case SpecialEnemies.None: break;
            case SpecialEnemies.Gazi:
                Debug.Log("decorator");
                enemy.EnemyDecorator = new Gazi(enemy.EnemyDecorator);
                break;
        }
    }

    public IEnumerator SpawnInterval()
    {
        int _aux = 0;
        foreach (HordeData horde in _hordeS)
        {

            if (horde.SplitHorde > 1)
            {
                _aux = horde.EnemyCount / horde.SplitHorde;
            }
            for (int i = 0; i < horde.EnemyCount; i++)
            {
                if (Divide(i, _aux)) yield return new WaitForSeconds(horde.LongspawnInterval);
                else yield return new WaitForSeconds(horde.SpawnInterval);
                while (_isPaused) yield return null;

                Spawn(horde);
            }
            yield return new WaitForSeconds(horde.SpawnInterval);
        }
        while (_isPaused) yield return null;
        EventEntity.Instance.FinishHorde();
    }

    bool Divide(int number, int divider)
    {
        if (divider != 0) return number % divider == 0 ;
        else return false;
    }
}
