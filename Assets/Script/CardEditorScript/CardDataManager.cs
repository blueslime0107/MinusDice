using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

[System.Serializable]
// public class CardDataBase {
//     public List<CardData> cardList = new List<CardData>();
// }

public class CardDataManager : MonoBehaviour
{
    public static CardDataManager instance = null; 
    public static CardDataBase dataBase = null; 

    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            // DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }

        createCardDBFile();
    }
    
    public string filePath = "";

    public void createCardDBFile(){
        string fileDist = Path.Combine(Application.dataPath+filePath, "CardDataFile.json");

        if (File.Exists(fileDist))
        {
            Debug.Log("JSON 파일이 이미 존재합니다. 새로 생성하지 않습니다.");
            return; // 이미 파일이 존재하면 더 이상 진행하지 않음
        }

        // 새로운 데이터 생성
        // CardData newCard = new CardData();
        // newCard.Name = "NEW CARD";
        // newCard.Id = 0;

        // dataBase.cardList.Add(newCard);

        // 데이터를 JSON 문자열로 변환
        string jsonText = JsonUtility.ToJson(dataBase, true);

        // JSON 파일에 쓰기
        File.WriteAllText(fileDist, jsonText);

        Debug.Log("JSON 파일이 생성되었습니다. 경로: " + fileDist);
    }



    public void loadCardDBFile(){
        string fileDist = Application.dataPath+filePath+"/CardDataFile.json";

            // JSON 파일 읽기
        string jsonString = File.ReadAllText(fileDist);

            // JSON 문자열을 객체로 역직렬화
        dataBase = JsonUtility.FromJson<CardDataBase>(jsonString);

    }

    public void saveCardDBFile(CardDataBase cardDataBase){
        string fileDist = Application.dataPath+filePath+"/CardDataFile.json";

            // JSON 문자열을 객체로 역직렬화
        Debug.Log(cardDataBase.ToString());
        string jsonText = JsonUtility.ToJson(cardDataBase, true);
        Debug.Log(jsonText);

        // JSON 파일에 쓰기
        File.WriteAllText(fileDist, jsonText);

    }
}
