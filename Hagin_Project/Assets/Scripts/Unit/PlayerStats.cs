using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


[CreateAssetMenu(fileName = "PlayerStats", menuName = "Game/PlayerStats", order = 0)]
public class PlayerStats : ScriptableObject 
{
    public int Life;
    public float Stamina;
    public float StaminaRecoveryRate;
    public float Speed;
    public int Level;
    public int Exp;

     public event System.Action OnStatsChanged;
     public void TriggerStatsChanged()
    {
        OnStatsChanged?.Invoke(); // 이벤트 호출
    }

    //public int Gold;
    public void InitializePlayerStats(int life, float stamina, float staminaRecoveryRate, float speed, int level, int exp)
    {
        Life = life;
        Stamina = stamina;
        StaminaRecoveryRate = staminaRecoveryRate;
        Speed = speed;
        Level = level;
        Exp = exp;

        //OnStatsChanged?.Invoke();
    }
}