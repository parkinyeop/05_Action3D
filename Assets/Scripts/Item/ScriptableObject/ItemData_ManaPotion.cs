using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mana Potion", menuName = "Scriptable Object/Item Data - Mana Potion", order = 3)]
public class ItemData_ManaPotion : ItemData, IUsable
{
    [Header("------ 마나포션 데이터")]
    public float mpGenPoint = 30f;
    public float genCount = 3f;

    public bool Use(GameObject target = null)
    {
        bool result = false;
        IMana mp = target.GetComponent<IMana>();
        if (mp != null)
        {
            float oldMP = mp.MP;
            mp.MP += mpGenPoint;
            //Debug.Log($"{itemName}을 사용했습니다. HP가 {healPoint}만큼 증갑니다. HP: {health.HP}->{health.HP += healPoint}");
            result = true;
        }
        return result;
    }
}
