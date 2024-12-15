using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private UnitBase unit;

    // 기존 생성자 (유지)
    public IdleState(UnitBase unit)
    {
        this.unit = unit;
    }

    // 기본 생성자 추가
    public IdleState() { }
    private bool hasEntered;
    public void Enter()
    {
        if (!hasEntered)
        {
            Debug.Log("Entering IdleState");
            hasEntered = true;
        }
        
        
    }

    public void Execute()
    {
       //Debug.Log("Executing Idle State");
    }

    public void Exit() 
    {
        Debug.Log("Exiting IdleState");
        hasEntered = false;
    }
}

