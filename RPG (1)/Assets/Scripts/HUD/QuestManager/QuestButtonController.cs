using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestButtonController : MonoBehaviour
{

    public Quest quest;
    private GameObject questManager;
    private QuestDisplayManager questDisplayManager;

    private void Start()
    {
        questManager = GameObject.Find("QuestManager");
        questDisplayManager = questManager.GetComponent<QuestDisplayManager>();
    }

    public void OnClickEvent()
    {
        questDisplayManager.SetQuestText(quest.questName, quest.description, quest.reward.exp, quest.reward.money, quest.reward.items);
    }

}
