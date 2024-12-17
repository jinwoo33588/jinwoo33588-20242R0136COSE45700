using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuUI : MonoBehaviour
{
    public GameObject MainMenuCanvas;
    // Start is called before the first frame update
    public void StartGame(){
        Debug.Log("아직 미구현입니다...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

}

