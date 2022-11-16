using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData_EquipItem : ItemData,IEquipItem
{
    public GameObject equipPrefab;
    public EquipPartType EquipPart => EquipPartType.Weapon;

    public  virtual void EquipItem(GameObject target) 
    {
        //대상이 아이템을 장비할 수 있는지 확인
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
    public virtual void AutoEquipItem(GameObject target) 
    {
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
        if (equipTarget != null)
        {
            //현재 파츠에 장비가 있는지 확인
            ItemData_EquipItem equipItem = equipTarget.PartsItem[(int)EquipPart];
            if (equipItem != null)
            {
                UnEquipItem(target);
                if(equipItem != this)
                {
                    EquipItem(target);
                }
            }
            else
            {
                EquipItem(target);
            }
        }
    }
}
