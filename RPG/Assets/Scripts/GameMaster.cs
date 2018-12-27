using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster sharedInstance;

    //public bool showOptions = false;
    //public float musicVolume = 0f, sfxVolume = 0f;

    public GameObject inventoryPanel;
    private int allSlots;
    private int enableSlots;
    private GameObject[] slot; //Espacios para los items

    public GameObject slotsHolder; 

    public InventorySistem inventory;
    
    private void OnEnable()
    {
        if (GameObject.FindGameObjectWithTag("SlotsHolder"))
        {
            slotsHolder = GameObject.FindGameObjectWithTag("SlotsHolder");
        }

        if (GameObject.FindGameObjectWithTag("InventoryUI")) {
            inventoryPanel = GameObject.FindGameObjectWithTag("InventoryUI");
            inventoryPanel.SetActive(false);
        }        
    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;

            inventory = new InventorySistem();

            InventoryItem tempItem = new InventoryItem
            {
                Category = BaseItem.ItemCategory.Clothing,
                Name = "Capa mágica",
                Description = "La peor capa del juego",
                Strength = 0.5f,
                Weight = 0.1f,
                Icon = null
            };

            inventory.AddItem(tempItem);
        }
        else if (sharedInstance != this) Destroy(this);

        DontDestroyOnLoad(this);

    }

    // Start is called before the first frame update
    void Start()
    {
        allSlots = 21;
        slot = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotsHolder.transform.GetChild(i).gameObject;            
            slot[i].GetComponent<InventoryItemUI>().empty = true;
            slot[i].GetComponent<InventoryItemUI>().ChangeColor();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        //if (currentScene.name != SceneName.mainMenu) //No estamos en el menu principal
        if (Input.GetKeyDown(KeyCode.I)) {
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        }
    }

    public void AddItemSlot(InventoryItem itemObject)
    {
        for (int i = 0; i < allSlots; i++)
        {   
            if (slot[i].GetComponent<InventoryItemUI>().empty)
            {
                slot[i].GetComponent<InventoryItemUI>().item.Category = itemObject.Category;

                slot[i].GetComponent<InventoryItemUI>().item.Name = itemObject.Name;
                slot[i].GetComponent<InventoryItemUI>().item.Icon = itemObject.Icon;
                
                slot[i].GetComponent<InventoryItemUI>().item.Description = itemObject.Description;                
                slot[i].GetComponent<InventoryItemUI>().item.Strength = itemObject.Strength;
                slot[i].GetComponent<InventoryItemUI>().item.Weight = itemObject.Weight;

                slot[i].GetComponent<InventoryItemUI>().UpdateSlot();

                return;
            }
        }
    }

    public void RpgDestroy(GameObject item) {
        Destroy(item);
    }
}
