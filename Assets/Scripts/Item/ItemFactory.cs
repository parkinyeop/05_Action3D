using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public static GameObject MakeItem(ItemIdCode code, Vector3 position)
    {
        GameObject obj = new GameObject();

        Item item = obj.AddComponent<Item>();
        item.data = GameManager.Inst.ItemData[code];

        string[] itemName = item.data.name.Split('_');
        obj.name = $"{itemName[1]}_{itemCount++}";
        obj.layer = LayerMask.NameToLayer("Item");

        obj.transform.position = position;
        obj.transform.rotation = Quaternion.identity;

        SphereCollider col = obj.AddComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = 0.5f;
        col.center = Vector3.up;
        return obj;
    }
    public static GameObject MakeItem(ItemIdCode code, Vector3 position, bool randomNoise)
    {
        GameObject obj = new GameObject();

        Item item = obj.AddComponent<Item>();
        item.data = GameManager.Inst.ItemData[code];

        string[] itemName = item.data.name.Split('_');
        obj.name = $"{itemName[1]}_{itemCount++}";
        obj.layer = LayerMask.NameToLayer("Item");

        if (randomNoise)
        {
            Vector3 randPos = new Vector3(Random.Range(0, 2), obj.transform.position.y, Random.Range(0, 2));
            obj.transform.position = position + randPos;
        }
        else
        {
            obj.transform.position = position;
        }
        obj.transform.rotation = Quaternion.identity;

        SphereCollider col = obj.AddComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = 0.5f;
        col.center = Vector3.up;

        return obj;
    }
    public static GameObject MakeItems(ItemIdCode code, int count)
    {
        for (int i = 0; i < count-1; i++)
        {
            MakeItem(code);
        }
        //GameObject[] objs = new GameObject[count];
        //Item[] items = new Item[count];
        //for (int i = 0; i <= count; i++)
        //{
        //    items[i] = objs[i].AddComponent<Item>();
        //    items[i].data = GameManager.Inst.ItemData[code];

        //    string[] itemName = items[i].data.name.Split('_');
        //    objs[i].name = $"{itemName[1]}_{itemCount++})";
        //    objs[i].layer = LayerMask.NameToLayer("Item");
        //    objs[i].transform.position = objs[i].transform.position;
        //    objs[i].transform.rotation = Quaternion.identity;

        //    SphereCollider col = objs[i].AddComponent<SphereCollider>();
        //    col.isTrigger = true;
        //    col.radius = 0.5f;
        //    col.center = Vector3.up;
        //}
        return MakeItem(code);
    }
    public static GameObject MakeItems(ItemIdCode code, Vector3 position, int count)
    {
        for(int i = 0; i < count-1; i++)
        {

        MakeItem(code, position);
        }
        return MakeItem(code, position);
    }
    public static GameObject MakeItems(ItemIdCode code, Vector3 position, int count, bool randomNoise)
    {
        for (int i = 0; i < count - 1; i++)
        {
            MakeItem(code, position, randomNoise);
        }
        return MakeItem(code, position, randomNoise);
    }
}
