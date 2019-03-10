using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager sharedInstance;

    public Dictionary<string, Dialog> sdialogDictionaryNames;
    public Dictionary<int, Dialog> dialogDictionaryId;

    private const string JSONDIR = "JSON_Files/Dialogs";

    public GameObject dialogPanel;
    private DialogDisplayController dialogDisplayController;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    void Start()
    {
        dialogDictionaryId = new Dictionary<int, Dialog>();
        sdialogDictionaryNames = new Dictionary<string, Dialog>();

        dialogDisplayController = dialogPanel.GetComponent<DialogDisplayController>();

        Object[] jsonFileArray = Resources.LoadAll(JSONDIR, typeof(TextAsset));

       
        //Create the dialogs objects from the JSONS
        foreach (var jsonDialog in jsonFileArray)
        {
            Dialog d =  JsonUtility.FromJson<Dialog>(jsonDialog.ToString());
            dialogDictionaryId.Add(d.id, d);
            sdialogDictionaryNames.Add(d.dialogName, d);
        }
       

    }

    public Dialog GetDialogById(int id) { return dialogDictionaryId[id]; }

    public Dialog GetDialogByName(string name) { return sdialogDictionaryNames[name]; }

    public void ShowDialog(int id) {
        Dialog d = dialogDictionaryId[id];
        dialogPanel.SetActive(true);

        dialogDisplayController.DisplayText(d);
    }

    public void ShowDialog(string name)
    {
        Dialog d = sdialogDictionaryNames[name];
        dialogPanel.SetActive(true);

        
    }

    public void CloseDialog() {
        dialogPanel.SetActive(false);
        dialogDisplayController.ResetDisplayPanel();
    }
   
}
