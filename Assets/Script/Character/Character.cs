using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;

public enum State{
    Winner, Loser
}

public class Character : MonoBehaviour
{
    [Header("Character Data")]

    public Team team;

    public int diceValue = 0;
    public float max_health = 10;
    public float health = 10;
    public List<CardPack> cards = new List<CardPack>();

    public List<int> breakPoints = new List<int>();

    [Header("Editor Data")]
    public bool isMouseTouching;

    public List<string> charTags = new List<string>();
    public List<CardData> preView_cards = new List<CardData>();
    public Costume costume;
    

    [Header("Obj Holder")]

    public StageManager sm;
    public CharDice charDice;
    public HPIndicator hPIndicator;
    public MovingObj movingObj;
    public Vector3 originPosition;
    public SpriteRenderer charRenderer;
    public GameObject charShadow;

    private void Awake() {
        for (int i = 0; i < preView_cards.Count; i++){
            GetCardWithData(preView_cards[i]);
        }
        originPosition = transform.position;
    }

    private void Start() {
        hPIndicator.refreshData(this);
    }

    public void GetCardWithData(CardData cardData){
        CardPack newCardPack = new CardPack();
        newCardPack.cardData = cardData;
        newCardPack.owner = this;
        newCardPack.Init();
        cards.Add(newCardPack);
    }

    public bool isntHavDice(){
        return diceValue <= 0;
    }

    public void changePose(int value){
        charRenderer.sprite = costume.costumes[value];
    }

    public void GetDamage(int value){
        health -= value;
        if(breakPoints.Count > 0){
           while (breakPoints.Count > 0 && breakPoints[0] >= health) {
                breakPoints.RemoveAt(0);
                team.cardDrawCount++;
            }
        }
        hPIndicator.refreshData(this);
    }
    // Update is called once per frame
    
}
