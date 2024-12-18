using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance;

     [Header("UI Panels")]
    public GameObject gameOverUI;  // GameOver UI 연결
    public TMP_Text countdownText; // 카운트다운 UI 연결
    public GameObject pauseMenuCanvas; // Pause 메뉴 Canvas
    
    public GameObject GameUI;



    [Header("Component")]
    public PlayerController player;
    public TMP_Text playTimeText;
    public TMP_Text staminaText;
    public RectTransform playerStaminaGroup;
    public RectTransform playerStaminaBar;
    public GameObject heartPrefab;        // 하트 프리팹
    public Transform heartContainer;     // 하트가 배치될 부모 오브젝트
    private Stack<GameObject> hearts = new Stack<GameObject>(); // 생성된 하트 리스트





    public float playTime;
    //int hour;
    
    
    //private PlayerController player;
    private bool isGamePaused = false;

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
    }
    private void Start()
    {
        playTime = 0f;
        player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("PlayerController not found!");
        }
       

        // UI 초기화
        if (pauseMenuCanvas != null) pauseMenuCanvas.SetActive(false);
        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (countdownText != null) countdownText.gameObject.SetActive(false);

        GameUI.gameObject.SetActive(true);
        AddHeart();
        Debug.Log("Life:" + player.Life);
    }
    void Update()
    {
        if(isGamePaused == false)
        {
            playTime += Time.deltaTime * 3600;
        }

        
    
        // ESC 키로 Pause 토글
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    void LateUpdate() 
    {
        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 3600) / 60);
        int sec = (int)(playTime % 60);

        playTimeText.text = string.Format("{0:00}", hour) + ":"+string.Format("{0:00}", min) + ":"+string.Format("{0:00}", sec);

        playerStaminaBar.localScale = new Vector3((float)player.stamina/player.maxStamina,1,1);
        //UpdateHeartUI();
    }

    
    // 하트 추가
    private void AddHeart()
    {
        for (int i = 0; i < player.Life; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartContainer);
            hearts.Push(heart); // 스택에 하트 추가
        }
    }
     // 하트 UI 업데이트// 하트 제거 (오른쪽부터 제거)
    public void RemoveHeart()
    {
        if (hearts.Count > 0)
        {
            GameObject heartToRemove = hearts.Pop(); // 스택에서 하트 제거
            Destroy(heartToRemove);
        }
    }

     public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        countdownText.gameObject.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1);
        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1f; // 게임 시작
    }

    public void PauseGame()
    {
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(true);
        }
        Time.timeScale = 0f;
        isGamePaused = true;
    }
    public void ResumeGame()
    {
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }
        Time.timeScale = 1f;
        isGamePaused = false;
    }


     public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true); // GameOver UI 활성화
        Time.timeScale = 0f;       // 게임 정지
    }

    public void Retry()
    {
        Time.timeScale = 1f; // 게임 속도 정상화
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }
     public void QuitToLobby()
    {
        Debug.Log("Quitting Game...");
        SceneManager.LoadScene("LobbyScene");
    }

    public void GameUIControl()
    {

    }
}
