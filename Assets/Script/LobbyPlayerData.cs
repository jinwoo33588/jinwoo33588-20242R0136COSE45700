using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[System.Serializable]
public class LobbyPlayerData
{
    public int HP;
    public int Stamina;
    public int Speed;
    public int Gold; // 골드 속성 추가
    //public List<string> EquippedItems;

    public LobbyPlayerData()
    {
        HP = 100;
        Stamina = 50;
        Speed = 10;
        Gold = 0; // 초기값 설정
        //EquippedItems = new List<string>();
    }
}