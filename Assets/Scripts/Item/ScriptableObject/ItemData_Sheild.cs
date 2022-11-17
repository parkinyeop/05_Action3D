using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sheild Item Data", menuName = "Scriptable Object/Item Data-Sheild", order = 6)]
public class ItemData_Sheild : ItemData_EquipItem
{
    [Header("------ [방패 데이터] ")]
    public float defencePower = 10f;

    public new EquipPartType EquipPart => EquipPartType.Sheild;

}
