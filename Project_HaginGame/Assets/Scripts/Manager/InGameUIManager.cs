using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public Slider staminaBar; // Unity Inspector에서 설정
    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (player == null)
            Debug.LogError("PlayerController not found!");
    }

    private void Awake()
    {
        if (player != null)
        {
            UpdateStaminaBar();
        }
    }

    private void UpdateStaminaBar()
    {
        staminaBar.maxValue = player.maxStamina;
        staminaBar.value = player.stamina;
    }
}
