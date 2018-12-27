using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySistem 
{
    //Tipos de objetos
    [SerializeField] private List<InventoryItem> weapons = new List<InventoryItem>();
    [SerializeField] private List<InventoryItem> armour = new List<InventoryItem>();
    [SerializeField] private List<InventoryItem> clothing = new List<InventoryItem>();
    [SerializeField] private List<InventoryItem> health = new List<InventoryItem>();
    [SerializeField] private List<InventoryItem> potion = new List<InventoryItem>();

    private InventoryItem selectedWeapon;
    private InventoryItem selectedArmour;

    public InventoryItem SelectedWeapon {
        get { return selectedWeapon; }
        set { selectedWeapon = value; }
    }

    public InventoryItem SelectedArmour {
        get { return selectedArmour; }
        set { selectedArmour = value; }
    }

    public InventorySistem() {
        ClearInventory();
    }

    public void ClearInventory() {
        weapons.Clear();
        armour.Clear();
        clothing.Clear();
        health.Clear();
        potion.Clear();
    }

    public void AddItem(InventoryItem item) {
        switch (item.Category) {
            case BaseItem.ItemCategory.Weapon:
                weapons.Add(item);
                break;

            case BaseItem.ItemCategory.Armour:
                armour.Add(item);
                break;

            case BaseItem.ItemCategory.Clothing:
                clothing.Add(item);
                break;

            case BaseItem.ItemCategory.Health:
                health.Add(item);
                break;

            case BaseItem.ItemCategory.Potion:
                potion.Add(item);
                break;
        }
    }

    public void DeleteItem(InventoryItem item) {
        switch (item.Category) {
            case BaseItem.ItemCategory.Weapon:
                weapons.Remove(item);
                break;

            case BaseItem.ItemCategory.Armour:
                armour.Remove(item);
                break;

            case BaseItem.ItemCategory.Clothing:
                clothing.Remove(item);
                break;

            case BaseItem.ItemCategory.Health:
                health.Remove(item);
                break;

            case BaseItem.ItemCategory.Potion:
                potion.Remove(item);
                break;
        }
    }



}
