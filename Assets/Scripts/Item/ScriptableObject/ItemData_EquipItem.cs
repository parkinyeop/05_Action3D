using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData_EquipItem : ItemData, IEquipItem
{
    public GameObject equipPrefab;
    EquipPartType equipPartType;
    public EquipPartType EquipPart => equipPartType;

    public virtual void EquipItem(GameObject target)
    {
        //대상이 아이템을 장비할 수 있는지 확인
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
        if (equipTarget != null)
        {
            if (equipPartType == EquipPartType.Weapon)
            {
                //equipPartType = EquipPartType.Weapon;
                equipPartType = EquipPartType.Sheild;
            }
            else
            {
                equipPartType = EquipPartType.Sheild;
                //equipPartType = EquipPartType.Weapon;
            }
            Debug.Log(equipPartType.ToString());
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
    public virtual bool AutoEquipItem(GameObject target)
    {
        bool result = false;
        IEquipTarget equipTarget = target.GetComponent<IEquipTarget>();
        if (equipTarget != null)
        {
            //현재 파츠에 장비가 있는지 확인
            ItemData_EquipItem equipItem = equipTarget.PartsItem[(int)EquipPart];
            if (equipItem != null)
            {
                UnEquipItem(target);
                if (equipItem != this)
                {
                    EquipItem(target);
                    result = true;
                }
            }
            else
            {
                EquipItem(target);
                result = true;
            }
        }
        return result;
    }
}
