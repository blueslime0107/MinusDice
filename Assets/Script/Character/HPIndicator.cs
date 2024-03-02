using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPIndicator : MonoBehaviour
{
    public Slider slider;
    public GameObject breakLine_obj;
    public TextMeshProUGUI hpIndicate;
    public TextMeshProUGUI breakIndicate;

    public List<RectTransform> breakObjList = new List<RectTransform>();

    public void refreshData(Character character){

        for (int i = 0; i < breakObjList.Count; i++)
        {
            breakObjList[i].gameObject.SetActive(false);
        }

        if(character.breakPoints.Count > 0){
        for(int i=0;i<character.breakPoints.Count;i++){
            if(i == breakObjList.Count){
                breakObjList.Add(Instantiate(breakLine_obj,slider.transform).GetComponent<RectTransform>());
            }
            breakObjList[i].anchoredPosition = Vector2.right * (character.breakPoints[i] / character.max_health) * 2.5f;
            breakObjList[i].gameObject.SetActive(true);
            // breakObjList[i]
        }
        }


        slider.value = character.health / character.max_health;
        hpIndicate.text = character.health.ToString();
        breakIndicate.text = (character.breakPoints.Count > 0) ? character.breakPoints[0].ToString() : "-";
    }
}
