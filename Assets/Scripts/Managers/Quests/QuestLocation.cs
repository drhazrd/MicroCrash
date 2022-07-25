using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class QuestLocation : MonoBehaviour
{
    public QuestManager _questManager;
    public QuestEvent _questEvent;
    Quest quest;
    public QuestEntry _questEntry;
    MeshRenderer render;
    public Color activeColor = Color.yellow, inactiveColor;
    bool active;
    private int soundID = 2;

    private void Start()
    {
        render = this.gameObject.GetComponent<MeshRenderer>();
        _questManager = QuestManager.thorManager;
    }
    private void Update()
    {
        //if(_questEvent.status == QuestEvent.EventStatus.Current)
        //{
         //   render.material.color = activeColor;
        //}else
         //   render.material.color = inactiveColor;
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "KartBody") return;
        //if (_questEvent.status != QuestEvent.EventStatus.Current) {
        //    return;
        //}
        AudioManager.audio_instance.PlaySFX(soundID);

        //_questEvent.UpdateQuestEvent(QuestEvent.EventStatus.Done);
        //_questEntry.UpdateButton(QuestEvent.EventStatus.Done);
        EventManager.current.GameQuestComplete(quest);
        Debug.Log(_questEvent.name + " was just completed");
    }
    public void Setup(QuestManager questManager, QuestEvent questEvent, QuestEntry questEntry)
    {
        _questManager = questManager;
        _questEvent = questEvent;
        _questEntry = questEntry;
        questEvent.currQuestEntry = questEntry; 
    }
} 