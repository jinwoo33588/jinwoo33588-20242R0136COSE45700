using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState currentState;
    public IState CurrentState => currentState;

    // 상태 전환
    public void ChangeState(IState newState)
    {
        if (currentState == newState) return;
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    // 현재 상태 업데이트
    public void UpdateState()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }
}

