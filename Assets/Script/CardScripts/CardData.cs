using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityData{
    public abstract void Condition();
    public abstract void Affect();
} 

[CreateAssetMenu(menuName = "CardData")]
public class CardData : ScriptableObject
{
    public int Id;
    public Sprite image;
}
