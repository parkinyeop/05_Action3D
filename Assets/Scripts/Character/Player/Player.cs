using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IBattle, IHealth
{
    ParticleSystem weaponPS;
    Transform weaponR;
    Transform weaponL;
    Collider weaponBlade;
    Animator animator;

    public float attackPower = 10f;
    public float defencePower = 3f;
    public float hp = 100f;
    public float maxHp = 100f;
    bool isAlive = true;


    public float AttackPower => attackPower;

    public float DefencePower => defencePower;
    

    public float HP
    {
        get => hp;
        set
        {
            if (isAlive && hp != value)
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
    public bool IsAlive => isAlive;
    /// <summary>
    /// 델리게이트
    /// </summary>
    public Action<float> onHealthChange { get; set; }
    public Action onDie { get; set; }



    private void Awake()
    {
        weaponR = GetComponentInChildren<WeaponPosition>().transform;
        weaponL = GetComponentInChildren<ShieldPosition>().transform;

        weaponPS = weaponR.GetComponentInChildren<ParticleSystem>();
        weaponBlade = weaponR.GetComponentInChildren<Collider>();

        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        hp = maxHp;
        weaponBlade.enabled = false;
        animator.SetBool("isAlive", true);
        isAlive = true;
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

    public void ShowWeaponAndSheild(bool isShow)
    {
        weaponR.gameObject.SetActive(isShow);
        weaponL.gameObject.SetActive(isShow);
    }

    public void Attack(IBattle target)
    {
        target?.Defence(AttackPower);
    }

    public void Defence(float damage)
    {
        if (isAlive)
        {
            animator.SetTrigger("Hit");
            HP -= (damage - DefencePower);
        }
    }

    public void Die()
    {
        isAlive = false;
        animator.SetLayerWeight(1, 0);
        animator.SetBool("isAlive", IsAlive);
        onDie?.Invoke();
        Debug.Log($"Player Class {isAlive}");
    }


}
