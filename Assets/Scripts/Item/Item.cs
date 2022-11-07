using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;   //아이템의 정보
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(data.modelPrefap, transform.position,transform.rotation,transform); //아이템의 외형 추가
    }
}
