using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSaveSlot : MonoBehaviour
{

    public CardData cardData;

    public Image slotCorner;
    public Image cardImage;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardId;

    public void refreshCard(){
        cardName.text = CardDataBase.init.GetName(cardData.Id);
        cardId.text = "Id: " + cardData.Id.ToString();
        slotCorner.color = (CreateManager.instance.editingCardSlot == this) ? CreateManager.instance.selectedSlotColor : CreateManager.instance.de_selectedSlotColor;
    }

    public void OnSlotSelected(){
        CreateManager.instance.loadCard(this);
        CreateManager.instance.refreshAllSlot();
    }

    private void OnEnable() {
        refreshCard();    
    }

}
