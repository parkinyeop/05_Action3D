using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템을 생성만하는 클래스, 팩토리 디자인 패턴
/// </summary>
public class ItemFactory
{
    static int itemCount = 0;   //생성된 아이템의 총수 , 아이디 역할도 함

    public static GameObject MakeItem(uint id)
    {
        return MakeItem((ItemIdCode)id);
    }
   public static GameObject MakeItem(ItemIdCode code)
    {
        GameObject obj = new GameObject();

        Item item = obj.AddComponent<Item>();
        item.data = GameManager.Inst.ItemData[code];

        string[] itemName = item.data.name.Split("_");  //00_Ruby
        obj.name = $"{itemName[1]}_{itemCount++}";
        obj.layer = LayerMask.NameToLayer("Item");

        SphereCollider col = obj.AddComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = 0.5f;
        col.center = Vector3.up;

        return obj;
    }
}
