using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIBehavior : MonoBehaviour {
    string panelName = "";
	void Awake()
    {
        panelName = transform.GetComponentInParent<UIBase>().transform.name;
        UIManager.Instance.RegistGameObject(panelName,transform.name,gameObject);
    }

    public void AddButtonListen(UnityAction tempAction)
    {
        Button tempButton = gameObject.GetComponent<Button>();
        if(tempButton!=null)
        tempButton.onClick.AddListener(tempAction);
    }

    public void AddSliderListen(UnityAction<float> tempAction)
    {
        Slider tempSlider = gameObject.GetComponent<Slider>();
        if (tempSlider != null)
            tempSlider.onValueChanged.AddListener(tempAction);
    }

    public void AddInputEndEdit(UnityAction<string> tmpAction)
    {
        InputField tempBtn = gameObject.GetComponent<InputField>();
        if (tempBtn!=null)
        {
            tempBtn.onEndEdit.AddListener(tmpAction);
        }
    }

    public void AddInputValueChange(UnityAction<string> tempAction)
    {
        InputField tempBtn = gameObject.GetComponent<InputField>();
        if (tempBtn!=null)
        {
            tempBtn.onValueChanged.AddListener(tempAction);
        }
    }

    public void AddPointDownLisenter(UnityAction<BaseEventData> action)
    {
        //trigger对象
        EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger=gameObject.AddComponent<EventTrigger>();
        }
        //入口
        EventTrigger.Entry entry = new EventTrigger.Entry();
        //配置入口
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(action);
        //添加到trigger事件
        trigger.triggers.Add(entry);
    }
}
