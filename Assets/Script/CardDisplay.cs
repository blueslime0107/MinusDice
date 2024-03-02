using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{   
    public CardData viewingCard;
    public Image image;
    public TextMeshProUGUI cardText;

    public void UpdateCard(CardData cardData)
    {   
        if(cardData != null){viewingCard = cardData;}
        image.sprite = viewingCard.image;
        cardText.text = CardDataBase.init.GetName(viewingCard.Id);
    }
}
