using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardAbility{
    public CardPack card;
    public virtual bool Condition(){return true;}
    public virtual void Affect(){}
}

[System.Serializable]
public class CardPack 
{
    [HideInInspector]public Character owner;
    public CardData cardData;
    public List<CardAbility> cardAbilities = new List<CardAbility>();
    public void Init(){
        int cardNumber = cardData.Id;
        string className = "CardId_" + cardNumber;

        // 카드 아이디의 클래스를 가져온다
        System.Type type = System.Type.GetType(className);
        if (type != null)
        {
            // 그 클래스가 가진 하위 클래스들을 가져온다
            System.Type[] nestedTypes = type.GetNestedTypes(System.Reflection.BindingFlags.NonPublic);
            
            foreach (System.Type nestedType in nestedTypes)
            {
                // 하위 클래스들의 CardAbility 클래스들을 리스트에 넣는다 
                object nestedInstance = System.Activator.CreateInstance(nestedType);
                CardAbility cardA = (CardAbility)nestedInstance;
                cardA.card = this;
                cardAbilities.Add((CardAbility)nestedInstance);
            }
        }
    }

}


