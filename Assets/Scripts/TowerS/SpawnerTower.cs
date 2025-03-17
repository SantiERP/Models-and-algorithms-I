using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnerTower : MonoBehaviour
{
    [SerializeField] List<Transform> _spawnPos = new List<Transform>();
    [SerializeField] float _disMagnitude;
    [SerializeField] float _reductionPrice;
    [SerializeField] float _modSpawnTower;
    [SerializeField] Canvas _canvas;
    [SerializeField] Transform _gameplay;

    RectTransform _UI;
    Transform _towerTransform;
    Dictionary<Transform, bool> _zoneTower = new Dictionary<Transform, bool>();
    Dictionary<Transform, Tower> _towerinZone = new Dictionary<Transform, Tower>();
    Vector2 _localPositionUI;

    List<Tower> _towerList = new List<Tower>();

    private void Start()
    {
        foreach(Transform t in _spawnPos)
        {
            _zoneTower.Add(t, false);
            _towerinZone.Add(t, default);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray();
        }
        
    }

    void OnnUiSelect(KeyValuePair<Transform,bool> D)
    {
        if (!D.Value) { OffUI(); _UI = EventEntity.Instance.UISelectFunc(); }
        else { OffUI(); _UI = EventEntity.Instance.UIDestroyFunc(); }
        _UI.gameObject.SetActive(true);

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(D.Key.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
               _canvas.GetComponent<RectTransform>(),
               screenPosition,              
               _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera,                   
               out _localPositionUI                 
           );

        _UI.anchoredPosition = _localPositionUI;
    }

    void OffUI() 
    { 
        if (_UI) _UI.gameObject.SetActive(false);
    }

    void Ray()
    {
        Vector3 mouseWorlpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorlpos.z = 0;
           
        if (Physics2D.Raycast(Camera.main.transform.position, mouseWorlpos - Camera.main.transform.position))
        {
            foreach (KeyValuePair<Transform,bool> t in _zoneTower)
            {
                Vector3 dis = t.Key.position - mouseWorlpos;
                if(dis.magnitude < _disMagnitude )
                {
                    _towerTransform = t.Key;
                    OnnUiSelect(t);
                    KeyValuePair<GameObject, GameObject> tower = EventEntity.Instance.OffUITower.Invoke();
                    tower.Key?.SetActive(true);
                    tower.Value?.SetActive(false);
                }
            }
        }
        else if (!IsPointerOverUIElement()) OffUI();
    }

    public void DestroyTower()
    {
        GameManager manager = GameManager.Instance;
        _zoneTower[_towerTransform] = false;

        if (manager.CompleteHorde == 0) manager.EarnPoint(_towerinZone[_towerTransform].Cost);
        else manager.EarnPoint(_towerinZone[_towerTransform].Cost * _reductionPrice);

        _towerinZone[_towerTransform].gameObject.SetActive(false);
        OffUI();
    }

    public void ButtonConfirmed()
    {
        GameObject Button = EventSystem.current?.currentSelectedGameObject;
        EventEntity.Instance.AdminUITower(Button);
    }

    public void SpawnTower(Tower tower)
    {
        if (!GameManager.Instance.SpendPoints(tower.Cost)) return;

        //EventEntity.Instance.OffUITower?.Invoke().SetActive(false);
        var t = Instantiate(tower);
        t.transform.SetParent(_gameplay);
        t.transform.position = _towerTransform.position + Vector3.up * _modSpawnTower;
        _towerList.Add(t);
        OffUI();
        _towerinZone[_towerTransform] = t;
        _zoneTower[_towerTransform] = true;
    }

    private bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
