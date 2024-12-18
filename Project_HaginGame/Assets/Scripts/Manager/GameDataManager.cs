using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class UnitStats
{
    public float stamina;
    public float maxStamina;
    public float staminaRecoveryRate;
    public float speed;
    public float runSpeed;
}

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    private string playerStatsPath;
    private string enemyStatsPath;

    public UnitStats playerStats { get; private set; }
    public UnitStats enemyStats { get; private set; }
    private UnitStats initialPlayerStats;
    private UnitStats initialEnemyStats;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerStatsPath = Path.Combine(Application.persistentDataPath, "PlayerStats.json");
        enemyStatsPath = Path.Combine(Application.persistentDataPath, "EnemyStats.json");

        LoadStats();
    }

     [ContextMenu("From Json Data")]
    private void LoadStats()
    {
        // Load Player Stats
        if (File.Exists(playerStatsPath))
        {
            string json = File.ReadAllText(playerStatsPath);
            playerStats = JsonUtility.FromJson<UnitStats>(json);
            initialPlayerStats = JsonUtility.FromJson<UnitStats>(json);
        }

        // Load Enemy Stats
        if (File.Exists(enemyStatsPath))
        {
            string json = File.ReadAllText(enemyStatsPath);
            enemyStats = JsonUtility.FromJson<UnitStats>(json);
            initialEnemyStats = JsonUtility.FromJson<UnitStats>(json);
        }
    }

    [ContextMenu("To Json Data")]
    public void ResetStats()
    {
        playerStats = JsonUtility.FromJson<UnitStats>(JsonUtility.ToJson(initialPlayerStats));
        enemyStats = JsonUtility.FromJson<UnitStats>(JsonUtility.ToJson(initialEnemyStats));
    }

    public void SaveStats()
    {
        File.WriteAllText(playerStatsPath, JsonUtility.ToJson(playerStats));
        File.WriteAllText(enemyStatsPath, JsonUtility.ToJson(enemyStats));
    }
}