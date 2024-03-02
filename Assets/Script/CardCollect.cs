using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardCollect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public CardData holdingCard;
    public MovingObj movingObj;
    public CardDisplay cardDisplay;

    public Vector3 originPos;

    // Update is called once per frame
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
        originPos = new Vector3(transform.position.x,0,transform.position.z);
    }

    public void startDrawCard(){
        movingObj.MoveToTarget(originPos,2f);
        cardDisplay.UpdateCard(holdingCard);
    }

    public void OnPointerDown(PointerEventData eventData){
        transform.localScale = new Vector3(0.9f,0.9f,1);
        StartCoroutine(LineMove());
        foreach(CardCollect cardCollect in StageManager.init.collectCards){
            if(cardCollect != this){
            cardCollect.movingObj.MoveToTarget(new Vector3(cardCollect.transform.position.x,-8,cardCollect.transform.position.z),2f);
            }
        }
        movingObj.MoveToTarget(Vector3.zero,2f);
    }

    public void OnPointerUp(PointerEventData eventData){
        transform.localScale = new Vector3(1f,1f,1);

        if(StageManager.init.touchingChar){
            StageManager.init.touchingChar.GetCardWithData(holdingCard);
            foreach(CardCollect cardCollect in StageManager.init.collectCards){
            cardCollect.gameObject.SetActive(false);
            }
            StageManager.init.lineDraw.HideLine();
            return;
        }
        foreach(CardCollect cardCollect in StageManager.init.collectCards){
            cardCollect.movingObj.MoveToTarget(cardCollect.originPos,2f);
        }

        
    }

    IEnumerator LineMove(){
        while(!Input.GetMouseButtonUp(0)){
            StageManager.init.lineDraw.SetLine(0,transform.position);
            if(StageManager.init.touchingChar){
            StageManager.init.lineDraw.SetLine(1,StageManager.init.touchingChar.charDice.transform.position);
            }
            else{
            StageManager.init.lineDraw.SetLine(1,Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        yield return null;
        }
        StageManager.init.lineDraw.HideLine();
        yield return null;
    }



}
