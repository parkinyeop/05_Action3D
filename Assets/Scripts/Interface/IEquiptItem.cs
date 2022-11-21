using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipItem 
{
    EquipPartType EquipPart { get; }

    void EquipItem(GameObject target, ItemSlot slot);
    void UnEquipItem(GameObject target);
    bool AutoEquipItem(GameObject target, ItemSlot slot);

}
