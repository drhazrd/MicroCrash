using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchQuestGoals : Quest.QuestGoal
{
    public string FetchObject;
    public int currentcyValue;
    public int experienceValue;
    public override string GetDescription()
    {
        return $"Found a {FetchObject}";
    }
    public override void Initialize()
    {
        base.Initialize();
        EventManager.current.ItemFetched(GetDescription(), currentcyValue, experienceValue);

    }
    private void OnItemFetch(FetchGameEvent gameEventInfo)
    {
        if (gameEventInfo.FetchItemName == FetchObject)
        {
            CurrentAmount++;
            Evaluate();
        }
    }
}
