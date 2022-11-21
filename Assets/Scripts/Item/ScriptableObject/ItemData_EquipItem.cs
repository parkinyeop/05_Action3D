using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData_EquipItem : ItemData, IEquipItem
{
    public GameObject equipPrefab;
    public virtual EquipPartType EquipPart => EquipPartType.Weapon;

    public virtual void EquipItem(GameObject target, ItemSlot slot)
    {
        //대상이 아이템을 장비할 수 있는지 확인
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
        if (equipTarget != null)
        {
            slot.IsEquipped = true;
            equipTarget.EquipItem(EquipPart, slot);
        }
    }
    public virtual void UnEquipItem(GameObject target, ItemSlot slot)
    {
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
        if (equipTarget != null)
        {
            slot.IsEquipped = false;
            equipTarget.UnEquipItem(EquipPart);
        }
    }
    public virtual void AutoEquipItem(GameObject target, ItemSlot slot)
    {
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();

        if (equipTarget != null)
        {
            ItemSlot partsSlot = equipTarget.PartsSlots[(int)EquipPart];

            //현재 파츠에 장비가 있는지 확인
            if (partsSlot != null)
            {
                UnEquipItem(target, partsSlot);

                ItemData_EquipItem equipItem = partsSlot.ItemData as ItemData_EquipItem;

                if (equipItem != this)
                {
                    EquipItem(target, slot);
                }
            }
            else
            {
                EquipItem(target, slot);
            }
        }

    }
}
