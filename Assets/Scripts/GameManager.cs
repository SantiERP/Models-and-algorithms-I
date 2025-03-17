using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    List<SterringAgent> _enemiesInScene = new List<SterringAgent>();
    public List<SterringAgent> EnemiesInScene => _enemiesInScene;
    public List<Node> Nodes = new List<Node>();

    [SerializeField] float PlayerPoints;
    public int PlayerLife;

    int _completeHorde;
    public int CompleteHorde { get { return _completeHorde; } }
    int _numberOfHordesForLevel;
    public int NumberOfHordesForLevel { get {  return _numberOfHordesForLevel; } 
        set { _numberOfHordesForLevel = value; } }

    public GaziScriptable Gazi;
    private void Awake()
    {
        if (Instance != null & Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (_enemiesInScene.All(e => !e.gameObject.activeSelf) &&
            _completeHorde == _numberOfHordesForLevel)
        {
            YouWin();
            OnWin?.Invoke();
            Destroy(this);
        }
    }

    public Action<float> OnScoreChanged;
    public Action<int> OnLifeChanged;
    public Action<int> OnUIHordeChanged;
    public Action<int> OnHordeChanged;
    public Action OnWin;
    public Action OnLose;
    public Action OnPause;
    public Action OnUnPause;
    //public Action<Enemies> OnRemoveList;

    public void RemoveList(Enemies e)
    {
        _enemiesInScene.Remove(e);
        //OnRemoveList?.Invoke(e);
    }

    public void AddList(Enemies e)
    {
        _enemiesInScene.Add(e);
    }

    public void UIHordeChange(int horde)
    {
        OnUIHordeChanged?.Invoke(horde);
    }

    public void HordeChange(int current)
    {
        _completeHorde = current;
        OnHordeChanged?.Invoke(_completeHorde);
    }

    public void EarnPoint(float points)
    {
        PlayerPoints += points;

        OnScoreChanged?.Invoke(PlayerPoints);
    }

    public bool SpendPoints(float cost)
    {
        if (PlayerPoints >= cost)
        {
            PlayerPoints -= cost;
            OnScoreChanged?.Invoke(PlayerPoints);
            return true;
        }else return false;
    }

    public bool LastWave()
    {
        return _completeHorde == _numberOfHordesForLevel;
    }

    public int NumberOfHordes(int number) { return number; }

    public bool SpendLife() 
    {
        if (PlayerLife >= 1)
        {
            PlayerLife -= 1;
            OnLifeChanged?.Invoke(PlayerLife);
            if (PlayerLife == 0) { YouLose(); }
        }
        return default;
    }

    void YouLose()
    {
        OnPause?.Invoke();
        ScreenManager.Instance.Push("Canvas Lose");
    }

    public void Pause()
    {
        OnPause?.Invoke();
    }

    public void UnPause()
    {
        OnUnPause?.Invoke();
    }

    void YouWin()
    {
        ScreenManager.Instance.Push("Canvas Win");
    }
}
