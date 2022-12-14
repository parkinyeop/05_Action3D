using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("------ 아이템 데이터")]
    public uint id = 0;
    public string itemName = "아이템";
    public GameObject modelPrefap;
    public Sprite itemIcon;
    public uint value;
    public uint maxStackCount = 1;
    public string itemDescription;
}
