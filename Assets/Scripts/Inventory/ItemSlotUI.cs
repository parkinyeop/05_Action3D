using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem;

public class ItemSlotUI : MonoBehaviour,
    IDragHandler, IBeginDragHandler, IEndDragHandler,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler,
    IPointerMoveHandler
{
    uint id;        //몇번째 슬롯인가
    protected ItemSlot itemSlot;    // UI와 연결된 아이템 슬롯

    Image itemImage;                //ItemData Image 출력
    TextMeshProUGUI itemCountText;  //아이템 갯수 출력 Text
    TextMeshProUGUI itemEquiptText;

    public uint ID => id;
    public ItemSlot ItemSlot => itemSlot;

    public Action<uint> onDragStart;
    public Action<uint> onDragEnd;
    public Action<uint> onDragCancel;
    public Action<uint> onClick;
    public Action<uint> onShiftClick;
    public Action<uint> onPointerEnter;
    public Action<uint> onPointerExit;
    public Action<Vector2> onPointerMove;
    public Action<bool> onEquipStateChange;

    //protected Player player;


    void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemCountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if (transform.childCount > 2)
        {
            Transform equip = transform.GetChild(2);
            itemEquiptText = equip.GetComponent<TextMeshProUGUI>();
        }
    }
    /// <summary>
    /// 슬롯 초기화 함수
    /// </summary>
    /// <param name="id"></param>
    /// <param name="slot"></param>
    public virtual void InitializeSlot(uint id, ItemSlot slot)
    {
        this.id = id;
        this.itemSlot = slot;
        this.itemSlot.onSlotItemChange = Refresh;
        this.itemSlot.onSlotItemEquip = SetEquiptMark;

        onDragStart = null;
        onDragEnd = null;
        onDragCancel = null;
        onPointerEnter = null;
        onPointerExit = null;
        onClick = null;
        onShiftClick = null;
        onPointerMove = null;

        Refresh();
        ClearEquiptMark();
    }

    void Refresh()
    {
        if (itemSlot.IsEmpty)
        {
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            itemCountText.text = null;
        }
        else
        {
            itemImage.sprite = ItemSlot.ItemData.itemIcon;
            itemImage.color = Color.white;
            itemCountText.text = ItemSlot.ItemCount.ToString();
        }
    }

    public void SetEquiptMark(bool isEquip)
    {
        if (isEquip)
        {

            itemEquiptText.color = Color.red;
        }
        else
        {
            itemEquiptText.color = Color.clear;
        }
    }
    public void ClearEquiptMark()
    {
        SetEquiptMark(false);
    }

    public void Resize(float iconSize)
    {
        RectTransform rectTransform = (RectTransform)itemImage.gameObject.transform;
        rectTransform.sizeDelta = new Vector2(iconSize, iconSize);
    }
    /// <summary>
    /// 다른 마우스 이벤트를 사용하기 위해 OnDrag는 반드시 있어야 한다
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log($"OnDrag : {eventData.delta}");

        // eventData.position : 마우스 포인터의 스크린좌표값
        // eventData.delta : 마우스 포인터의 위치 변화량
        // eventData.button == PointerEventData.InputButton.Left : 마우스 버튼 왼쪽이 눌려있다
        // eventData.button == PointerEventData.InputButton.Right : 마우스 버튼 오른쪽이 눌려있다
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log($"OnDragStart : {ID}");
        onDragStart?.Invoke(ID);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        if (obj != null)
        {
            ItemSlotUI endSlot = obj.GetComponent<ItemSlotUI>();
            if (endSlot != null)
            {
                onDragEnd?.Invoke(endSlot.ID);
            }
            else
            {
                onClick?.Invoke(ID);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Keyboard.current.leftShiftKey.ReadValue() > 0)
        {
            if (ItemSlot.ItemCount > 1)
            {
                onShiftClick?.Invoke(ID);
            }
        }
        else
        {
            onClick?.Invoke(ID);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke(ID);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExit?.Invoke(ID);
    }

    public virtual void OnPointerMove(PointerEventData eventData)
    {
        onPointerMove?.Invoke(eventData.position);
    }
}
