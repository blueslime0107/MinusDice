using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CodeBlock : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform codeBlockRectTransform;
    private Vector2 offset;
    private CodingManager codingManager;

    void Start()
    {
        codeBlockRectTransform = GetComponent<RectTransform>();
    }

    // CodingManager에 대한 초기화 함수
    public void Init(CodingManager manager)
    {
        codingManager = manager;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(codeBlockRectTransform, eventData.position, eventData.pressEventCamera, out offset);

        // 마우스를 누르면 CodeBlock을 복제하여 따라다니게 함
        codingManager.CreateCodeBlock(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (codeBlockRectTransform == null)
            return;

        Vector2 pointerDelta = eventData.delta;
        Vector2 position = codeBlockRectTransform.anchoredPosition + pointerDelta;

        codeBlockRectTransform.anchoredPosition = position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 마우스를 뗐을 때 추가적인 로직이 필요하다면 여기에 추가
    }
}
