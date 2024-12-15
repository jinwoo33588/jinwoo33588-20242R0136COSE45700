using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public PlayerStats playerStats;
    private string filePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadPlayerStats();
    }

    [ContextMenu("From Json Data")]
    public void LoadPlayerStats()
    {
        filePath = Path.Combine(Application.dataPath, "PlayerStats.json");
        if (File.Exists(filePath))
        {
            //filePath = Path.Combine(Application.dataPath, "LobbyData.json"); //persistentDataPath
            string jsonData = File.ReadAllText(filePath);
            PlayerStatsData data = JsonUtility.FromJson<PlayerStatsData>(jsonData);
            playerStats.InitializePlayerStats(data.Life, data.Stamina, data.StaminaRecoveryRate, data.Speed, data.Level, data.Exp);
            Debug.Log("Player stats loaded from JSON");
            
        }
        else
        {
            
            Debug.LogWarning("로비 데이터 파일이 없어 기본값으로 초기화합니다.");
            playerStats.InitializePlayerStats(3,10f,10f,10f,0,100);
            
        }
    }

    [ContextMenu("To Json Data")]
    public void SavePlayerStats()
    {
        PlayerStatsData data = new PlayerStatsData
        {
            Life = playerStats.Life,
            Stamina = playerStats.Stamina,
            StaminaRecoveryRate = playerStats.StaminaRecoveryRate,
            Speed = playerStats.Speed,
            Level = playerStats.Level,
            Exp = playerStats. Exp
        }; 
        string jsonData = JsonUtility.ToJson(data, true);
        filePath = Path.Combine(Application.dataPath, "PlayerStats.json"); //persistentDataPath
        File.WriteAllText(filePath, jsonData);
        Debug.Log("로비 데이터 저장: " + filePath);
    }
     public void UpdatePlayerStats(int lifeChange, float staminaChange,float staminaRecoveryRateChange, float speedChange, int levelChange, int expChange)
    {
        // 실제 PlayerStats 값 변경
        playerStats.Life += lifeChange;
        playerStats.Stamina += staminaChange;
        playerStats.Speed += speedChange;
        playerStats.Level += levelChange;
        playerStats.Exp += expChange;

        // 값 변경 후, UI 갱신
        playerStats.TriggerStatsChanged();
    }
}

// JSON 데이터 구조
[System.Serializable]
public class PlayerStatsData
{
    public int Life;
    public float Stamina;
    public float StaminaRecoveryRate;
    public float Speed;

    public int Level;
    public int Exp;
}
