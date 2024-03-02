using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CodeBlockPalette : MonoBehaviour, IPointerDownHandler
{

    private CodingManager codingManager;

    public void Init(CodingManager manager)
    {
        codingManager = manager;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        codingManager.CreateCodeBlock(gameObject);
    }
}
