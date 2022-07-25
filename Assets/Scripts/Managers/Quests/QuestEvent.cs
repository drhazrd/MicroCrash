using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Questing Entry", menuName = "Thor Quest")]
public class QuestEvent:ScriptableObject
{
    
    public enum EventStatus { Waiting, Current, Done};

    public string questEventName;
    public string questEventDescription;
    public QuestEntry[] questEntries;
    public QuestEntry currQuestEntry;
    public GameObject location;
    public int id;
    public int order = -1;
    public Quest status;

    public List<QuestPath> pathList = new List<QuestPath>();
    private int questEntryID;

    public QuestEvent(int i, string n, string d, GameObject loc)
    {
        id = i;
        questEventName = n;
        questEventDescription = d;
        //status = EventStatus.Waiting;
        location = loc;
    }
    public void UpdateQuestEvent(Quest es)
    {
        status = es;
        currQuestEntry = questEntries[questEntryID];
        questEntries[questEntryID].UpdateButton(es);
    }
    public int GetID()
    {
        return id;
    }
}
