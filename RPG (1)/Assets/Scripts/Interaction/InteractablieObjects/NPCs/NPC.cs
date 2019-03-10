using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractableObject
{
    public int id;
    public string name;

    public int[] dialogsId;
    public int currentDialog = 0;

    //public GameObject selectionCircle;

    [HideInInspector]
    public List<Dialog> dialogList;

    // Start is called before the first frame update
    void Start()
    {
        dialogList = new List<Dialog>();
        LoadDialogs();
    }

    public void LoadDialogs() {
        foreach (var dialogId in dialogsId)
        {
            dialogList.Add(DialogManager.sharedInstance.GetDialogById(id));
        }             
    }

    /*
    public override void Highlight()
    {
        selectionCircle.SetActive(true);
    }

    public override void StopHighlight()
    {
        selectionCircle.SetActive(false);
    }*/

    public override void Interact()
    {

        //check if the previous dialog launched a quest
        //if so, check if the quest is completed and increase the current dialog index

        if (currentDialog >= 0 && dialogList[currentDialog].questId != -1)
        {
            int questId = dialogList[currentDialog].questId;
            if (QuestManager.sharedInstance.CheckQuestIsCompleted(questId)) { currentDialog++;  }
        }

        //Show dialogs
        DialogManager.sharedInstance.GetDialogById(currentDialog).npcName = name;
        DialogManager.sharedInstance.ShowDialog(dialogsId[currentDialog]);
    }

}
