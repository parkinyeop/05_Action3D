using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    public ItemData[] itemDatas;

    /// <summary>
    /// 인덱서(indexer) . 배열처럼 사용하는 프로퍼티 변종
    /// </summary>
    /// <param name="id">itemDatas의 인데스로 사용할 변수</param>
    /// <returns>itemDatas 의 인덱스 데이터를 리턴</returns>
    public ItemData this[uint id] => itemDatas[id];

    public ItemData this[ItemIdCode code] => itemDatas[(int)code];

    public int Length => itemDatas.Length;
}
