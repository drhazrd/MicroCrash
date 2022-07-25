using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    private void Awake()
    {
        current = this;
    }

    public event Action<string,int,int> onPickup;

    public void ItemFetched(string itemName, int itemValue, int experienceValue)
    {
        if (onPickup != null)
        {
            onPickup(itemName,itemValue, experienceValue);
        }
    }
    
    public event Action<int> onDoorTriggerEnter;
    public void OpenDoorTrigger(int id)
    {
        if (onDoorTriggerEnter != null)
        {
            onDoorTriggerEnter(id);
        }
    }
    public event Action<int> onDoorTriggerExit;
    public void CloseDoorTrigger(int id)
    {
        if (onDoorTriggerExit != null)
        {
            onDoorTriggerExit(id);
        }
    }
    public event Action onElevatorTrigger;
    public void OperateElevator()
    {
        if (onElevatorTrigger != null)
        {
            onElevatorTrigger();
        }
    }
    public event Action onPlayerSpawn;
    public void SpawnPlayer()
    {
        if (onPlayerSpawn != null)
        {
            onPlayerSpawn();
        }
    }
    public event Action onAreaEnter;
    public event Action onLoad;
    public event Action onStateChange;
    public event Action<Quest> onQuestCompleted;
    public void GameQuestComplete(Quest quest)
    {
        if(onQuestCompleted != null)
        {
            onQuestCompleted(quest);
        }
    }
}