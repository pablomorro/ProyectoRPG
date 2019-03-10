using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster sharedInstance;
    public static System.Random rnd;

    //public bool showOptions = false;
    //public float musicVolume = 0f, sfxVolume = 0f;

    //Inventory
    public GameObject inventoryPanel;
    private int allSlots;
    public static int stack = 1;
    private GameObject[] slot; //Espacios para los items
    [SerializeField] public List<GameObject> totalItemsInGame;

    public GameObject slotsHolder;

    public AudioManager audioManager;

    public InventorySistem inventory;

    [HideInInspector]
    public QuestProgressionManager questProgressionManager;
    public GameObject mapPanel;
    public Transform canvas;

    private void OnEnable()
    {
        if (GameObject.FindGameObjectWithTag("SlotsHolder"))
        {
            slotsHolder = GameObject.FindGameObjectWithTag("SlotsHolder");
            canvas = GameObject.Find("HUD").transform;
        }

        if (GameObject.FindGameObjectWithTag("InventoryUI")) {
            inventoryPanel = GameObject.FindGameObjectWithTag("InventoryUI");
            inventoryPanel.SetActive(false);
        }

        questProgressionManager = GetComponent<QuestProgressionManager>();

        rnd = new System.Random();
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
            slot[i].GetComponentInChildren<InventoryItemUI>().ClearSlot();
            slot[i].GetComponentInChildren<InventoryItemUI>().ChangeColor();
        }


        //Start playing the main theme
        audioManager.Play("MainTheme");

    }

    // Update is called once per frame
    void Update()
    {
        //if (currentScene.name != SceneName.mainMenu) //No estamos en el menu principal
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //abrir la lista de misiones
            GameObject.Find("QuestManager").GetComponent<QuestDisplayManager>().ShowQuestList();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            //abrir el mapa
           mapPanel.GetComponent<MapController>().ShowMap();
        }
    }

    public void AddItemSlot(InventoryItem itemObject)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (itemObject.Category == BaseItem.ItemCategory.Potion)
                for (int j = i; j < allSlots; j++) {
                    if (slot[j].GetComponentInChildren<InventoryItemUI>().item.Category == BaseItem.ItemCategory.Potion)
                    {
                        stack++;
                        slot[j].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = stack.ToString();
                        return;
                    }
                    else {
                        slot[i].GetComponentInChildren<InventoryItemUI>().ChangeSlot(itemObject);

                        slot[i].GetComponentInChildren<InventoryItemUI>().UpdateSlot();
                        
                        return;
                    }
                }

            else if (slot[i].GetComponentInChildren<InventoryItemUI>().empty)
            {
                slot[i].GetComponentInChildren<InventoryItemUI>().ChangeSlot(itemObject);

                slot[i].GetComponentInChildren<InventoryItemUI>().UpdateSlot();



                return;
            }
        }
    }

    public void RpgDestroy(GameObject item) {
        Destroy(item);
    }

    public Vector2 ScreenToCanvasPoint(Vector2 screenPosition) {
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(screenPosition);

        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return (new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y) - (canvasSize / 2));
    }
}
