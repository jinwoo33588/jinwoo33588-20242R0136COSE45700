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
    public float staminaRecoveryRate = 5f;  // Idle 상태에서 회복
    public float staminaDecreaseRate = 10f; // Run 상태에서 감소
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    [Header("UI")]
    public Slider staminaSliderPrefab;  // 스태미나 슬라이더 프리팹
    public Vector3 sliderOffset = new Vector3(0, 2, 0); // 슬라이더 위치 오프셋
    private Image fillImage;            // 슬라이더 Fill 이미지 참조

    private Slider staminaSlider;
    private Camera mainCamera;

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
        // 스태미나 슬라이더 생성 및 설정
        mainCamera = Camera.main;
        if (staminaSliderPrefab != null)
        {
            staminaSlider = Instantiate(staminaSliderPrefab, FindObjectOfType<Canvas>().transform);
            staminaSlider.maxValue = maxStamina;
            UpdateStaminaSlider();
        }

    }


    // 상태 업데이트
    protected virtual void Update()
    {
        UpdateState();
        HandleStamina();
        // 슬라이더 업데이트
        if (staminaSlider != null)
        {
            fillImage = staminaSlider.fillRect.GetComponent<Image>();
            UpdateStaminaSlider();
        }
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
    private void UpdateStaminaSlider()
    {
        // 슬라이더의 위치 업데이트
        Vector3 worldPosition = transform.position + sliderOffset;
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
        staminaSlider.transform.position = screenPosition;

        // 슬라이더 값 업데이트
        staminaSlider.value = stamina;
        // 슬라이더 색상 변경
        UpdateSliderColor();
    }
    private void UpdateSliderColor()
    {
        // 스태미나 비율에 따라 색상 변경
        float staminaPercentage = stamina / maxStamina;

        if (fillImage != null)
        {
            if (staminaPercentage > 0.5f)
            {
                fillImage.color = Color.green; // 녹색
            }
            else if (staminaPercentage > 0.2f)
            {
                fillImage.color = Color.yellow; // 노란색
            }
            else
            {
                fillImage.color = Color.red; // 빨간색
            }
        }
    }
}
