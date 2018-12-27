using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem : BaseItem
{

    [SerializeField] private ItemCategory category;
    [SerializeField] private float strength;
    [SerializeField] private float weight;
    
    public ItemCategory Category
    {
        get => category; 
        set => category = value; 
    }

    public float Strength
    {
        get => strength; 
        set => strength = value; 
    }

    public float Weight
    {
        get => weight; 
        set => weight = value; 
    }

    public void CopyInventoryItem(InventoryItem item) {
        
        //Base item
        Category = item.Category;
       
        Description = item.Description;
        Name = item.Name;
        Icon = item.Icon;

        //Item en el inventario
        Strength = item.Strength;
        Weight = item.Weight;
    }

}
