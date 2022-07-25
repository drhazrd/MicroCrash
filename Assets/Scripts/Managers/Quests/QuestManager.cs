using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager thorManager;

    public List<Quest> CurrentQuests;
    GameObject mikey;

    public GameObject questHolder;
    public Transform questContent;
    public GameObject questPrefab;
    QuestEvent final;

    public GameObject[] questLocations;

    private void Awake()
    {
        thorManager = this;
        foreach (var quest in CurrentQuests)
        {
            quest.Initilize();
            quest.QuestCompleted.AddListener(QuestOnCompletion);
            GameObject questObj = Instantiate(questPrefab, questContent);
            questObj.transform.Find("Icon").GetComponent<Image>().sprite = quest.Information.Icon;
            questObj.GetComponent<Button>().onClick.AddListener(delegate {
                questHolder.GetComponent<QuestWindow>().Initialize(quest);
                questHolder.SetActive(true);

            });
        }
    }
    public void Fetched(string item, int passedValue, int otherPassedValue)
    {
        EventManager.current.ItemFetched(item, passedValue, passedValue);
    }
    private void QuestOnCompletion(Quest quest)
    {
        questContent.GetChild(CurrentQuests.IndexOf(quest)).Find("checkmark").gameObject.SetActive(true);
    }
    public void CloseWindow()
    {
        gameObject.SetActive(false);
        for(int i = 0; i < questContent.childCount; i++)
        {
            Destroy(questContent.GetChild(i).gameObject);
        }
        mikey.AddComponent<QuestLocation>().Setup(this,final,new QuestEntry());
    }
   
}