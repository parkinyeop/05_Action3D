using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData_EquipItem : ItemData,IEquipItem
{
    public GameObject equipPrefab;
    public virtual EquipPartType EquipPart => EquipPartType.Weapon;

    public  virtual void EquipItem(GameObject target, ItemSlot slot) 
    {
        //대상이 아이템을 장비할 수 있는지 확인
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
        if (equipTarget != null)
        {
            equipTarget.EquipItem(EquipPart, slot);
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
    public virtual bool AutoEquipItem(GameObject target, ItemSlot slot) 
    {
        bool result = false;

        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();

        if (equipTarget != null)
        {
            ItemSlot partsSlot = equipTarget.PartsSlots[(int)EquipPart];

            //현재 파츠에 장비가 있는지 확인
            //ItemData_EquipItem equipItem = equipTarget.ItemData as ItemData_EquipItem;
            if (partsSlot != null)
            {
                UnEquipItem(target);
                ItemData_EquipItem equipItem = partsSlot.ItemData as ItemData_EquipItem;
                if(equipItem != this)
                {
                    EquipItem(target, slot);
                    result = true;
                }
            }
            else
            {
                EquipItem(target, slot);
                result = true;
            }
        }
        return result;
    }
}
