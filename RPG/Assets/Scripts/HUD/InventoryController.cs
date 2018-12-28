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
    InventoryItemUI currentItem;

    GameObject draggedItem;

    // Start is called before the first frame update
    void Start()
    {
        graphicRaycaster = GameObject.Find("HUD").GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();
    }

    // Update is called once per frame
    void Update()
    {
        DragItems();
    }

    void DragItems() {
        if (Input.GetMouseButtonDown(0)) {
            pointerEventData.position = Input.mousePosition;
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            if (raycastResults.Count > 0)
            {                
                foreach (var item in raycastResults)
                {
                    if (item.gameObject.GetComponent<InventoryItemUI>())
                    {

                        currentItem.item.Category = item.gameObject.GetComponent<InventoryItemUI>().item.Category;

                        currentItem.item.Name = item.gameObject.GetComponent<InventoryItemUI>().item.Name;
                        currentItem.item.Icon = item.gameObject.GetComponent<InventoryItemUI>().item.Icon;

                        currentItem.item.Description = item.gameObject.GetComponent<InventoryItemUI>().item.Description;
                        currentItem.item.Strength = item.gameObject.GetComponent<InventoryItemUI>().item.Strength;
                        currentItem.item.Weight = item.gameObject.GetComponent<InventoryItemUI>().item.Weight;

                        currentItem.slotIcon = item.gameObject.GetComponent<InventoryItemUI>().slotIcon;

                        item.gameObject.GetComponent<InventoryItemUI>().empty = true;

                        draggedItem.transform.SetParent(GameMaster.sharedInstance.canvas);

                    }
                }
      
            }
        }

        //Item follows mouse
        if (draggedItem != null)
            draggedItem.GetComponent<RectTransform>().localPosition = GameMaster.sharedInstance.ScreenToCanvasPoint(Input.mousePosition);

        //End dragging
        if (Input.GetMouseButtonUp(0)) {
            pointerEventData.position = Input.mousePosition;
            raycastResults.Clear();
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            if (raycastResults.Count > 0) {
                foreach (var item in raycastResults)
                {
                    if (item.gameObject.CompareTag("Slot"))
                    {
                        draggedItem.transform.SetParent(item.gameObject.transform);
                        draggedItem.transform.localPosition = Vector3.zero;
                        break;

                    }
                }
            }

            draggedItem = null;
        }
           
        raycastResults.Clear();

    }
}
