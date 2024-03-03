using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[System.Serializable]
public class BattleRoom{
    public Character attacker;
    public Character defender;
    public int damage;
    public bool battleProcessing = true;
    public bool updateProcessing = true;

    public List<CardPack> activedCards = new List<CardPack>(); 

    public void AtStart(){
        updateProcessing = true;
    } 

    public IEnumerator UpdateCardEvent(){
        while(true){
        List<CardAbility> abilist = StageManager.init.checkEffectOFAllCards(this);
        for (int i = 0; i < abilist.Count; i++)
        {
            abilist[i].Affect();
            activedCards.Add(abilist[0].card);
        }
         Debug.Log(abilist.Count);
        if(abilist.Count == 0){
            TakeDamage();
        }
        if(!battleProcessing){
            break;
        }
        yield return null;
        }
    }

    public void TakeDamage(){
    if(damage > 0){
       Vector3 knokBack = defender.transform.position; 
        knokBack += ((defender.transform.position.x < attacker.transform.position.x) ? Vector3.left : Vector3.right) * damage * 0.5f;
       defender.movingObj.MoveToTarget(knokBack,1f);
       attacker.changePose(3);
       defender.changePose(4);

    }
    else{
         Vector3 knokBack = defender.transform.position; 
        knokBack += ((defender.transform.position.x < attacker.transform.position.x) ? Vector3.left : Vector3.right) * 0.5f;
        
        defender.movingObj.MoveToTarget(knokBack,1f);
        attacker.movingObj.MoveToTarget(-knokBack,1f);

       attacker.changePose(3);
        defender.changePose(3);
    }
    defender.GetDamage(damage);
    battleProcessing = false;
    }
}

public enum BattleState {
    ClashFin
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager init;
    [HideInInspector]public Character attacker;
    [HideInInspector]public Character defender;

    
    public BattleState curState;

    [Header("Editor Data")]

    [SerializeField] Vector3 clashRightPosition;
    [SerializeField] Vector3 clashLeftPosition;
    [SerializeField] GameObject battleBG_Black;

    public List<BattleRoom> battleRooms = new List<BattleRoom>();

    private void Awake() {
        if(init == null){init = this;}
    }

    public void MatchStart(Character char_atk, Character char_def){
        attacker = char_atk;
        defender = char_def;

        attacker.transform.position = new Vector3(attacker.transform.position.x,attacker.transform.position.y,0);
        defender.transform.position = new Vector3(defender.transform.position.x,defender.transform.position.y,0);

        StageManager.init.gameCamera.objectsToInclude.Add(attacker.gameObject);
        StageManager.init.gameCamera.objectsToInclude.Add(defender.gameObject);

        attacker.GetState(CharState.IsInClash);
        defender.GetState(CharState.IsInClash);

        battleBG_Black.SetActive(true);
        StartCoroutine(BattleCorutine());
    }

    IEnumerator BattleCorutine(){

        attacker.changePose(2);
        defender.changePose(2);
        if(attacker.transform.position.x > defender.transform.position.x){
        attacker.movingObj.MoveToTarget(clashRightPosition,1f);
        defender.movingObj.MoveToTarget(clashLeftPosition,1f);
        }else{
        attacker.movingObj.MoveToTarget(clashLeftPosition,1f);
        defender.movingObj.MoveToTarget(clashRightPosition,1f);
        }

        

        while(attacker.movingObj.isMoving() || defender.movingObj.isMoving()){
            yield return null;
        }

       if(attacker.diceValue == 1 && defender.diceValue >= 6){attacker.diceValue += 6;}
       if(defender.diceValue == 1 && attacker.diceValue >= 6){defender.diceValue += 6;}

       

       yield return new WaitForSeconds(0.5f);

       if(attacker.diceValue > defender.diceValue){
        attacker.GetState(CharState.Winner);
        defender.GetState(CharState.Loser);
        MakeBattleRoom(attacker, defender, Math.Abs(attacker.diceValue - defender.diceValue));
       } 
       else{
        defender.GetState(CharState.Winner);
        attacker.GetState(CharState.Loser);
        MakeBattleRoom(defender,attacker , Math.Abs(attacker.diceValue - defender.diceValue));
       }

       
       while(battleRooms.Count > 0){
        StartCoroutine(battleRooms[0].UpdateCardEvent());
        while(battleRooms[0].battleProcessing){
           
            yield return null;
        }
        Debug.Log("Breaked");
        yield return new WaitForSeconds(1f);
        battleRooms.RemoveAt(0);
        yield return null;
       }
       

       ClashEnd();

        while(attacker.movingObj.isMoving() || defender.movingObj.isMoving()){
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

       MatchEnd(); 

       yield return null;
    }

    public void ClashEnd(){
       attacker.diceValue = 0;
       defender.diceValue = 0;
    }



    public void MatchEnd(){

        attacker.changePose(1);
        defender.changePose(1);

        attacker.RemoveState(CharState.Winner);
        attacker.RemoveState(CharState.Loser);
        attacker.RemoveState(CharState.IsInClash);
        defender.RemoveState(CharState.Winner);
        defender.RemoveState(CharState.Loser);
        defender.RemoveState(CharState.IsInClash);


        attacker.movingObj.MoveToTarget(attacker.originPosition,2f,true);
        defender.movingObj.MoveToTarget(defender.originPosition,2f,true);

        StageManager.init.gameCamera.objectsToInclude.Clear();

        battleBG_Black.SetActive(false);
    }

    public void MakeBattleRoom(Character atk, Character def,int damage){
        BattleRoom newRoom = new BattleRoom();
        newRoom.attacker = atk;
        newRoom.defender = def;
        newRoom.damage = damage;
        battleRooms.Add(newRoom);
    } 

    public BattleRoom CurRoom(){
        return battleRooms[0];
    }
}
