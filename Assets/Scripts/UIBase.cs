using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIBase : MonoBehaviour {

    void Awake()
    {
        AddComponentToChild();
    }
    
    public void AddComponentToChild()
    {
        Transform[] allChild = transform.GetComponentsInChildren<Transform>();
        for (int i=0;i<allChild.Length;i++)
        {
            if(allChild[i].name.EndsWith("_N"))
            allChild[i].gameObject.AddComponent<UIBehavior>();
            else if(allChild[i].name.EndsWith("_L"))
            {
                allChild[i].gameObject.AddComponent<UISubManager>();
            }
        }
    }

    public GameObject GetGameObject(string objName)
    {
        return UIManager.Instance.GetGameObject(transform.name,objName);
    }

    public void AddSubButtonListen(string cellName,string cellChild,UnityAction action)
    {
        GameObject cellObj = GetGameObject(cellName);
        cellObj.GetComponent<UISubManager>().AddButtonListen(cellChild, action);
    }

    public void AddButtonListen(string objName,UnityAction action)
    {
        GameObject sonChild = GetGameObject(objName);
        sonChild.GetComponent<UIBehavior>().AddButtonListen(action);
    }

    public void AddSliderListen(string objName,UnityAction<float> action)
    {
        GameObject sonChild = GetGameObject(objName);
        sonChild.GetComponent<UIBehavior>().AddSliderListen(action);
    }

    public void AddInputEndEdit(string objName,UnityAction<string> action)
    {
        GameObject sonChild = GetGameObject(objName);
        sonChild.GetComponent<UIBehavior>().AddInputEndEdit(action);
    }

    public void AddInputValueChange(string objName,UnityAction<string> action)
    {
        GameObject sonChild = GetGameObject(objName);
        sonChild.GetComponent<UIBehavior>().AddInputValueChange(action);
    }

    public void AddPointDownLisenter(string objName, UnityAction<BaseEventData> action)
    {
        GameObject sonChild = GetGameObject(objName);
        sonChild.GetComponent<UIBehavior>().AddPointDownLisenter(action);
    }

    private void OnDestroy()
    {
        UIManager.Instance.UnRegistPanelGameObject(transform.name);
    }
}
