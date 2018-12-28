using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{

    public InventoryItem item;

    public Transform slotIcon;
    public bool empty;

    public void ChangeColor()
    {
        Color c = new Color(255, 255, 255, 0); 
        slotIcon.GetComponent<Image>().color = c;
    }

    public void UpdateSlot() {
        empty = false;

        Color c = new Color(255, 255, 255, 255);
        slotIcon.GetComponent<Image>().color = c;

        slotIcon.GetComponent<Image>().sprite = item.Icon;
    }

    public void ClearSlot() {

    }
    
}
