using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance;
    [SerializeField]
    private PlayerStats playerData;

     private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadPlayerData();
        UpdateUI();
    }

    public void UpdatePlayerData(int life, float stamina, float staminaRecoveryRate, float speed, int gold)
    {
        playerData.Life = life;
        playerData.Stamina = stamina;
        playerData.StaminaRecoveryRate = staminaRecoveryRate;
        playerData.Speed = speed;
        
    }

    

    private void UpdateUI()
    {
       // UIManager.Instance.UpdateLobbyUI(playerData);
    }

    public void SavePlayerData()
    {
        GameDataManager.Instance.SavePlayerStats();
    }

    private void LoadPlayerData()
    {
        GameDataManager.Instance.LoadPlayerStats();
    }

    public void OnUpgradeStat()
    {


    }
}

