using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_manager : MonoBehaviour
{
    public TextMeshProUGUI Coins;
    public TextMeshProUGUI Life;
    public TextMeshProUGUI Waves;
    int _numMaxHorde;
    float _archerTowerCost;
    float _varangianCost;
    float _alchemiaCost;

    [SerializeField] ArcherTower _archer;
    [SerializeField] VarangiansTower _varangian;
    [SerializeField] AlchemeiaTower _alchemeia;

    [SerializeField] GameObject _panelArcher;
    [SerializeField] GameObject _panelVaragians;
    [SerializeField] GameObject _panelAlchemia;
        
    [SerializeField] RectTransform UIselectTower;
    [SerializeField] RectTransform UIdestroyTower;

    [SerializeField] GameObject _towerFromConfirmArcher;
    [SerializeField] GameObject _towerFromConfirmAlchemia;
    [SerializeField] GameObject _towerFromConfirmVarangians;

    [SerializeField] GameObject _archerUI;
    [SerializeField] GameObject _alchemiaUI;
    [SerializeField] GameObject _varangiansUI;

    KeyValuePair<GameObject, GameObject> _returnUITower;
    Dictionary<GameObject, float> _towerCost = new Dictionary<GameObject, float>();   
    Dictionary<GameObject, GameObject> _uiSpawnTower = new Dictionary<GameObject, GameObject>();

    private void Start()
    {
        _numMaxHorde = GameManager.Instance.NumberOfHordesForLevel;
        _archerTowerCost = _archer.Cost;
        _varangianCost = _varangian.Cost;
        _alchemiaCost = _alchemeia.Cost;  
        _towerCost.Add(_panelArcher, _archerTowerCost);
        _towerCost.Add(_panelVaragians, _varangianCost);
        _towerCost.Add(_panelAlchemia, _alchemiaCost);
        _uiSpawnTower.Add(_archerUI, _towerFromConfirmArcher);
        _uiSpawnTower.Add(_alchemiaUI, _towerFromConfirmAlchemia);
        _uiSpawnTower.Add(_varangiansUI, _towerFromConfirmVarangians);
    }

    void UpdateWaves(int w)
    {
        Waves.text = w.ToString() + "/" +_numMaxHorde.ToString() + " Hordes";
    }

    void UpdateScoreUI(float newPoint)
    {
        Coins.text = newPoint.ToString();
    }

    void UpdteLifeUI(int newLife)
    {
        Life.text = newLife.ToString();
    }

    RectTransform SelectTower()
    {
        return UIselectTower;
    }

    RectTransform DestroyTower()
    {
        return UIdestroyTower;
    }

    void UISpawnTower(GameObject ButtonON)
    {
        foreach (KeyValuePair<GameObject, GameObject> pair in _uiSpawnTower)
        {
            if (pair.Key == ButtonON)
            {
                pair.Value.SetActive(true);
                pair.Key.SetActive(false);
                _returnUITower = pair;
            }
            else 
            {
                pair.Value.SetActive(false); 
                pair.Key.SetActive(true);
            }
        }
    }
    KeyValuePair<GameObject, GameObject> OffUI()
    {
        return _returnUITower;
    }
    void CheckCost(float t)
    {
        foreach (KeyValuePair<GameObject, float> pair in _towerCost)
        {
            if (t < pair.Value)
            {
                pair.Key.SetActive(true);
            } else if (pair.Key.activeSelf) pair.Key.SetActive(false);
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnScoreChanged += UpdateScoreUI;
        GameManager.Instance.OnScoreChanged += CheckCost;
        GameManager.Instance.OnLifeChanged += UpdteLifeUI;
        GameManager.Instance.OnUIHordeChanged += UpdateWaves;
        EventEntity.Instance.UISelectFunc += SelectTower;
        EventEntity.Instance.UIDestroyFunc += DestroyTower;
        EventEntity.Instance.AdminUITower += UISpawnTower;
        EventEntity.Instance.OffUITower += OffUI;
    }

    private void OnDisable()
    {
        EventEntity.Instance.UISelectFunc -= SelectTower;
        EventEntity.Instance.UIDestroyFunc -= DestroyTower;
        EventEntity.Instance.AdminUITower -= UISpawnTower;
        EventEntity.Instance.OffUITower -= OffUI;
        GameManager.Instance.OnScoreChanged -= UpdateScoreUI;
        GameManager.Instance.OnScoreChanged -= CheckCost;
        GameManager.Instance.OnLifeChanged -= UpdteLifeUI;
        GameManager.Instance.OnUIHordeChanged -= UpdateWaves;
    }

    public void BTN_Options()
    {
        GameManager.Instance.Pause();
        ScreenManager.Instance.Push("Canvas OptionsLevel");
    }

}
