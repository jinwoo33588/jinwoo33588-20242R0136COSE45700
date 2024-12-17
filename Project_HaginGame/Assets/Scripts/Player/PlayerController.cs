using UnityEngine;

public class PlayerController : UnitBase
{
    [Header("Player Settings")]
    public float dashSpeed = 12f;
    public float dashDuration = 0.3f;
    public float rotationSpeed = 10f; 
    private bool isDashing = false;

    //private CharacterController controller;
    //public Camera mainCamera;

    protected override void Start()
    {
        base.Start();
        // 스탯 불러오기
        var stats = GameDataManager.Instance.playerStats;
        stamina = stats.stamina;
        maxStamina = stats.maxStamina;
        staminaRecoveryRate = stats.staminaRecoveryRate;
        walkSpeed = stats.speed;
        runSpeed = stats.runSpeed;
        //currentSpeed = walkSpeed;
        //controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        //mainCamera = Camera.main;
        
        
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

            /*
            // 카메라 기준으로 이동 방향 계산
            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 cameraRight = mainCamera.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;

            Vector3 moveDirection = (cameraForward * movementDirection.z + cameraRight * movementDirection.x).normalized;

            // 플레이어 이동
            transform.position += moveDirection * currentSpeed * Time.deltaTime;

            // 부드러운 방향 회전
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            */
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
        stamina -= 20; // Dash 사용 시 스태미나 소모
    }
}
