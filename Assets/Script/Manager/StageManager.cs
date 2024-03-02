using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEditor.Rendering.LookDev;
using UnityEditor.SceneManagement;
using UnityEngine;

[System.Serializable]
public class Team{
    public List<Character> chars;
    public List<CardData> cardDeck;
    public List<int> cardDeckLi_Id;
    public List<CardData> cardGrave;
    public int cardDrawCount = 0;

    public void Init(){
        for (int i = 0; i < chars.Count; i++)
        {
            chars[i].team = this;
        }
        for (int i = 0; i < cardDeckLi_Id.Count; i++)
        {
            cardDeck.Add(CardDataBase.init.cardDatas[cardDeckLi_Id[i]]);
        }
        // cardDeck = cardDeck_List;
        ShuffleDeck();
    }

    public void DrawCards(int count){
        List<CardData> newCardList = new List<CardData>();
        for (int i = 0; i < count; i++)
        {
            newCardList.Add(cardDeck[0]);
            Debug.Log(cardDeck[0]);
            cardDeck.RemoveAt(0);
        }
        StageManager.init.DrawCards(newCardList);
    }

    public void ShuffleDeck(){
        int n = cardDeck.Count;
        System.Random random = new System.Random();

        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);

            CardData temp = cardDeck[i];
            cardDeck[i] = cardDeck[j];
            cardDeck[j] = temp;
        }
        

        // return list; // 섞인 리스트 반환
    }
}

public class StageManager : MonoBehaviour
{
    public static StageManager init;

    [Header("Game Data")]

    public Team RTeam;
    public Team LTeam;

    [HideInInspector] public List<DiceCube> dices;

    [SerializeField] List<Character> characters;

    [SerializeField] List<CardDisplay> holdingCards;


    // public List<CardData> cardDeck;
    // public List<CardData> cardDeck_List;
    // public List<CardData> cardGrave;


    [Header("Editor Data")]

    [SerializeField] public List<CardCollect> collectCards;
    public Character touchingChar;
    public Character holdingViewLockChar;
    [SerializeField] int howManyDice;
    public Character battleChar_atk;
    public Character battleChar_def;

    public bool allCharsNotMove;

    [Header("Object Holder")]
    [SerializeField] GameObject diceCubeobj;
    public GameCamera gameCamera;
    public GameObject holdingCardView;
    public GameObject holdingCardViewContent;
    public GameObject collectCard_obj;
    public LineDraw lineDraw;


    // [SerializeField] List<CardAbility> cards;

    private void Awake()
    {
        if(init == null){init = this;}

        for(int i=0;i<howManyDice;i++){
        DiceCube obj = Instantiate(diceCubeobj).GetComponent<DiceCube>();
        obj.gameObject.SetActive(false);
        dices.Add(obj);
        obj.sm = this;
        }

        // 캐릭터에게 매니저 부여
        for(int i=0;i<characters.Count;i++){
            characters[i].sm = this;
        }

        
        

        // for(int i=0;i<cards.Count;i++){
        // cards[i].WhenClashWin(battleChar_atk);
        // }
    }

    private void Start() {
        LTeam.Init();
        RTeam.Init();
        StartCoroutine(MainBattle());
    }

    void Update(){
        DefaultUpdate();
    }

    void DefaultUpdate(){
        updateLockChar();
    }

    IEnumerator MainBattle(){
        while(true){
            while(LTeam.cardDrawCount > 0 || RTeam.cardDrawCount > 0){
                if(LTeam.cardDrawCount > 0){
                    LTeam.cardDrawCount--;
                    LTeam.DrawCards(3);
                    while(collectCards[0].gameObject.activeSelf){
                        yield return null;
                    }
                    // yield return new WaitForSeconds(0.5f);
                }
                if(RTeam.cardDrawCount > 0){
                    RTeam.cardDrawCount--;
                    RTeam.DrawCards(3);
                    while(collectCards[0].gameObject.activeSelf){
                        yield return null;
                    }
                    // yield return new WaitForSeconds(0.5f);
                }
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
            RollDice();
            while(!checkAllCharsDice()){
                yield return null;
            }
            while(checkNoOneHasDice()){
                SelectCharacterBattle();
                yield return null;
            }
            while(true){
                for (int i = 0; i < characters.Count; i++)
                {
                    if(characters[i].movingObj.isMoving()){
                        break;
                    }
                    if(i == characters.Count-1){
                        allCharsNotMove = true;
                    }
                }
                if(allCharsNotMove){
                    allCharsNotMove = false;
                    break;
                }
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SelectCharacterBattle(){
        if(Input.GetMouseButtonDown(0)){
        if(touchingChar){
        battleChar_atk = touchingChar;
        lineDraw.SetLine(0,battleChar_atk.charDice.transform.position);
        }
        }
        if(Input.GetMouseButton(0) && battleChar_atk){
            if(touchingChar){
                lineDraw.SetLine(1,touchingChar.charDice.transform.position);
            }
            else{
                lineDraw.SetLine(1,Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
        if(Input.GetMouseButtonUp(0)){
            if(battleChar_atk && touchingChar && battleChar_atk != touchingChar){
                battleChar_def = touchingChar;
                BattleManager.init.MatchStart(battleChar_atk,battleChar_def);
            }
            battleChar_atk = null;
            battleChar_def = null;
            lineDraw.HideLine();
        }
    }    

    // 버튼을 누르면 주사위를 굴림
    public void RollDice(){
        List<float> newList = ArrangeObjects(dices.Count,12,12);
        for(int i=0;i<dices.Count;i++){
            DiceCube diceCube = dices[i];
            diceCube.gameObject.transform.position = new Vector3(newList[i], 3.5f,i);
            if(diceCube.gameObject.activeSelf){
                diceCube.PutDiceInChar(characters[i]);
                continue;
            }
            diceCube.rollDice();
        }
    }

    public void DrawCards(List<CardData> cardDatas){
        for(int i=0;i<cardDatas.Count;i++){
            if(collectCards.Count == i){
                GameObject obj = Instantiate(collectCard_obj);
                collectCards.Add(obj.GetComponent<CardCollect>());
            }
            collectCards[i].SetPosition(Vector3.right * (-5 + 5*i) + Vector3.up * 5);
            collectCards[i].holdingCard = cardDatas[i];
            collectCards[i].gameObject.SetActive(true);
            collectCards[i].startDrawCard();
        }
    }

    public List<float> ArrangeObjects(int objectCount, float maxArraySize, float maxObjectSpacing)
    {
        List<float> xValues = new List<float>();
        float newSize = maxArraySize;

        float term = newSize / (objectCount-1);
        if(term > maxObjectSpacing){
            Debug.Log("OVered");
            term = maxObjectSpacing;
            newSize = term * (objectCount-1); 
        }

        Debug.Log("TERM"+term);

        for (int i = 0; i < objectCount; i++)
        {
            Debug.Log(term * i - newSize*0.5f);
            xValues.Add(term * i - newSize*0.5f); 

        }

        // Debug.Log(xValues);

        return xValues;
    }
    // 모든 캐릭터가 주사위를 가졌는지
    public bool checkAllCharsDice(){
        for(int i=0;i<characters.Count;i++){
            if(characters[i].diceValue <= 0)
            return false;
        }
        return true;
    }

    public bool checkNoOneHasDice(){
        for(int i=0;i<characters.Count;i++){
            if(characters[i].diceValue > 0)
            return true;
        }
        return false;
    }

    public void ViewCharacterCards(Character character){
        for(int i=0;i<holdingCards.Count;i++){
            holdingCards[i].gameObject.SetActive(false);
        }
        if(character == null){return;}
        for(int i=0;i<character.cards.Count;i++){
            if(i == holdingCards.Count){
                GameObject cardView = Instantiate(holdingCardView, holdingCardViewContent.transform);
                holdingCards.Add(cardView.GetComponent<CardDisplay>());
            }
            holdingCards[i].gameObject.SetActive(true);
            holdingCards[i].UpdateCard(character.cards[i].cardData);
        }
    }

    public void refreshViewCharCards(){
        ViewCharacterCards((touchingChar == null) ? holdingViewLockChar : touchingChar);
    }

    void updateLockChar(){
        if(Input.GetMouseButtonDown(0)){
            if(touchingChar){
                holdingViewLockChar = touchingChar;
            }
            else{
                holdingViewLockChar = null;
                refreshViewCharCards();
            }
        }
    }
}
