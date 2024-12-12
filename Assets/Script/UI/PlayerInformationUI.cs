using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInformationUI : MonoBehaviour
{
    public TMP_Text hpText;
    public TMP_Text staminaText;
    public TMP_Text speedText;
    public TMP_Text levelText;
    public TMP_Text expText;
    //public Slider hpSlider;
    private InGamePlayerData inGameData;
    
    private void Start()
    {
        // InGamePlayerData 초기화
        inGameData = PlayerDataManager.Instance.inGameData;
        UpdateUI();  // 시작 시 UI 초기화
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
        //var playerData = PlayerDataManager.Instance.inGameData;

        hpText.text = "HP: " + inGameData.HP;
        //hpSlider.text = "HP: " + playerData.HP;

        staminaText.text = "Stamina: " + inGameData.Stamina;
        speedText.text = "Speed: " + inGameData.Speed;
        levelText.text = "Level: " + inGameData.Level;
        expText.text = "Exp: " + inGameData.Exp;
    }
}
