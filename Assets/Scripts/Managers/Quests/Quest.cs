using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Quest", menuName = "Thor new Quests")]

public class Quest : ScriptableObject
{
    [System.Serializable]
    public struct Info
    {
        public string Name;
        public Sprite Icon;
        public string Description;

    }
    [Header("Info")] public Info Information;

    [System.Serializable]
    public struct Stat
    {
        public int Currency;
        public int XP;
    }
    [Header("Reward")] public Stat Reward = new Stat { Currency = 10, XP = 10 };
    public bool Completed { get; private set; }
    public QuestCompletedEvent QuestCompleted;


    public abstract class QuestGoal : ScriptableObject
    {
        protected string Description;
        public int CurrentAmount { get; protected set; }
        public int RequiredAmount = 1;
        public bool Completed { get; protected set; }
        [HideInInspector] public UnityEvent GoalCompleted;

        public virtual string GetDescription()
        {
            return Description;
        }
        public virtual void Initialize()
        {
            Completed = false;
            GoalCompleted = new UnityEvent();
        }
        protected void Evaluate()
        {
            if (CurrentAmount >= RequiredAmount)
            {
                Complete();
            }
        }
        private void Complete()
        {
            Completed = true;
            GoalCompleted.Invoke();
            GoalCompleted.RemoveAllListeners();
        }
        public void Skip()
        {
            Complete();
        }
    }

    public List<QuestGoal> Goals;
    public void Initilize()
    {
        Completed = false;
        QuestCompleted = new QuestCompletedEvent();

        foreach(var goal in Goals)
        {
            goal.Initialize();
            goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    private void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
        if (Completed)
        {
            QuestCompleted.Invoke(this);
            QuestCompleted.RemoveAllListeners();
        }
    }
}
public class QuestCompletedEvent : UnityEvent<Quest> { }
/*
    public List<QuestEvent> questEvents = new List<QuestEvent>();
    List<QuestEvent> pathList = new List<QuestEvent>();
    public Quest() { }
    public QuestEvent AddQuestEvent(int id, string n, string d, GameObject l)
    {
        QuestEvent questEvent = new QuestEvent(id, n, d, l);
        questEvents.Add(questEvent);
        return questEvent;
    }

    public void AddPath(int fromQuestEvent, int toQuestEvent)
    {
        QuestEvent from = FindQuestEvent(fromQuestEvent);
        QuestEvent to = FindQuestEvent(toQuestEvent);

        if(from!=null & to != null)
        {
            QuestPath p = new QuestPath(from, to);
            from.pathList.Add(p);
        }
    }
    QuestEvent FindQuestEvent(int id)
    {
        foreach(QuestEvent n in questEvents)
        {
            if (n.GetID() == id)
                return n;
        }
        return null;
    }
    public void BFS(int id, int orderNumber = 1)
    {
        QuestEvent thisEvent = FindQuestEvent(id);
        thisEvent.order = orderNumber;

        foreach (QuestPath e in thisEvent.pathList)
        {
            if (e.endEvent.order == -1)
                BFS(e.endEvent.GetID(), orderNumber + 1);
        }
    }
    public void PrintPath()
    {
        foreach(QuestEvent n in questEvents)
        {
            Debug.Log(n.name + "" + n.order);
        }
    }
}

 */ 