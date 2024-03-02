using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using System.IO;

[System.Serializable]
public class LanguageCardText{
    public int Id;
    public string Name;
    public string Flavor;
    public string Abili;
}

[System.Serializable]
public class LanguageCardTextList{
    public List<LanguageCardText> cardList = new List<LanguageCardText>();
}

public class CardDataBase : MonoBehaviour
{
    public static CardDataBase init;
    public List <CardData> cardDatas;


    public List<string> languageFiles_Path;
    public LanguageCardTextList languageCardTexts_List;
    public List<LanguageCardText> languageCardTexts;

    // Update is called once per frame
    void Awake(){
        init = this;
        loadCardDBFile(0);
    }   

    public void loadCardDBFile(int language){
        string fileDist = languageFiles_Path[language];

            // JSON 파일 읽기
        string jsonString = File.ReadAllText(fileDist);

            // JSON 문자열을 객체로 역직렬화
        languageCardTexts_List = JsonUtility.FromJson<LanguageCardTextList>(jsonString);

        // languageCardTexts.Add(languageCardTexts_List.cardList[0]);
        for(int i=0;i<languageCardTexts_List.cardList.Count;i++){
            LanguageCardText langT = languageCardTexts_List.cardList[i];
            while(languageCardTexts.Count != langT.Id){
                languageCardTexts.Add(null);
            }
            languageCardTexts.Add(langT);
        }
    }

    public string GetName(int id){
        return languageCardTexts[id].Name;
    }

    public string GetFlavor(int id){
        return languageCardTexts[id].Flavor;
    }

    public string GetAbili(int id){
        return languageCardTexts[id].Abili;
    }
}


