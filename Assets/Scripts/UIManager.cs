using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour {
    Dictionary<string, Dictionary<string, GameObject>> allChild;
    public static UIManager Instance;
    void Awake()
    {
        allChild = new Dictionary<string, Dictionary<string, GameObject>>();
        Instance = this;
    }
	

    public void RegistGameObject(string panelName,string objName,GameObject obj)
    {
        if (!allChild.ContainsKey(panelName))
        {
            Dictionary<string, GameObject> tempDict = new Dictionary<string, GameObject>();
            allChild.Add(panelName,tempDict);
        }
        allChild[panelName].Add(objName, obj);
    }

    public void UnRegistPanelGameObject(string panelName)
    {
        if (allChild.ContainsKey(panelName))
        {
            allChild[panelName].Clear();
        }
    }

    public void UnRegistGameObject(string paneName,string objName)
    {
        if (allChild.ContainsKey(paneName))
        {
            if (allChild[paneName].ContainsKey(objName))
            {
                allChild[paneName].Remove(objName);
            }
        }
    }

    public GameObject GetGameObject(string panelName,string objName)
    {
        if (allChild.ContainsKey(panelName))
        {
            return allChild[panelName][objName];
        }
        return null;
    }
}
