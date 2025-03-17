using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAmount 
{
    float _timeToNextHorde;
    float _counterTime;
    float _timeYieldReturn;
    Image _buttonImage;
    bool _isPaused;
    public bool IsPaused {  get { return _isPaused; } set { _isPaused = value; } }
    public float CounterTime { get { return _counterTime; } set { _counterTime = value; } }
    
    public FillAmount( float TimetonextHorde, float Time,Image Button)
    {
        _timeToNextHorde = TimetonextHorde;
        _buttonImage = Button;
        _timeYieldReturn = Time;
    }

    public IEnumerator UINextRound()
    {
        while (true) 
        {
            while (_isPaused) yield return null;

            yield return new WaitForSeconds(_timeYieldReturn);
            _counterTime += Time.deltaTime;
            _buttonImage.fillAmount = Mathf.Lerp(0, 1, _counterTime / _timeToNextHorde);

            if (_counterTime >= _timeToNextHorde) 
            { 
                EventEntity.Instance.StartHorde(); 
                _isPaused = true;
            }
        }
    }
}
