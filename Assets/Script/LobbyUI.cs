using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    public void GameStartButton()
    {
        
            Debug.Log("GameStart!");
            Time.timeScale = 1;
            SceneManager.LoadScene("GameScene");
        
    }
}
