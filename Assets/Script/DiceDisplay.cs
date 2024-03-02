using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DiceDisplay : MonoBehaviour
{

    [SerializeField] SpriteRenderer spRenderer;
    [SerializeField] TextMeshProUGUI diceText;
    [SerializeField] List<Sprite> diceSprites;

    [Space ]
    public int value = 0;
    // Update is called once per frame
    void Update()
    {
        bool overTW = value > 12;
        spRenderer.sprite = diceSprites[overTW ? 13 : value];
        diceText.enabled = overTW;
        diceText.text = value.ToString();
        
    }   
}
