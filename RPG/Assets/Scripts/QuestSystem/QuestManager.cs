using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public List<Quest> activeQuests;
    public List<Quest> completedQuests;

    private Quest quest;
    private QuestDisplayManager displayManager;

    private const string JSONDIR = "JSON_Files";

    // Start is called before the first frame update
    void Start()
    {
        activeQuests = new List<Quest>();
        completedQuests = new List<Quest>();

        displayManager = GameObject.Find("QuestManager").GetComponent<QuestDisplayManager>();

        LoadQuests();
    }

    public void CompleteQuest(Quest q)
    {

        //Mostrar en pantalla aviso de mision completada
        Debug.Log("Quest completed: " + q.questName);

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
    }


    private void LoadQuests()
    {
        Quest q = JsonUtility.FromJson<Quest>(Resources.Load<TextAsset>(JSONDIR + "/Template").text);
        activeQuests.Add(q);
    }

}
