using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryController : MonoBehaviour
{

    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEventData;
    List<RaycastResult> raycastResults;
    [SerializeField] Dictionary<string, GameObject> gameObjectItems;

    GameObject draggedItem;
    GameObject droddepItem;
    GameObject droddepItemInInventory;

    GameObject myParent;
    [SerializeField] Transform playerPos;    

    // Start is called before the first frame update
    void Start()
    {
        graphicRaycaster = GameObject.Find("HUD").GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();
        gameObjectItems = new Dictionary<string,GameObject>();

        foreach (var item in GameMaster.sharedInstance.totalItemsInGame)
        {
            gameObjectItems.Add(item.name, item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DragItems();
        DropItems();
    }

    void DragItems() {
        //Select item
        if (Input.GetMouseButtonDown(0)) {
            pointerEventData.position = Input.mousePosition;
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            if (raycastResults.Count > 0)
            {                
                foreach (var item in raycastResults)
                {
                    if (item.gameObject.GetComponent<InventoryItemUI>())
                    {
                        myParent = item.gameObject.transform.parent.gameObject;

                        draggedItem = item.gameObject;
                        draggedItem.transform.SetParent(GameMaster.sharedInstance.canvas);
                    }
                }
      
            }
        }

        //Item follows mouse
        if (draggedItem != null)
            draggedItem.GetComponent<RectTransform>().localPosition = GameMaster.sharedInstance.ScreenToCanvasPoint(Input.mousePosition);

        //End dragging
        if (Input.GetMouseButtonUp(0) && draggedItem != null) {
            pointerEventData.position = Input.mousePosition;
            raycastResults.Clear();
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            draggedItem.transform.SetParent(myParent.transform);

            if (raycastResults.Count > 0) {
                foreach (var item in raycastResults)
                {
                    if (item.gameObject.CompareTag("Slot"))
                    {                        
                        if (draggedItem.transform.position != myParent.transform.position) //Cambiamos el item de Slot
                        {
                            draggedItem.transform.SetParent(item.gameObject.transform);

                            item.gameObject.transform.GetChild(0).SetParent(myParent.transform);
                            myParent.transform.GetChild(0).position = myParent.transform.position;
                            
                        }
                        break;

                    }
                    
                    else if (item.gameObject.CompareTag("CharacterOutfit"))
                    {
                        bool reset = false;
                        // Borrar del Slot
                        if (item.gameObject.GetComponentInChildren<InventoryItemUI>().item.Name == "")
                            reset = true;

                        EquipItem(item);
                        
                        if (reset) myParent.GetComponentInChildren<InventoryItemUI>().ClearSlot();
                        break;
                    }
                }
            }

            draggedItem.transform.localPosition = Vector3.zero;
            draggedItem = null;
        }
           
        raycastResults.Clear();

    }

    void EquipItem(RaycastResult item) {

        Color c = new Color(255, 255, 255, 0); //Se queda transparente 
        foreach (Transform slot in item.gameObject.transform) //slot es el panel en la zona de outfit
        {
            if (slot.parent.name == "Potions" && myParent.transform.GetComponentInChildren<InventoryItemUI>().item.Category == BaseItem.ItemCategory.Potion)
            { //En el slot para pociones voy a meter una pocion(item.Category == Potion)
                ChangeItemSlot(slot, c, item);
            }

            else if (slot.parent.name == "Weapon" && myParent.transform.GetComponentInChildren<InventoryItemUI>().item.Category == BaseItem.ItemCategory.Weapon)
            {
                ChangeItemSlot(slot, c, item);
            }

            else if (slot.parent.name == "Shield" && myParent.transform.GetComponentInChildren<InventoryItemUI>().item.Category == BaseItem.ItemCategory.Armour)
            {
                ChangeItemSlot(slot, c, item);
            }

            else if (slot.parent.name == "Breastplate" && myParent.transform.GetComponent<InventoryItemUI>().item.Category == BaseItem.ItemCategory.Clothing)
            {
                ChangeItemSlot(slot, c, item);
            }
        }
               
    }

    private void ChangeItemSlot(Transform slotIcon, Color c, RaycastResult item) {

        draggedItem.transform.SetParent(slotIcon.parent);

        item.gameObject.transform.GetChild(0).SetParent(myParent.transform);
        item.gameObject.GetComponentInChildren<Image>().color = c;

        myParent.transform.GetChild(0).position = myParent.transform.position;
    }

    void DropItems()
    {
        //Select item
        if (Input.GetMouseButtonDown(1))
        {
            pointerEventData.position = Input.mousePosition;
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var item in raycastResults)
                {
                    if (item.gameObject.GetComponent<InventoryItemUI>() && item.gameObject.GetComponent<InventoryItemUI>().item.Category != BaseItem.ItemCategory.Nulo)
                    {
                        //droddepItem = item.gameObject;

                        droddepItem = gameObjectItems[item.gameObject.GetComponent<InventoryItemUI>().item.Name];
                        if (droddepItem != null)
                        {
                            droddepItemInInventory = item.gameObject;
                            if (droddepItemInInventory.transform.GetComponentInChildren<Text>().text != "")
                            {
                                GameMaster.stack--;
                                droddepItemInInventory.transform.GetComponentInChildren<Text>().text = GameMaster.stack.ToString();
                                if (GameMaster.stack == 0)
                                {
                                    droddepItemInInventory.GetComponent<InventoryItemUI>().ClearSlot();
                                    droddepItemInInventory.transform.GetComponentInChildren<Text>().text = "";
                                    GameMaster.stack = 1;
                                }
                                Instantiate(droddepItem, playerPos);
                            }
                            else
                            {
                                droddepItemInInventory.GetComponent<InventoryItemUI>().ClearSlot();
                                Instantiate(droddepItem, playerPos);
                            }
                        }
                            
                    }
                }

            }
        }
    }
    
}
