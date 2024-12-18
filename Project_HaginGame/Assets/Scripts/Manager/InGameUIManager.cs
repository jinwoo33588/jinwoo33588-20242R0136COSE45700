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
 
    private PlayerController player;
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
        player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("PlayerController not found!");
        }

        // UI 초기화
        if (pauseMenuCanvas != null) pauseMenuCanvas.SetActive(false);
        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (countdownText != null) countdownText.gameObject.SetActive(false);
    }
    private void Update()
    {
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
        GameManager.Instance.QuitGame(); // GameManager 통해 로비 이동
    }

   
}
