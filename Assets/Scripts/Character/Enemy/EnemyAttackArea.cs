using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    public Action<IBattle> onPlayerIn;
    public Action<IBattle> onPlayerOut;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IBattle battle = other.GetComponent<IBattle>();
            onPlayerIn?.Invoke(battle);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IBattle battle = other.GetComponent<IBattle>();
            onPlayerOut?.Invoke(battle);
        }
    }
}
