using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState
{
    private UnitBase unit;

    // 기존 생성자 (유지)
    public WalkState(UnitBase unit)
    {
        this.unit = unit;
    }

    // 기본 생성자 추가
    public WalkState() { }
    private bool hasEntered;

    public void Enter()
    {
        if(!hasEntered)
        {
            Debug.Log("Entered Walk State");
            hasEntered = true;
        }
        
    }

    public void Execute()
    {
        //Debug.Log("Executing Walk State");
    }

    public void Exit() 
    {
        Debug.Log("Exiting Walk State");
        hasEntered = false;
    }
}


