using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SpecialEnemies
{
    None,
    Skirmisher,
    HeavyInfantry,
    Gazi
}
public class Horde : MonoBehaviour
{
    EnemySpawner _enemySpawner;
    FillAmount _fillAmount;

    [SerializeField] HordeScriptable _hordeS;
    [SerializeField] Image _imageButton;
    [SerializeField] GameObject _uiFillAmount;
    [SerializeField] Vector3 _modPosition;
    [SerializeField] float _multiplier;
    [SerializeField] float _timeToNextHorde;
    [SerializeField] float _timeYieldReturn;
    [SerializeField] int _pointReward;

    int _auxUIHorde = 0;
    int _auxHorde = 0;
    public int AuxUI { get { return _auxUIHorde; }}

    public void FirstHordeButtom(GameObject obj)
    {
        SpawnEnemiesOftheHorde();
        Destroy(obj);
    }

    public void HordeButton()
    {
        Reward();
        _fillAmount.CounterTime = 0;
        _imageButton.fillAmount = 0;
        SpawnEnemiesOftheHorde();
    }

    void Reward()
    {
        GameObject selectedObject = EventSystem.current?.currentSelectedGameObject;
        Debug.Log(selectedObject);
        if (!selectedObject) return;

        int reward = (int)(_pointReward * (1 - (_fillAmount.CounterTime / _timeToNextHorde)));
        GameManager.Instance.EarnPoint(reward);

        DesactivateScripts();
    }

    void SpawnEnemiesOftheHorde()
    {
        _uiFillAmount.SetActive(false);
        _enemySpawner = new EnemySpawner(transform.position, _hordeS.Hordes[_auxUIHorde].SubHordes, _multiplier,_modPosition);
        StartCoroutine(_enemySpawner.SpawnInterval());
        _auxUIHorde++;
        GameManager.Instance.UIHordeChange(_auxUIHorde);
    }

    void FinishSpawnHorde()
    {
        _auxHorde++;
        CurrentHorde(_auxHorde);
        if (_auxHorde == _hordeS.Hordes.Count) return;

        _uiFillAmount.SetActive(true);
        _fillAmount = new FillAmount(_timeToNextHorde, _timeYieldReturn,_imageButton);
        _fillAmount.IsPaused = false;
        StartCoroutine(_fillAmount.UINextRound());
    }

    void CurrentHorde(int CurretnHorde) 
    {
        GameManager.Instance.HordeChange(CurretnHorde);
    }

    void DesactivateScripts()
    {
        if(_fillAmount != null) _fillAmount.IsPaused = true;
    }

    void Pause()
    {
        DesactivateScripts();
        if (_enemySpawner != null) _enemySpawner.IsPaused = true;
    }
    void UnPause()
    {
        if (_fillAmount != null && _uiFillAmount.activeSelf)  _fillAmount.IsPaused = false;
        if (_enemySpawner != null) _enemySpawner.IsPaused = false;

    }

    private void OnEnable()
    {
        EventEntity.Instance.FinishHorde += FinishSpawnHorde;
        EventEntity.Instance.StartHorde += HordeButton;
        GameManager.Instance.OnLose += DesactivateScripts;
        GameManager.Instance.OnPause += Pause;
        GameManager.Instance.OnUnPause -= UnPause;
        GameManager.Instance.NumberOfHordesForLevel = _hordeS.Hordes.Count;
    }

    private void OnDisable()
    {
        EventEntity.Instance.FinishHorde -= FinishSpawnHorde;
        EventEntity.Instance.StartHorde -= HordeButton;
        GameManager.Instance.OnLose -= DesactivateScripts;
        GameManager.Instance.OnPause -= Pause;
        GameManager.Instance.OnUnPause += UnPause;
        GameManager.Instance.NumberOfHordesForLevel = 0;
    }
}
