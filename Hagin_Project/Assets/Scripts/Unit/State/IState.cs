using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter(); // 상태 진입 시 호출
    void Execute(); // 매 프레임 호출
    void Exit(); // 상태 종료 시 호출
}
