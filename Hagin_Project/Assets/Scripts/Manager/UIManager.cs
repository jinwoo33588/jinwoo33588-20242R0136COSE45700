using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public PlayerStats playerStats;

    [SerializeField]
    private TMPro.TextMeshProUGUI lifeText;
    [SerializeField]
    private TMPro.TextMeshProUGUI staminaText;
    [SerializeField]
    private TMPro.TextMeshProUGUI speedText;
    [SerializeField]
    private TMPro.TextMeshProUGUI levelText;
    [SerializeField]
    private TMPro.TextMeshProUGUI expText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        if (playerStats != null)
        {
            playerStats.OnStatsChanged += UpdateUI;  // 이벤트 구독
        }
    }

    private void OnDestroy()
    {
        if (playerStats != null)
        {
            playerStats.OnStatsChanged -= UpdateUI;  // 이벤트 구독 해제
        }
    }
    private void UpdateUI()
    {
        if (playerStats != null)
        {
            lifeText.text = $"Life: {playerStats.Life}";
            staminaText.text = $"Stamina: {playerStats.Stamina}";
            speedText.text = $"Speed: {playerStats.Speed}";
            levelText.text = $"Level: {playerStats.Level}";
            expText.text = $"Exp: {playerStats.Exp}";
        }


        //Debug.Log($"UIManager - Stamina: {playerStats.Stamina}");
    }
    private void Update()
    {
        // 매 프레임마다 UI 갱신
        // 기본적으로 이벤트 시스템에 의해 업데이트 되지만, 필요시 호출
        UpdateUI();
    }
    /*public void UpdateLobbyUI(PlayerStats playerstats)
    {
        lifeText.text = $"Life: {playerstats.Life}";
        staminaText.text = $"Stamina: {playerstats.Stamina}";
    }
    */

    /*
    public void UpdateGameUI(PlayerController player)
    {
        lifeText.text = $"Life: {player.Life}";
        staminaText.text = $"Stamina: {player.Stamina}";
    }
    */

    public void ShowGameOverScreen()
    {
        // Game Over UI 활성화
        Debug.Log("Game Over UI Shown");
    }

    public void ShowLevelUpOptions(System.Action<int> onOptionSelected)
    {
        // UI에서 옵션을 선택하는 인터페이스 제공
        Debug.Log("Level Up Options Shown");
        // 예: 속성 1~4 선택 (Stamina, RunSpeed, WalkSpeed, StaminaRecoveryRate)
        onOptionSelected.Invoke(1); // 디버그용 기본 선택
    }

}
