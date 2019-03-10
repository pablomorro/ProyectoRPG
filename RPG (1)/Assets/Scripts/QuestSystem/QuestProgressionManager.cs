using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProgressionManager : MonoBehaviour
{
    private List<Quest> activeQuests;


    private void Start()
    {
        
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            

            //q.UpdateQuestProgress(0);
        }

        //if (q != null) { q.CheckForQuestCompleted(); }
    }

    public void UpdateQuest(int id)
    {
        activeQuests = GameObject.Find("QuestManager").GetComponent<QuestManager>().activeQuests;

        foreach (var activeQuest in activeQuests)
        {
            activeQuest.UpdateQuestProgress(id);
        }
    }

    public void UpdateQuest(InventoryItem item)
    {
        activeQuests = GameObject.Find("QuestManager").GetComponent<QuestManager>().activeQuests;

        foreach (var activeQuest in activeQuests)
        {
            activeQuest.UpdateQuestprogress(item);
        }
    }
}
