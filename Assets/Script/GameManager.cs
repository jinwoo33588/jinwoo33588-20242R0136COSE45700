using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private void Start()
    {
        // PlayerDataManager 초기화 및 인게임 데이터 설정
        PlayerDataManager.Instance.InitializeInGameData();
        Debug.Log("게임 시작: 인게임 데이터 초기화 완료");
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
            
        else
            Destroy(gameObject);
    }

    public void GameOver()
    {
        Debug.Log("게임 종료");
        PlayerDataManager.Instance.FinalizeGameData();
        // 로비로 돌아가기
        UnityEngine.SceneManagement.SceneManager.LoadScene("LobbyScene");
    }
}
