using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISubManager : MonoBehaviour {

    Dictionary<string, GameObject> subChild;
    string panelName = "";

    void Awake()
    {
        panelName = transform.GetComponentInParent<UIBase>().transform.name;
        UIManager.Instance.RegistGameObject(panelName, transform.name, gameObject);
        RegistAllChild();
    }

    public void RegistAllChild()
    {
        subChild = new Dictionary<string, GameObject>();
        Transform[] allChild=transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < allChild.Length; i++)
        {
            if (allChild[i].name.EndsWith("_C"))
            {
                subChild.Add(allChild[i].name,allChild[i].gameObject);
            }
        }
    }

    public void AddButtonListen(string objName, UnityAction tempAction)
    {
        if (subChild.ContainsKey(objName))
        {
            Button tempButton = subChild[objName].GetComponent<Button>();
            if (tempButton != null)
                tempButton.onClick.AddListener(tempAction);
        }
    }

    public void AddSliderListen(string objName, UnityAction<float> tempAction)
    {
        if (subChild.ContainsKey(objName))
        {
            Slider tempSlider = gameObject.GetComponent<Slider>();
            if (tempSlider != null)
                tempSlider.onValueChanged.AddListener(tempAction);
        }
    }

    public void AddInputEndEdit(string objName,UnityAction<string> tempAction)
    {
        InputField inputField = gameObject.GetComponent<InputField>();
        if (inputField!=null)
        {
            inputField.onEndEdit.AddListener(tempAction);
        }
    }

    public void AddInputValueChange(string objName,UnityAction<string> tempAction)
    {
        InputField inputField = gameObject.GetComponent<InputField>();
        if (inputField!=null)
        {
            inputField.onValueChanged.AddListener(tempAction);
        }
    }

    public void AddPointDownLisenter(string objName, UnityAction<BaseEventData> action)
    {
        if (subChild.ContainsKey(objName))
        {
            //trigger对象
            EventTrigger trigger = subChild[objName].GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = gameObject.AddComponent<EventTrigger>();
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
}
