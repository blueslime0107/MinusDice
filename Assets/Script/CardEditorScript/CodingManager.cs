using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CodingManager : MonoBehaviour
{
    public List<CodeBlockPalette> codeBlocks = new List<CodeBlockPalette>(); // CodeBlock 리스트

    private RectTransform codeBlockContainer; // CodeBlock이 복제될 부모 컨테이너

    public CodeBlock holdingBlock;

    void Start()
    {
        codeBlockContainer = new GameObject("CodeBlockContainer").AddComponent<RectTransform>();
        codeBlockContainer.SetParent(transform);
        codeBlockContainer.localScale = Vector3.one;
        codeBlockContainer.localPosition = Vector3.zero;
        codeBlockContainer.sizeDelta = new Vector2(Screen.width, Screen.height);

        // CodeBlock 리스트 초기화
        foreach (CodeBlockPalette codeBlock in codeBlocks)
        {
            codeBlock.Init(this);
        }
    }

    // CodeBlock을 생성하는 함수
    public void CreateCodeBlock(GameObject codeBlock)
    {
        GameObject clonedCodeBlock = Instantiate(codeBlock, codeBlockContainer);

        clonedCodeBlock.GetComponent<CanvasGroup>().blocksRaycasts = false; // 복제된 CodeBlock이 마우스 이벤트를 가로채지 않도록 함
        clonedCodeBlock.GetComponent<Canvas>().sortingOrder = 1; // Sorting Order를 설정하여 다른 UI 오브젝트보다 위에 나타나도록 함

        // 이동 위치 설정 등 기타 설정이 필요하다면 여기에 추가

        // 복제된 CodeBlock을 CodeBlock의 리스트에 추가
    }
}