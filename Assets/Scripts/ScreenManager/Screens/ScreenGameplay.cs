using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGameplay : IScreen
{
    Dictionary<Behaviour, bool> _beforeActivation;

    Transform _root;

    public ScreenGameplay(Transform root)
    {
        _root = root;

        _beforeActivation = new Dictionary<Behaviour, bool>();

        foreach (Transform child in root)
        {
        }
    }

    public void Activate()
    {
        foreach (var pair in _beforeActivation)
        {
            pair.Key.enabled = pair.Value;
        }
    }

    public void Deactivate()
    {
        foreach (var behaviour in _root.GetComponentsInChildren<Behaviour>())
        {
            _beforeActivation[behaviour] = behaviour.enabled;

            behaviour.enabled = false;

        }
    }

    public void Free()
    {
        GameObject.Destroy(_root.gameObject);
    }
}
