using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    private UnitBase unit;

    // 기존 생성자 (유지)
    public RunState(UnitBase unit)
    {
        this.unit = unit;
    }

    // 기본 생성자 추가
    public RunState() { }
    private bool hasEntered;
    public void Enter()
    {
        if (!hasEntered)
        {
            Debug.Log("Entered Run State");
            hasEntered = true;
        }
        
    }

    public void Execute()
    {
        //Debug.Log("Executing Run State");
    }

    public void Exit() 
    { 
        Debug.Log("Exiting Run State");
        hasEntered = false;
    }
}

