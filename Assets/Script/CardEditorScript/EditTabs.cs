using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TabObject{
    public Button button;
    public GameObject tapTable;
}

public class EditTabs : MonoBehaviour
{
    [SerializeField] List<TabObject> tabObjects = new List<TabObject>();

    private void Awake() {
         for(int i=0;i<tabObjects.Count;i++){
            int index = i;
            tabObjects[i].button.onClick.AddListener(() => WhenButtonPressed(tabObjects[index].button));
        }    
    }
    // Start is called before the first frame update
    void WhenButtonPressed(Button button)
    {
        for(int i=0;i<tabObjects.Count;i++){
            tabObjects[i].tapTable.SetActive(false);
            if(tabObjects[i].button == button){
                tabObjects[i].tapTable.SetActive(true);
            }
        }        
    }
}
