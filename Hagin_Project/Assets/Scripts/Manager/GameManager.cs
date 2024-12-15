using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameDataManager gameDataManager;
    public UIManager uiManager;
    public PlayerController playerController;
    public PlayerStats inGamePlayerStats;
    

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        gameDataManager.playerStats = inGamePlayerStats;
        playerController.playerStats = inGamePlayerStats;
        uiManager.playerStats = inGamePlayerStats;

        GameDataManager.Instance.LoadPlayerStats();

        //var playerController = FindObjectOfType<PlayerController>();
        //var uiManager = FindObjectOfType<UIManager>();
        
        //playerController.playerStats = inGamePlayerStats;
        //uiManager.playerStats = inGamePlayerStats;

        Debug.Log("Game Started with Player Data: " + JsonUtility.ToJson(inGamePlayerStats));
        // Initialize player, enemies, and game world using playerData
        //player.Initialize(playerData);
    }
    

    public void GameOver()
    {
       // playerController.SaveStats();
        Debug.Log("Game Over");
        //게임 오버 UI 활성화 및 로직 추가하기
         //UIManager.Instance.ShowGameOverScreen();
         // 골드 제외한 데이터 초기화 ****여긴 다시 작성하기****
        
        
       
        
        //GameDataManager.Instance.SavePlayerData(playerData);
    }
    
}


