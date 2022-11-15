using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipItem 
{

    void EquipItem(GameObject target) { }
    void UnEquipItem(GameObject target) { }
    void ToggleItem(GameObject target) { }

}
