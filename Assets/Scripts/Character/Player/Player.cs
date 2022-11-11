using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Player : MonoBehaviour, IBattle, IHealth
{
    ParticleSystem weaponPS;
    Transform weaponR;
    Transform weaponL;
    Collider weaponBlade;
    Animator animator;


    [Header("-------[Player Status]")]
    public float attackPower = 10f;
    public float defencePower = 3f;
    public float hp = 100f;
    public float maxHp = 100f;

    bool isAlive = true;
    public float itemPickupRange = 2f;

    Inventory inven;

    public float AttackPower => attackPower;
    public float DefencePower => defencePower;
    public float MaxHP => maxHp;
    public bool IsAlive => isAlive;
    public float HP
    {
        get => hp;
        set
        {
            //플레이어가 살아있고 hp의 값이 바뀌었으면
            if (isAlive && hp != value)
            {
                hp = value; //현재 hp 를 갱신
                if (hp < 0)
                {
                    Die();
                }
                // Clamp 는 최소값/최대값을 설정하고 hp가 이 값을 넘지 못하도록 방지
                hp = Mathf.Clamp(hp, 0.0f, maxHp);

                onHealthChange?.Invoke(hp / maxHp);
            }
        }
    }


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
        isAlive = true;
        weaponBlade.enabled = false;
        animator.SetBool("isAlive", true);

        inven = new Inventory(this);
        GameManager.Inst.InvenUI.InitailizeInventory(inven);
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
        ShowWeaponAndSheild(true);
        Debug.Log($"Player Class {isAlive}");
    }
    public void ItemPickUp()
    {
        Collider[] items = Physics.OverlapSphere(transform.position,
            itemPickupRange, LayerMask.GetMask("Item"));

        foreach (var itemCollider in items)
        {
            Item item = itemCollider.gameObject.GetComponent<Item>();
            if (inven.AddItem(item.data))
            {
                Destroy(itemCollider.gameObject);
            }
           // inven.AddItem(item.data);
        }
    }

    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, transform.up, itemPickupRange);
    }
}
