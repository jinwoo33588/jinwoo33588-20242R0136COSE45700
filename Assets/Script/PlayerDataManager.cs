using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;
    

    public LobbyPlayerData lobbyData;
    public InGamePlayerData inGameData;

    private string filePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //filePath = Path.Combine(Application.dataPath, "LobbyData.json"); //persistentDataPath
        LoadLobbyData();
    }

    [ContextMenu("To Json Data")]
    public void SaveLobbyData()
    {
        string jsonData = JsonUtility.ToJson(lobbyData, true);
        filePath = Path.Combine(Application.dataPath, "LobbyData.json"); //persistentDataPath
        File.WriteAllText(filePath, jsonData);
        Debug.Log("로비 데이터 저장: " + filePath);
    }

    [ContextMenu("From Json Data")]
    public void LoadLobbyData()
    {
        filePath = Path.Combine(Application.dataPath, "LobbyData.json"); //persistentDataPath
        if (File.Exists(filePath))
        {
            //filePath = Path.Combine(Application.dataPath, "LobbyData.json"); //persistentDataPath
            string jsonData = File.ReadAllText(filePath);
            lobbyData = JsonUtility.FromJson<LobbyPlayerData>(jsonData);
            Debug.Log("로비 데이터 로드: " + filePath);
        }
        else
        {
            lobbyData = new LobbyPlayerData();
            Debug.LogWarning("로비 데이터 파일이 없어 기본값으로 초기화합니다.");
        }
    }

    // In-Game 데이터 초기화
    public void InitializeInGameData()
    {
        if (inGameData == null)
        {
            inGameData = new InGamePlayerData(lobbyData.HP, lobbyData.Stamina, lobbyData.Speed);
            Debug.Log("InGamePlayerData 초기화 완료");
        }
        else
        {
            Debug.LogWarning("InGamePlayerData가 이미 초기화되어 있습니다.");
        }
        
    }
    public void FinalizeGameData()
    {
        // 게임 중 획득한 골드를 로비 데이터에 추가
        lobbyData.Gold += inGameData.Gold;

        // 로비 데이터 저장
        //SaveLobbyData();

        // 인게임 데이터 초기화
        ResetInGameData();
    }


    //게임 종류 후 로비로 돌아올때 초기화
    public void ResetInGameData()
    {
        inGameData = null;  // 게임 데이터 초기화
        Debug.Log("게임 데이터 초기화 완료");
    }
    
}
