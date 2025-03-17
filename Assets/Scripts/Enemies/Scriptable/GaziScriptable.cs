using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GaziScriptable", menuName = "ScriptableObjects/ScriptableSpecialEnemy/GaziScriptable")]
public class GaziScriptable : ScriptableObject
{
    public float DistanceOfHealt;
    public float QuantityToCure;
    public float CoolDown;
}
