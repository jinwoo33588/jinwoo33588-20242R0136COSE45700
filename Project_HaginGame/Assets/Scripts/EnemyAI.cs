using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;                // 플레이어의 Transform
    public float speed = 3f;                // 적 이동 속도
    public float predictionDistance = 2f;  // 플레이어 예측 위치 거리
    public float minSeparationDistance = 1f; // 적 간 최소 거리
    public Vector2 boundaryX = new Vector2(-25f, 25f); // 제한된 공간 X축
    public Vector2 boundaryZ = new Vector2(-15f, 15f); // 제한된 공간 Z축

    [Header("StaminaSetting")]
    public float maxStamina = 10f;
    public float staminaRecoveryRate = 1f;
    public float staminaConsumptionRate = 2f;
    public float lowStaminaSpeedMultiplier = 0.5f;

    private float currentStamina;
    private bool isTired = false;

    protected Animator animator;
    private bool isMove = false;


    private Transform otherEnemy;           // 다른 적의 Transform

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        isMove = true;
        animator.SetBool("isMove", isMove);
        // 다른 적 찾기
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        foreach (var enemy in enemies)
        {
            if (enemy != this) // 본인 제외
            {
                otherEnemy = enemy.transform;
                break;
            }
        }
    }

    void Update()
    {
        if (isTired)
        {
            RecoverStamina(); // 피로 상태에서 스태미너 회복
        }
        else
        {
             // 적 1과 적 2 구분 (이름 기반 또는 태그 활용)
            if (gameObject.name == "Enemy1")
            {
                ChasePlayerDirectly();
            }   
            else if (gameObject.name == "Enemy2")
            {
                ChasePlayerWithPrediction();
            }
            ConsumeStamina();
        }
        // 적 1과 적 2 구분 (이름 기반 또는 태그 활용)
        if (gameObject.name == "Enemy1")
        {
            ChasePlayerDirectly();
        }
        else if (gameObject.name == "Enemy2")
        {
            ChasePlayerWithPrediction();
        }

        // 제한된 공간 내에서 이동 제한
        ConstrainWithinBoundaries();
    }

    // 적 1: 플레이어를 직접 추적
    private void ChasePlayerDirectly()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        // 회전 업데이트
        RotateTowards(direction);

        MaintainSeparation(); // 적 간 거리 유지
        
    }

    // 적 2: 플레이어의 이동 방향 예측 및 추적
    private void ChasePlayerWithPrediction()
    {
        // 플레이어의 예측 위치 계산
        Vector3 futurePosition = player.position + player.forward * predictionDistance;
        Vector3 direction = (futurePosition - transform.position).normalized;
        // 회전 업데이트
        RotateTowards(direction);

        // 적 간 거리 유지
        MaintainSeparation();

        // 이동
        transform.position += direction * speed * Time.deltaTime;
        
    }

    // 적 간 최소 거리 유지
    private void MaintainSeparation()
    {
        if (otherEnemy == null) return;

        float distance = Vector3.Distance(transform.position, otherEnemy.position);
        if (distance < minSeparationDistance)
        {
            // 적에게서 멀어지는 방향으로 이동
            Vector3 separationDirection = (transform.position - otherEnemy.position).normalized;
            transform.position += separationDirection * (speed * 0.5f) * Time.deltaTime;
        }
    }
    // 이동 방향으로 회전
    private void RotateTowards(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0.01f) // 방향 벡터가 유효한지 확인
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // 부드럽게 회전
        }
    }

    // 제한된 공간 내에서 위치를 Clamp
    private void ConstrainWithinBoundaries()
    {
        float clampedX = Mathf.Clamp(transform.position.x, boundaryX.x, boundaryX.y);
        float clampedZ = Mathf.Clamp(transform.position.z, boundaryZ.x, boundaryZ.y);
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
    private void ConsumeStamina()
    {
        currentStamina -= staminaConsumptionRate * Time.deltaTime;
        if (currentStamina <= 0)
        {
            currentStamina = 0;
            isTired = true;
        }
    }
    private void RecoverStamina()
    {
        currentStamina += staminaRecoveryRate * Time.deltaTime;
        if (currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
            isTired = false;
        }
    }

    // 디버깅을 위한 시각화
    void OnDrawGizmos()
    {
        if (player == null) return;

        // 플레이어와 적 간의 추적 경로
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, player.position);

        // 적 2의 예측 위치
        if (gameObject.name == "Enemy2")
        {
            Vector3 futurePosition = player.position + player.forward * predictionDistance;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, futurePosition);
        }

        // 제한된 공간
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(boundaryX.y - boundaryX.x, 0, boundaryZ.y - boundaryZ.x));
    }
}


