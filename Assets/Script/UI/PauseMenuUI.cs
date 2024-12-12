using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject pausePanel; // UI 패널 연결

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused); // 패널 활성화/비활성화
        Time.timeScale = isPaused ? 0 : 1; // 게임 일시정지
    }

    public void QuitToLobby()
    {
        Debug.Log("QuitToLobby called!");
        Time.timeScale = 1; // 시간 정상화
        SceneManager.LoadScene("LobbyScene"); // 로비 씬으로 이동
    }
}
