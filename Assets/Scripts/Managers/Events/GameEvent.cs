using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : MonoBehaviour
{
    public string EventDescritpion;
}
public class FetchGameEvent : GameEvent
{
    public string FetchItemName;
    public FetchGameEvent(string name)
    {
        FetchItemName = name;
    }
}
