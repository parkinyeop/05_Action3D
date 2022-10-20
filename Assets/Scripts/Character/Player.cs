using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    ParticleSystem weaponPS;
    Transform weaponR;

    private void Awake()
    {
        weaponR = GetComponentInChildren<WeaponPosition>().transform;
        weaponPS = weaponR.GetComponent<ParticleSystem>();
    }

    public void WeaponEffectSwitch(bool on)
    {
        if(weaponPS != null)
        {
            if(on)
            {
                weaponPS.Play();
            }
            else
            {
                weaponPS.Stop();
            }
        }
    }
}
