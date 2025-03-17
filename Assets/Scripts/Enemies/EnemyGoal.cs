using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemies>(out Enemies e))
        {
            EnemyFactory.Instance.ReturnToPool(e);
            GameManager.Instance.SpendLife();
        }
    }
}
