using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : UnitBase 
{
    public PlayerStats playerStats;
    public int life;
    public float stamina;
    public float staminaRecoveryRate;
    public float speed;

    private void Start()
    {
        LoadPlayerStats();
        ApplyStats();
    }
    private void LoadPlayerStats()
    {
        GameDataManager.Instance.LoadPlayerStats();
    }
    private void ApplyStats()
    {
        life = playerStats.Life;
        stamina = playerStats.Stamina;
        speed = playerStats.Speed;
        staminaRecoveryRate = playerStats.StaminaRecoveryRate;
    }

    private void Update()
    {
        //base.Update();
        HandleMovement();
        //RecoverStamina();
        //AddSpeed(1);
        UseStamina();
        //Debug.Log($"PlayerController - Stamina: {playerStats.Stamina}");
    }
    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;
        transform.Translate(move);
        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        /*if (move.magnitude > 0.1f)
        {
            StateMachine.ChangeState(new WalkState());
            if (Input.GetKey(KeyCode.LeftShift) && Stamina > 0)
            {
                move *= RunSpeedMultiplier;
                StateMachine.ChangeState(new RunState());
                Stamina -= Time.deltaTime;
            }
        }
        else
        {
            StateMachine.ChangeState(new IdleState());
        }

        characterController.Move(move * MovementSpeed * Time.deltaTime);
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }*/
    }
    public void AddSpeed(int InAddSpeed)
    {
        speed += InAddSpeed;
    }
    
    private void RecoverStamina()
    {
        if (StateMachine.CurrentState is IdleState || StateMachine.CurrentState is WalkState)
        {
            stamina = Mathf.Min(stamina + staminaRecoveryRate * Time.deltaTime, 100);
        }
    }
    private void UseStamina()
    {
        if(Input.GetKey(KeyCode.R))
        {
            stamina -= 10f;
            Debug.Log("PlayerController - Stamina used");
        }
    }
    public void SaveStats()
    {
        // 변경된 스탯 저장
        playerStats.Speed = speed;
        playerStats.Stamina = stamina;

        GameDataManager.Instance.SavePlayerStats();
    }
    public void ClearStats()
    {
        playerStats = null;  // 플레이어 데이터 삭제
        Debug.Log("Player stats cleared.");
    }
}
