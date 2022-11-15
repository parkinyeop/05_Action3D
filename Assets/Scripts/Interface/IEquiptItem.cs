using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipItem 
{
    EquipPartType EquipPart { get; }

    public void EquipItem(GameObject target);
    public void UnEquipItem(GameObject target);
    public void ToggleEquipItem(GameObject target);

}
