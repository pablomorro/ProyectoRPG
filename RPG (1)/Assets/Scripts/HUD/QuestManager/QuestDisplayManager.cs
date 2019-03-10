using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDisplayManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject questPanel, questNotificationPanel;

    public TextMeshProUGUI questNameText, questDescriptionText, questRewardText;

    public GameObject buttonPrefab;
    public Transform questListPanel;

    private List<Quest> activeQuests;
    private Dictionary<int,GameObject> buttons;

    private void Start()
    {
        activeQuests = GameObject.Find("QuestManager").GetComponent<QuestManager>().activeQuests;
        buttons = new Dictionary<int, GameObject>();
    }

    private void Awake()
    {
        Invoke("InitializeQuestButtons", 0.5f);
    }

    public void ShowQuestList()
    {
        if (questPanel.activeSelf)
        {
            questPanel.SetActive(!questPanel.activeSelf);
            return;
        }

        //Obtener lista con las misiones activas
        activeQuests = GetComponent<QuestManager>().activeQuests;

        //Crear un botón por cada mision activa
        foreach (var quest in activeQuests)
        {
            if (!buttons.ContainsKey(quest.id))
            {
                //Asignar el boton a la lista 
                GameObject button = Instantiate(buttonPrefab);
                button.transform.SetParent(questListPanel);
                button.GetComponentInChildren<TextMeshProUGUI>().text = quest.questName;
                button.GetComponent<QuestButtonController>().quest = quest;

                buttons.Add(quest.id, button);
            }

        }
        //Mostrar el  panel
        questPanel.SetActive(!questPanel.activeSelf);
    }


    void InitializeQuestButtons()
    {
        //Obtener lista con las misiones activas
        activeQuests = GetComponent<QuestManager>().activeQuests;

        //Crear un botón por cada mision activa
        foreach (var quest in activeQuests)
        {
            if (!buttons.ContainsKey(quest.id))
            {
                //Asignar el boton a la lista 
                GameObject button = Instantiate(buttonPrefab);
                button.transform.SetParent(questListPanel);
                button.GetComponentInChildren<TextMeshProUGUI>().text = quest.questName;
                button.GetComponent<QuestButtonController>().quest = quest;

                buttons.Add(quest.id, button);
            }

        }
    }

    public void SetQuestText(string questName, string description, float exp, float money, InventoryItem[] items)
    {
        questNameText.text = questName;
        questDescriptionText.text = description;
        questRewardText.text = exp.ToString() + " xp";
    }

    public void RemoveQuestText()
    {
        questNameText.text = "";
        questDescriptionText.text = "";
        questRewardText.text = "";
    }

    public void RemoveCompletedQuest(Quest q)
    {
        if(buttons.ContainsKey(q.id))
        {
            GameObject b = buttons[q.id];
            Destroy(b);
            RemoveQuestText();
        }  
    }

    public void ShowNotificationPanel(string questName, int status)
    {
        questNotificationPanel.SetActive(true);
        questNotificationPanel.GetComponent<NotificationController>().ShowQuestStatus(questName, status);
    }
}
