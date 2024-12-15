using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExampleUI : MonoBehaviour
{
    public TMP_Text staminaText;
    public TMP_Text speedText;
    public PlayerStats inGameData;
    // Start is called before the first frame update
    void Start()
    {
        //inGameData = GameDataManager.Instance.playerstats;
    }
    void Update()
    {
         // 매 프레임마다 UI 갱신
        if (inGameData != null)
        {
            UpdateUI();
        }
    }
    public void UpdateUI()
    {
        staminaText.text = "Stamina: " + inGameData.Stamina;
        speedText.text = "Speed: " + inGameData.Speed;

        
    }
}
