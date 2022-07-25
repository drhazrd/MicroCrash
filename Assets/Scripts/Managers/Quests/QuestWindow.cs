using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    [SerializeField]
    Text titleText;
    [SerializeField]
    Text descriptionText;
    [SerializeField]
    GameObject goalPrefab;
    [SerializeField]
    Transform goalsContent;
    [SerializeField]
    Text xpText;
    [SerializeField]
    Text coinsText;

    public void Initialize(Quest quest)
    {
        titleText.text = quest.Information.Name;
        descriptionText.text = quest.Information.Description;
        foreach(var goal in quest.Goals)
        {
            GameObject goalObj = Instantiate(goalPrefab, goalsContent);
            goalObj.transform.Find("Text").GetComponent<Text>().text = goal.GetDescription();

            GameObject countObj = goalObj.transform.Find("Count").gameObject;
            GameObject skipObj = goalObj.transform.Find("Skip").gameObject;

            if (goal.Completed)
            {
                countObj.SetActive(false);
                skipObj.SetActive(true);
                goalObj.transform.Find("Done").gameObject.SetActive(true);
            }
            else
            {
                countObj.GetComponent<Text>().text = goal.CurrentAmount + "/" + goal.RequiredAmount;
                skipObj.GetComponent<Button>().onClick.AddListener(delegate
                {
                    goal.Skip();

                    countObj.SetActive(false);
                    skipObj.SetActive(false);
                    goalObj.transform.Find("Done").gameObject.SetActive(true);

                });
            }
        }
        xpText.text = quest.Reward.Currency.ToString();
        coinsText.text = quest.Reward.XP.ToString();
    }
}
