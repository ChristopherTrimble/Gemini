//Author: Christopher Trimble

using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public enum QuestItemNames
{
    Rock,
    Relic
}

[Serializable]
public class QuestItem
{
    public QuestItemNames name;
    public int amount;
    public Sprite image;

    public QuestItem(QuestItemNames name, int amount, Sprite image)
    {
        this.name = name;
        this.amount = amount;
        this.image = image;
    }
}

[Serializable]
public class SerializableQuestItem
{
    public string name;
    public int amount;
    
    public SerializableQuestItem(QuestItemNames name, int amount)
    {
        this.name = name.ToString();
        this.amount = amount;
    }

    public SerializableQuestItem(QuestItem questItem)
    {
        name = questItem.name.ToString();
        amount = questItem.amount;
    }
    
    public SerializableQuestItem(string name, int amount)
    {
        this.name = name;
        this.amount = amount;
    }
}
