using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mana Potion", menuName = "Scriptable Object/Item Data - Mana Potion", order = 3)]
public class ItemData_ManaPostion : ItemData, IUsable
{
    [Header("------[마나포션 데이터]")]
    public float totlaRegenPoint = 30f;
    public float duration = 3f;

    public bool Use(GameObject target = null)
    {
        bool result = false;
        IMana mana = target.GetComponent<IMana>();
        if (mana != null)
        {
            mana.ManaRegenerate(totlaRegenPoint, duration);
           
            result = true;
        }
        return result;
    }
}
