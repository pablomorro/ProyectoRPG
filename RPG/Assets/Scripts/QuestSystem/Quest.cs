using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    public int id;
    public string description; 
    public string questName; 
    public int recipient; //id del npc que nos manda la quest
    public int requiredLevel;
    public Reward reward;
    public Task task;

    [Serializable]
    public class Reward
    {
        public float exp;
        public float money;
        public InventoryItem[] items;

        public Reward(float exp, float money, InventoryItem[] items)
        {
            this.exp = exp;
            this.money = money;
            this.items = items;
        }
    }

    [Serializable]
    public class Task
    {
        public int[] talkTo; //id of the npcs ww have to talk with to complete te quest
        public QuestItem[] items; //list with the items we must gather to complete the quest
        public QuestKill[] kills; //list with the amount of enemies we must kill to complete the quest
        public QuestPosition[] questPositions; //list with the positions we must reach to complete the quest

        public Task(int[] talkTo, QuestItem[] items, QuestKill[] kills, QuestPosition[] questPositions)
        {
            this.talkTo = talkTo;
            this.items = items;
            this.kills = kills;
            this.questPositions = questPositions;
        }
    }

    [Serializable]
    public class QuestPosition
    {
        public bool visited;
        public Transform position;

        public QuestPosition(bool visited, Transform position)
        {
            this.visited = visited;
            this.position = position;
        }
    }

    [Serializable]
    public class QuestItem
    {
        public int reqAmount; //cantidad que tenemos que recoger
        public int gatheredQuantity = 0;
        //###########CAMBIAR ESTO###########
        public int id = 0; //id del item que tenemos que recoger
        //###########CAMBIAR ESTO###########
        public InventoryItem item;

        public QuestItem(int reqAmount, int id,InventoryItem item)
        {
            this.reqAmount = reqAmount;
            this.item = item;
            this.id = id;
        }
    }

    [Serializable]
    public class QuestKill
    {
        public int id;
        public int amount;
        public int playerCurrent;

        public QuestKill(int id, int amount)
        {
            this.id = id;
            this.amount = amount;
            this.playerCurrent = 0;
        }
    }

    public Quest()
    {
       
        questName = "primera tarea";
        description = "recoge dos espadas";
        //Reward
        reward = new Reward(400,100,null);
        //Task
        QuestItem[] items = new QuestItem[1];
        items[0] = new QuestItem(2, 0, null);
        QuestKill[] questKills = new QuestKill[1];
        questKills[0] = new QuestKill(0,10);
        task = new Task(null, items, null, null);


    }

    //public void UpdateQuestProgress(NPC npc) { }

    public void UpdateQuestProgress(int enemyId)
    {
        //comrpobamos que en la mision hay que matar enemigos
        if (task.kills != null)
        {
            //para cada enemigo que hay que matar, comprobamos si el id del enemigo que hemos matado se corresponde con su id
            foreach (var item in task.kills)
            {
                if (item.id == enemyId)
                {
                    //aumentamos la cantidad de enemigos que hemos matado.
                    item.playerCurrent += 1;
                    CheckForQuestCompleted();
                }
            }
        }
    }

    public void UpdateQuestprogress(Transform position) {
        //comrpobamos que en la mision hay que visitar posiciones
        if (task.questPositions != null)
        {
            //para cada posicion que hay que visitar comprobamos si ya lo hemos hecho
            foreach (var item in task.questPositions)
            {
                if (!item.visited && item.position.Equals(position))
                {
                    //aumentamos la cantidad de enemigos que hemos matado.
                    item.visited = true;
                    CheckForQuestCompleted();
                }
            }
        }
    }

    public void UpdateQuestprogress(InventoryItem item) {
        //comrpobamos que en la mision hay que recoger items
        if (task.items != null)
        {
            //para cada que hay que coger comprobamos que si es el que hemos recogido
            foreach (var i in task.items)
            {
                if (i.gatheredQuantity != i.reqAmount && i.id == item.id)
                {
                    //aumentamos la cantidad de enemigos que hemos matado.
                    i.gatheredQuantity++;
                    CheckForQuestCompleted();
                }
            }
        }
    }


    public void CheckForQuestCompleted()
    {

        if (CheckKillTasksCompleted() && CheckPositionTasksCompleted() && CheckItemTasksCompleted())
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        GameObject.Find("QuestManager").GetComponent<QuestManager>().CompleteQuest(this);
    }

    private bool CheckKillTasksCompleted()
    {
        var completed = true;

        if (task.kills != null)
        {
            foreach (var item in task.kills)
            {
                if (item.amount != item.playerCurrent)
                {
                    return false;
                }
            }
        }

        return completed;
    }

    private bool CheckPositionTasksCompleted()
    {
        var completed = true;

        if (task.questPositions != null)
        {
            foreach (var item in task.questPositions)
            {
                if (!item.visited)
                {
                    return false;
                }
            }
        }

        return completed;
    }

    private bool CheckItemTasksCompleted()
    {
        var completed = true;

        if (task.items != null)
        {
            foreach (var item in task.items)
            {
                if (item.reqAmount != item.gatheredQuantity)
                {
                    return false;
                }
            }
        }

        return completed;
    }

}
