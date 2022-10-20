using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Player player;

    private void Start()
    {
        //player = GameManager.Inst.Player;
        player = GetComponentInParent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IBattle target = other.GetComponent<IBattle>();

            if(target != null)
            {
                player.Attack(target);
            }
        }
    }
}
