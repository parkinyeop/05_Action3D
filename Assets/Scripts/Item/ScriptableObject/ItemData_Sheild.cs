using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sheild Item Data", menuName = "Scriptable Object/Item Data-Sheild", order = 6)]
public class ItemData_Sheild : ItemData_EquipItem
{
    //public GameObject equipPrefab;
    //[Header("------ [무기 데이터] ")]
    //public float attackPower = 30f;

    public new EquipPartType EquipPart => EquipPartType.Sheild;

}
