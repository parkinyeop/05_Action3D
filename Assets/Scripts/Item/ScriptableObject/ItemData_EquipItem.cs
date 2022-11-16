using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData_EquipItem : ItemData,IEquipItem
{
    public GameObject equipPrefab;
    public EquipPartType EquipPart => EquipPartType.Weapon;

    public  virtual void EquipItem(GameObject target) {
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
        if (equipTarget != null)
        {
            equipTarget.EquipItem(EquipPart, this);
        }
    }
    public virtual void UnEquipItem(GameObject target) 
    {
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
        if (equipTarget != null)
        {
            equipTarget.UnEquipItem(EquipPart);
        }
    }
    public virtual void ToggleEquipItem(GameObject target) { }
}
