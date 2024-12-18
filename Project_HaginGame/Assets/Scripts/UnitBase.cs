using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBase : MonoBehaviour
{
    // 유닛의 상태
    public enum UnitState { Idle, Walk, Run }
    protected UnitState currentState = UnitState.Idle;

    // 유닛의 스탯
    [Header("Stats")]
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float staminaRecoveryRate = 1f;  // Idle 상태에서 회복
    public float staminaDecreaseRate = 10f; // Run 상태에서 감소
    public float walkSpeed = 12f;
    public float runSpeed = 18f;

    // 유닛 이동 관련
    protected Vector3 movementDirection;
    protected float currentSpeed;

    // 애니메이션
    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        // 유닛 기본 초기화 로직 (필요시 추가)
        currentSpeed = walkSpeed;
    }


    // 상태 업데이트
    protected virtual void Update()
    {
        UpdateState();
        HandleStamina();
        // 슬라이더 업데이트
        
    }

    // 상태에 따른 로직
    protected virtual void UpdateState()
    {
        switch (currentState)
        {
            case UnitState.Idle:
                currentSpeed = 0;
                break;
            case UnitState.Walk:
                currentSpeed = walkSpeed;
                break;
            case UnitState.Run:
                if (stamina > 0)
                {
                    currentSpeed = runSpeed;
                    stamina -= staminaDecreaseRate * Time.deltaTime;
                }
                else
                {
                    SwitchState(UnitState.Walk); // 스태미나 소진 시 걷기로 전환
                }
                break;
        }
    }

    // 스태미나 회복/감소
    protected virtual void HandleStamina()
    {
        if (currentState == UnitState.Idle  && stamina < maxStamina)
        {
            stamina += staminaRecoveryRate * Time.deltaTime;
        }
        else if(currentState == UnitState.Walk  && stamina < maxStamina)
        {
            stamina += staminaRecoveryRate * Time.deltaTime * 3;
        }
    }

    // 상태 변경
    public void SwitchState(UnitState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            UpdateAnimation();
        }
    }

    // 애니메이션 상태 업데이트
    protected virtual void UpdateAnimation()
    {
        if (animator == null)
        {
            if (animator == null) 
    {
            Debug.LogWarning("Animator가 연결되지 않았습니다.");
            return;
    }
        } 
        Debug.Log($"Current State: {currentState}");
        animator.SetBool("isIdle", currentState == UnitState.Idle);
        animator.SetBool("isWalk", currentState == UnitState.Walk);
        animator.SetBool("isRun", currentState == UnitState.Run);
    }
    

    
}
