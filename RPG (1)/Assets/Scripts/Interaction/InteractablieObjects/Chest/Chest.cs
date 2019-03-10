using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableObject
{
    //public GameObject selectionCircle;

    [SerializeField] private GameObject chestItems;
    public List<InventoryItemAgent> items;
    
    [SerializeField] private int chestSlots;
    private GameObject slots; 
    private GameObject[] chestSlotsUI; 

    void OnEnable()
    {
        //chestItems = GameObject.FindGameObjectWithTag("ChestItems");
        if (chestItems)
        {
            slots = chestItems.transform.GetChild(0).gameObject;
            chestSlots = items.Capacity;
            chestSlotsUI = new GameObject[chestSlots];

            for (int i = 0; i < chestSlots; i++)
            {
                chestSlotsUI[i] = slots.transform.GetChild(i).gameObject;
                chestSlotsUI[i].GetComponentInChildren<InventoryItemUI>().ClearSlot();
                chestSlotsUI[i].GetComponentInChildren<InventoryItemUI>().ChangeColor();
            }

            for (int i = 0; i < items.Capacity; i++)
            {
                chestSlotsUI[i].GetComponentInChildren<InventoryItemUI>().ChangeSlot(items[i].item); 
                chestSlotsUI[i].GetComponentInChildren<InventoryItemUI>().UpdateSlot();
            }
        }
    }

    /*
    public override void Highlight()
    {
        selectionCircle.SetActive(true);
    }

    public override void StopHighlight()
    {
        selectionCircle.SetActive(false);
    }
    */

    public override void Interact()
    {

        chestItems.SetActive(true);
    }

}
