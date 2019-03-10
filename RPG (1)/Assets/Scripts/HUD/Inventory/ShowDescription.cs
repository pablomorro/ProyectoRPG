using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowDescription : MonoBehaviour
{

    public GameObject itemDescription;

    private InventoryItem myItem;
    private TextMeshProUGUI myItemDescriptionText;

    // Start is called before the first frame update
    void Start()
    {
        myItemDescriptionText = itemDescription.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowItemDescription()
    {

        itemDescription.SetActive(true);

        myItem = this.transform.GetComponentInChildren<InventoryItemUI>().item;
        if (myItem.Name != "")
        {
            myItemDescriptionText.text = "Name: " + myItem.Name + "\n";
            myItemDescriptionText.text += "Description: " + myItem.Description + "\n";

        }
            
    }

    public void HideItemDescription()
    {
        itemDescription.SetActive(false);
        myItemDescriptionText.text = "";
    }
}
