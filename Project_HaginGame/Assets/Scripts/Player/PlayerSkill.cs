using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float walkSpeed = 3f;                 // 걷기 속도
    public float runSpeed = 6f;                 // 달리기 속도
    public float dashSpeed = 12f;               // 대쉬 속도
    public float dashDuration = 0.5f;           // 대쉬 지속 시간

    [Header("Stamina Settings")]
    public float maxStamina = 10f;              // 최대 스태미너
    public float staminaRecoveryRate = 2f;      // 스태미너 회복 속도
    public float staminaConsumptionRateWalk = 1f; // 걷기 스태미너 소비 속도
    public float staminaConsumptionRateRun = 2f;  // 달리기 스태미너 소비 속도
    public float staminaConsumptionRateDash = 5f; // 대쉬 스태미너 소비 속도

    private float currentStamina;               // 현재 스태미너
    private bool isDashing = false;             // 대쉬 중 여부
    private bool isTired = false;               // 피로 상태 (스태미너 부족)

    private CharacterController characterController;
    private Vector3 velocity;  

    public bool IsDashing => isDashing;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentStamina = maxStamina;
    }

    void Update()
    {
        if (isTired)
        {
            RecoverStamina(); // 피로 상태에서 스태미너 회복
        }
        else
        {
            HandleMovement(); // 이동 및 스태미너 소비
        }
        
    }

    private void HandleMovement()
    {
        float speed = walkSpeed;

        // 대쉬 시작
        if (Input.GetKeyDown(KeyCode.Space) && currentStamina >= staminaConsumptionRateDash)
        {
            StartDash();
        }

        // 대쉬 중
        if (isDashing)
        {
            speed = dashSpeed;
            DashLogic();
        }
        else
        {
            // 달리기 (Shift키)
            if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
            {
                speed = runSpeed;
                ConsumeStamina(staminaConsumptionRateRun);
            }
            else
            {
                // 걷기
                ConsumeStamina(staminaConsumptionRateWalk);
            }
        }

        // 이동 처리
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (input.magnitude > 0 && currentStamina > 0)
        {
            characterController.Move(input * speed * Time.deltaTime);
        }
        else if (currentStamina <= 0) // 스태미너 0이면 이동 불가
        {
            isTired = true;
        }
        // 캐릭터 회전 처리
        //Quaternion targetRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void DashLogic()
    {
        currentStamina -= staminaConsumptionRateDash * Time.deltaTime;
        if (currentStamina <= 0)
        {
            currentStamina = 0;
            EndDash();
            isTired = true;
        }
    }

    private void StartDash()
    {
        isDashing = true;
        Invoke(nameof(EndDash), dashDuration);
    }

    private void EndDash()
    {
        isDashing = false;
    }

    private void ConsumeStamina(float rate)
    {
        currentStamina -= rate * Time.deltaTime;
        if (currentStamina <= 0)
        {
            currentStamina = 0;
            isTired = true;
        }
    }

    private void RecoverStamina()
    {
        if (characterController.velocity.magnitude == 0) // 플레이어가 멈춰있을 때만 회복
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
                isTired = false;
            }
        }
    }

}
