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
        get { return category; }
        set { category = value; }
    }

    public float Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    public void CopyInventoryItem(InventoryItem item) {
        Category = item.Category;

        Description = item.Description;
        Name = item.Name;

        Strength = item.Strength;
        Weight = item.Weight;
    }

}
