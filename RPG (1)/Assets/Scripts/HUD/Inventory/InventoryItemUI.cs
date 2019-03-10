using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{

    public InventoryItem item;
    
    public bool empty;

    public void ChangeColor()
    {
        Color c = new Color(255, 255, 255, 0);
        this.GetComponent<Image>().color = c;
    }

    public void UpdateSlot() {
        
        Color c = new Color(255, 255, 255, 255);
        this.GetComponent<Image>().color = c;

        this.GetComponent<Image>().sprite = item.Icon;
    }

    public void ClearSlot() {
        empty = true;
        this.GetComponent<Image>().sprite = null;
        Color c = new Color(255, 255, 255, 0);
        this.GetComponent<Image>().color = c;

        item.Category = BaseItem.ItemCategory.Nulo;
        item.Description = null;
        item.Name = "";
        item.Icon = null;
        item.Weight = 0f;
        item.Strength = 0f;
    }

    public void ChangeSlot(InventoryItem myItem) {

        empty = false;

        item.Category = myItem.Category;
        item.Description = myItem.Description;
        item.Name = myItem.Name;
        item.Icon = myItem.Icon;
        item.Weight = myItem.Weight;
        item.Strength = myItem.Strength;
    }
    
}
