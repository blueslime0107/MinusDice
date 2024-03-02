using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDice : MonoBehaviour
{
    [SerializeField] DiceDisplay diceDisplay;
    [HideInInspector]public Character character;

    private void Awake() {
        character = GetComponentInParent<Character>();    
    }

    void Update()
    {
        updateDice();
    }

    void updateDice(){
        diceDisplay.value = character.diceValue;
    }

    private void OnMouseEnter() {
        StageManager.init.touchingChar = character;
        StageManager.init.refreshViewCharCards();
    }
    private void OnMouseExit() {
       StageManager.init.touchingChar = null;
        StageManager.init.refreshViewCharCards();
    }

    // private void OnMouseDown() {
    //     setThisCharBattle();

    // }

    // private void OnMouseDown() {
    //     if(!onMouse){return;}
    //     setThisCharBattle();
    // }

    // void setThisCharBattle(){
    //     if(character.bm.battleChar_atk){
    //         character.bm.battleChar_def = character;
    //     }
    //     else{
    //         character.bm.battleChar_atk = character;
    //     }
        
    // }
}
