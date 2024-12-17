using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text countdownText;
	private float startTime;

    private bool isGamePaused = false;
    private void Start()
    {
    	if(countdownText != null)
        {
        	Time.timeScale = 0f; //게임 정지
            StartCoroutine(StartGame());
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
    public void EndGame()
    {
        Debug.Log("Game Over!");
        GameDataManager.Instance.ResetStats();
        GameDataManager.Instance.SaveStats();
        SceneManager.LoadScene("MainMenu");
    }

    // 게임 재시작
    public void RestartGame()
    {
        Debug.Log("Restarting Game...");
        GameDataManager.Instance.ResetStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 일시정지 및 재개
    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            Debug.Log("Game Paused");
        }
        else
        {
            Time.timeScale = 1f;
            Debug.Log("Game Resumed");
        }
    }

    // 게임 종료
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
     private IEnumerator StartGame()
    {
        countdownText.text = "3";
        startTime = Time.realtimeSinceStartup;
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
}
