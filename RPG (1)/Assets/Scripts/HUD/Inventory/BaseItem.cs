using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem
{

    public enum ItemCategory {
        Weapon,
        Armour,
        Clothing, 
        Health,
        Potion,
        Nulo
    }

    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    public string Name {
        get => name; 
        set => name = value; 
    }

    public string Description {
        get => description; 
        set => description = value; 
    }

    public Sprite Icon {
        get => icon; 
        set => icon = value; 
    }
    
}
