using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(data.modelPrefap, transform.position,transform.rotation,transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
