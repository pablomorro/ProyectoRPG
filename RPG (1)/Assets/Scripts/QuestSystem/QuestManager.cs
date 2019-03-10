using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager sharedInstance;

    private Dictionary<int, Quest> questDictionary;
    public List<Quest> activeQuests;
    public List<Quest> completedQuests;

    private Quest quest;
    private QuestDisplayManager displayManager;

    private const string JSONDIR = "JSON_Files/Quests";

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        activeQuests = new List<Quest>();
        completedQuests = new List<Quest>();
        questDictionary = new Dictionary<int, Quest>();

        displayManager = GameObject.Find("QuestManager").GetComponent<QuestDisplayManager>();

        LoadQuests();

        string[] sentences = { "Hola aventurero, tengo una mision para ti", " Recoge dos espadas y regresa a hablar conmigo " };
        Dialog d = new Dialog(0,"dialogo_0",sentences,0);

    }

    public void CompleteQuest(Quest q)
    {

        //Mostrar en pantalla aviso de mision completada
        displayManager.ShowNotificationPanel(q.questName, 1);

        //Reproducir audio de mision completada

        //Dar recompensa al juagdor

        //mover la quest a la lista de misiones completadas
        quest = q;
        Invoke("RemoveQuest", 0.25f);
    }

    private void RemoveQuest()
    {

        displayManager.RemoveCompletedQuest(quest);

        activeQuests.Remove(quest);
        completedQuests.Add(quest);
        quest = null;
    }


    private void LoadQuests()
    {
        Object[] jsonFileArray = Resources.LoadAll(JSONDIR, typeof(TextAsset));

        //Create the quests objects from the JSONS

        foreach (var jsonQuest in jsonFileArray)
        {
            Quest q = JsonUtility.FromJson<Quest>(jsonQuest.ToString());
            questDictionary.Add(q.id, q);
            //activeQuests.Add(q);
        }

    }


    //añade la quest a la lista de quest activas
    public void SetQuestAsActive(int id) {
        Quest q = questDictionary[id];

        displayManager.ShowNotificationPanel(q.questName, 0);

        if (!activeQuests.Contains(q)) {
            activeQuests.Add(q);
        }
    }

    public bool CheckQuestIsCompleted(int id) {
        return completedQuests.Contains(questDictionary[id]);
    }


}
