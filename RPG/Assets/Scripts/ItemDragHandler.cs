using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,-5);
        
        transform.position = pos;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped in " + eventData.pointerCurrentRaycast.gameObject.name);

        //slot destino
        GameObject slot = eventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject;

        //item destino
        InventoryItemUI inventoryItemUI = slot.GetComponentInChildren<InventoryItemUI>();
        InventoryItem item = inventoryItemUI.item;

        //item que estamos moviendo
        InventoryItem itemOrigen = eventData.pointerDrag.GetComponentInParent<InventoryItemUI>().item;

        item.CopyInventoryItem(itemOrigen);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }

}
