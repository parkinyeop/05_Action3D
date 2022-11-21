using UnityEngine;
interface IEquipTarget
{
    //캐릭터의 부위와 아이템 장착을 확인하고 설정할 수 있는 프로퍼티
    ItemSlot[] PartsSlots { get;}

    /// <summary>
    /// 아이템 장비 함수
    /// </summary>
    /// <param name="part">장비위치</param>
    /// <param name="itemData">장비 아이템</param>
    void EquipItem(EquipPartType part, ItemSlot itemSlot);

    /// <summary>
    /// 아이템 장착 해제 함수
    /// </summary>
    /// <param name="parta">해제위치</param>
    void UnEquipItem(EquipPartType part);
    
}