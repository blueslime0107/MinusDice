using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CreateManager : MonoBehaviour
{
    public static CreateManager instance = null; 

    public CardSaveSlot editingCardSlot;
    public List<CardSaveSlot> cardSaveSlots = new List<CardSaveSlot>();

    public Image preView_cardImage;
    public TextMeshProUGUI preView_cardName;
    public TextMeshProUGUI preView_cardFlavor;

    public TMP_InputField input_cardName;
    public TMP_InputField input_cardFlavor;

    public GameObject obj_cardSlot;
    public GameObject cardSlot_content;

    public Color32 de_selectedSlotColor;
    public Color32 selectedSlotColor;

    void Awake(){
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {instance = this; //내자신을 instance로 넣어줍니다.
            // DontDestroyOnLoad(gameObject);
        }
        else{
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CardDataManager.instance.loadCardDBFile();

        // for(int i=0;i<CardDataManager.dataBase.cardList.Count;i++){
        //     CardData cardData = CardDataManager.dataBase.cardList[i];
        //     if(cardSaveSlots.Count-1 < i || cardSaveSlots[i] == null){
        //         CardSaveSlot saveSlot = Instantiate(obj_cardSlot).GetComponent<CardSaveSlot>();
        //         saveSlot.transform.SetParent(cardSlot_content.transform);

        //         saveSlot.cardData = cardData;
        //         saveSlot.refreshCard();

        //         cardSaveSlots.Add(saveSlot);
        //     }
        // }
    }


    public void AddNewCardSlot(){

        CardSaveSlot newSlot = Instantiate(obj_cardSlot).GetComponent<CardSaveSlot>();
        newSlot.transform.SetParent(cardSlot_content.transform);

        newSlot.cardData = new CardData();
        newSlot.refreshCard();

        cardSaveSlots.Add(newSlot);
        // return newSlot;
    }

    public void SaveCardData(){
        CardDataBase cardDataBase = new CardDataBase();
        for(int i=0;i<cardSaveSlots.Count;i++){
            // cardDataBase.cardList.Add(cardSaveSlots[i].cardData);
        }
        CardDataManager.instance.saveCardDBFile(cardDataBase);
    }

    // 카드 항목을 눌렀을때 편집중인 카드로 불러오기
    public void loadCard(CardSaveSlot cardSaveSlot){
        editingCardSlot = cardSaveSlot;

        // preView_cardName.text = CardDataBase.init.GetName(editingCardSlot.cardData);

        // input_cardName.text = editingCardSlot.cardData.Name;
        // input_cardFlavor.text = editingCardSlot.cardData.Flavor;
    }

    // 카드 정보 입력 완료시 수정
    public void updateEditingCard(){
        // editingCardSlot.cardData.Name = input_cardName.text;
        // editingCardSlot.cardData.Flavor = input_cardFlavor.text;

        preView_cardName.text = input_cardName.text;
    }


    public void refreshAllSlot(){
        for(int i=0;i<cardSaveSlots.Count;i++){
            cardSaveSlots[i].refreshCard();
        }
    }

}
