using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestEntry : MonoBehaviour
{

	public Button buttonComponent;
	public Image icon;
	public Text eventName;
	public Color currentColor;
	public Color waitingColor;
	public Color doneColor;
	Quest thisEvent;
	bool questComplete;

	void Awake()
	{
		buttonComponent.onClick.AddListener(ClickHandler);
	}
	public void Setup(Quest e, GameObject scrollList)
	{
		thisEvent = e;
		buttonComponent.transform.SetParent(scrollList.transform, false);
		eventName.text = "<b>" + thisEvent.name + "</b>\n" + thisEvent.Information.Name;
		icon.color = waitingColor;
		buttonComponent.interactable = false;
	}
	public void UpdateButton(Quest s)
	{

		if (questComplete)
		{
			icon.color = doneColor;
			buttonComponent.interactable = false;
		}
		else if (!questComplete)
		{
			this.gameObject.SetActive(true);
			icon.color = currentColor;
			buttonComponent.interactable = true;
		}
	}
	public void ClickHandler()
	{
		//targetMarker.target = thisEvent.location;
	}
}
