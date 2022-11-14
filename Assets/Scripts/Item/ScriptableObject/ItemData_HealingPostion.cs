using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Potion", menuName = "Scriptable Object/Item Data - Healing Potion", order = 2)]
public class ItemData_HealingPostion : ItemData, IUsable
{
    [Header("------ 힐링포션 데이터")]
    public float healPoint = 20f;

    public bool Use(GameObject target = null)
    {
        bool result = false;
        IHealth health = target.GetComponent<IHealth>();
        if (health != null)
        {
            float oldHP = health.HP;
            health.HP += healPoint;
            //Debug.Log($"{itemName}을 사용했습니다. HP가 {healPoint}만큼 증갑니다. HP: {health.HP}->{health.HP += healPoint}");
            result = true;
        }
        return result;
    }
}
