using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IBattle, IHealth
{
    ParticleSystem weaponPS;
    Transform weaponR;
    Collider weaponBlade;

    public float attackPower = 10f;
    public float defencePower = 3f;
    public float hp = 100f;
    public float maxHp = 100f;

    public float AttackPower => attackPower;

    public float DefencePower => defencePower;

    public float HP
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                if (hp < 0)
                {
                    Die();
                }
                hp = Mathf.Clamp(hp, 0.0f, maxHp);

                onHealthChange?.Invoke(hp / maxHp);
            }
        }
    }

    public float MaxHP => maxHp;

    public Action<float> onHealthChange { get; set; }
    public Action onDie { get; set; }


    private void Awake()
    {
        weaponR = GetComponentInChildren<WeaponPosition>().transform;

        weaponPS = weaponR.GetComponentInChildren<ParticleSystem>();
        weaponBlade = weaponR.GetComponentInChildren<Collider>();
    }
    private void Start()
    {
        hp = maxHp;
        weaponBlade.enabled = false;
    }
    public void WeaponEffectSwitch(bool on)
    {
        if (weaponPS != null)
        {
            if (on)
            {
                weaponPS.Play();
            }
            else
            {
                weaponPS.Stop();
            }
        }
    }
    public void WeaponBladeEnable()
    {
        if (weaponBlade != null)
        {
            weaponBlade.enabled = true;
        }
    }
    public void WeaponBladeDisable()
    {
        if (weaponBlade != null)
        {
            weaponBlade.enabled = false;
        }
    }

    public void Attack(IBattle target)
    {
        target?.Defence(AttackPower);
    }

    public void Defence(float damage)
    {
        HP -= (damage - DefencePower);
    }

    public void Die()
    {
        onDie?.Invoke();
    }


}
