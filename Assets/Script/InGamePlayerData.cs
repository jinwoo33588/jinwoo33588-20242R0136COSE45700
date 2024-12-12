using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[System.Serializable]
public class InGamePlayerData
{
    public int HP;
    public int Stamina;
    //public int MaxSpeed;
    public int Speed;
    public int Level;
    public int Exp;
    public int Gold; // 게임 중 획득한 골드

    public InGamePlayerData(int hp,  int stamina, int speed)
    {
        
        HP = hp;
        Stamina = stamina;
        Speed = speed;
        Level = 1;
        Exp = 0;
        Gold = 0; // 초기화
    }
}