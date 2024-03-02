using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceCube : MonoBehaviour
{
    [HideInInspector] public StageManager sm; 
    [SerializeField] DiceDisplay diceDisplay;
    [SerializeField] float rollTime;
    [SerializeField] float rotationSpeed;
    GameObject touchingChar;
    [Space]
    public int diceValue = 0;

    void Update()
    {
        if(rollTime > 0){
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            rollTime -= Time.deltaTime;
            diceValue = Random.Range(1,7);
            diceDisplay.value = diceValue;
        }
        else{
            transform.eulerAngles = Vector3.zero;
        }
    }

    // 새로 굴리기
    public void rollDice(){
        gameObject.SetActive(true);
        rollTime = 1f;
    }


    private void OnMouseDrag() {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cursorPosition.x, cursorPosition.y, transform.position.z);    
    }

    // 주사위를 넣었을때 캐릭터 모두 주사위가 있으면 전투진행
    private void OnMouseUp() {
        if(touchingChar){
            CharDice charDice = touchingChar.GetComponent<CharDice>();
            if(charDice.character.isntHavDice()){
                PutDiceInChar(charDice.character);
            }
        }
    }

    public void PutDiceInChar(Character character){
        character.diceValue = diceValue;
        gameObject.SetActive(false);
        // sm.BattleBeginWhenAllCharsHasDice();
    }

    // 캐릭터의 주사위칸 인식을 위한 업데이트
    private void OnTriggerStay2D(Collider2D other) {
        touchingChar = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other) {
        touchingChar = null;
    }


}
