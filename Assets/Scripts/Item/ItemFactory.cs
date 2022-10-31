using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//함수의 구성요소 : 이름 파라미터 리턴값 바디
// overloading : 이름이 같고 파라미터가 다른 함수를 만드는 것
// overriding ; 이름, 파라미터, 리턴값이 같은 함수를 만드는 것

/// <summary>
/// 아이템을 생성만하는 클래스, 팩토리 디자인 패턴
/// </summary>
public class ItemFactory
{
    static int itemCount = 0;   //생성된 아이템의 총수 , 아이디 역할도 함

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
    public static GameObject MakeItem(ItemIdCode code, Vector3 position, bool randomNoise = false)
    {
        GameObject obj = MakeItem(code);
        if (randomNoise)
        {
            Vector2 noise = Random.insideUnitCircle * 0.5f;
            position.x = noise.x;
            position.z = noise.y;
        }
        obj.transform.position = position;
        return obj;
    }
    public static GameObject[] MakeItem(ItemIdCode code, int count)
    {
        GameObject[] objs = new GameObject[count];
        for(int i = 0; i < count; i++)
        {
            objs[i] = MakeItem(code);
        }
        return objs;
    }
    public static GameObject[] MakeItem(ItemIdCode code, int count, Vector3 position, bool randomNoise = false)
    {
        GameObject[] objs = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            objs[i] = MakeItem(code, position, randomNoise);
        }
        return objs;
    }

    public static GameObject MakeItem(int id)
    {
        if(id < 0)
        {
            return null;
        }
        return MakeItem((ItemIdCode)id);
    }
    public static GameObject MakeItem(int id, Vector3 position, bool randomNoise = false)
    {
        GameObject obj = MakeItem((ItemIdCode)id);
        if (randomNoise)
        {
            Vector2 noise = Random.insideUnitCircle * 0.5f;
            position.x = noise.x;
            position.z = noise.y;
        }
        obj.transform.position = position;
        return obj;
    }
    public static GameObject[] MakeItem(int id, int count)
    {
        GameObject[] objs = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            objs[i] = MakeItem(id);
        }
        return objs;
    }
    public static GameObject[] MakeItem(int id, int count, Vector3 position, bool randomNoise = false)
    {
        GameObject[] objs = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            objs[i] = MakeItem(id, position, randomNoise);
        }
        return objs;
    }
}
