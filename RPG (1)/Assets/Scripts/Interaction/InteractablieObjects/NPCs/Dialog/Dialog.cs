using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    public int id;
    public string npcName;
    public string dialogName;
    public string[] sentences;
    public int questId;

    public Dialog(int id, string dialogName, string[] sentences, int questId)
    {
        this.id = id;
        this.dialogName = dialogName;
        this.sentences = sentences;
        this.questId = questId;
      
    }
}
