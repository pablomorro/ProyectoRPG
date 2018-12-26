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
        if (GameObject.FindGameObjectWithTag("InventoryUI")) {
            inventoryPanel = GameObject.FindGameObjectWithTag("InventoryUI");
            inventoryPanel.SetActive(false);
        }

        if (GameObject.FindGameObjectWithTag("SlotsHolder")) {
            slotsHolder = GameObject.FindGameObjectWithTag("SlotsHolder");
        }
    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;

            inventory = new InventorySistem();

            InventoryItem tempItem = new InventoryItem();

            tempItem.Category = BaseItem.ItemCategory.Clothing;
            tempItem.Name = "Capa mágica";
            tempItem.Description = "La peor capa del juego";
            tempItem.Strength = 0.5f;
            tempItem.Weight = 0.1f;

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

    public void RpgDestroy(GameObject item) {
        Destroy(item);
    }
}
