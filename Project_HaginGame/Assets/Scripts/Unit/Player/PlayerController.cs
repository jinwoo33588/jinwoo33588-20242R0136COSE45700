using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitBase
{
    [Header("Player Settings")]
    public int Life = 3;
    public float dashSpeed = 12f;
    public float dashDuration = 0.3f;
    public float rotationSpeed = 10f; 
    private bool isDashing = false;

    public bool gameOver = false;


    void Awake()
    {
        //Playerprefs.SetInt("MaxScore", 122230);
    }
    protected override void Start()
    {
        base.Start();
        // 스탯 불러오기
        var stats = GameDataManager.Instance.playerStats;
        stamina = stats.stamina;
        maxStamina = stats.maxStamina;
        staminaRecoveryRate = stats.staminaRecoveryRate;
        
        if (animator == null)
        {
            Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다. Player 오브젝트에 Animator를 추가하세요.");
        }
        
    }

    protected override void Update()
    {
        base.Update();

        if (!isDashing)
        {
            HandleMovement();
            HandleDash();
        }
    }

    // WASD 키보드 이동 및 Run 상태 관리
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (movementDirection.magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
            {
                SwitchState(UnitState.Run);
            }
            else
            {
                SwitchState(UnitState.Walk);
            }

            transform.position += movementDirection * currentSpeed * Time.deltaTime;
            transform.LookAt(transform.position + movementDirection);
        }
        else
        {
            SwitchState(UnitState.Idle);
        }
    }

    // Dash 스킬 구현
    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && stamina > 10)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private System.Collections.IEnumerator DashCoroutine()
    {
        isDashing = true;
        float dashStartTime = Time.time;

        Vector3 dashDirection = transform.forward;

        while (Time.time < dashStartTime + dashDuration)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        stamina -= 20; // Dash 사용 시 스태미나 소모 // 쿨타임 추가하기
    }

    // 현재 스태미나 반환
    public float GetCurrentStamina()
    {
        return stamina;
    }

    // 최대 스태미나 반환
    public float GetMaxStamina()
    {
        return maxStamina;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&& Life > 0 )
        {
            Life --;
            InGameUIManager.Instance.RemoveHeart();
        }
        if(Life<=0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Player has died!");

        // 게임 종료 처리 (예: 메인 메뉴로 이동)
        GameManager.Instance.GameOver();
    }

}
