using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipItem 
{
    EquipPartType EquipPart { get; }

    void EquipItem(GameObject target, ItemSlot slot);
    void UnEquipItem(GameObject target, ItemSlot slot);
    void AutoEquipItem(GameObject target, ItemSlot slot);

}
