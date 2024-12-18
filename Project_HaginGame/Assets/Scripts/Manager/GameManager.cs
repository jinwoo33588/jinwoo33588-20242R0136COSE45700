using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool isGamePaused = false;
    private void Start()
    {
    	// 게임 시작 시 카운트다운 요청
        if (InGameUIManager.Instance != null)
        {
            InGameUIManager.Instance.StartCountdown();
        }
    }

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
    }

    // 게임 종료 시 호출
    // 게임 종료 시 호출
    public void GameOver()
    {
        Debug.Log("Game Over!");
        GameDataManager.Instance.ResetStats();
        GameDataManager.Instance.SaveStats();
        InGameUIManager.Instance.ShowGameOverUI();
        
    }


    // 게임 재시작
    public void RestartGame()
    {
        Debug.Log("Restarting Game...");
        GameDataManager.Instance.ResetStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 게임 종료
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        SceneManager.LoadScene("LobbyScene");
    }
 
}
