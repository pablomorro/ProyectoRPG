using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemAgent : MonoBehaviour
{
    public InventoryItem item;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")) {
            //Hacemos una copia del objeto recogido
            InventoryItem collectedItem = new InventoryItem();
            collectedItem.CopyInventoryItem(item);
            GameMaster.sharedInstance.inventory.AddItem(collectedItem);
            GameMaster.sharedInstance.AddItemSlot(collectedItem);
            GameMaster.sharedInstance.RpgDestroy(gameObject);
        }
    }
}
