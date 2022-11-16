using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipItem 
{
    EquipPartType EquipPart { get; }

    void EquipItem(GameObject target);
     void UnEquipItem(GameObject target);
    bool AutoEquipItem(GameObject target);

}
