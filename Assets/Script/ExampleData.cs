using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExampleData : MonoBehaviour
{
    public PlayerData playerData;

    [ContextMenu("To Json Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명령어가 생성됨
    void SavePlayerDataToJson()
    {
        // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다  
        string jsonData = JsonUtility.ToJson(playerData,true);
        // 데이터를 저장할 경로 지정
        string path = Path.Combine(Application.dataPath, "playerData.json");
        // 파일 생성 및 저장
        File.WriteAllText(path, jsonData);

    }
    [ContextMenu("From Json Data")]
    void LoadPlayerDataFromJson()
    {
    // 데이터를 불러올 경로 지정
        string path = Path.Combine(Application.dataPath, "playerData.json");
    // 파일의 텍스트를 string으로 저장
        string jsonData = File.ReadAllText(path);
        // 이 Json데이터를 역직렬화하여 playerData에 넣어줌
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);
    }
    
}
[System.Serializable]
public class PlayerData
{
    public string name;
    public int age;
    public int level;
    public bool isDead;
    public string[] items;
}
