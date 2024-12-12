using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InGamePlayerData playerData;

    private float currentSpeed;
    private float stamina;

    private void Start()
    {
        playerData = PlayerDataManager.Instance.inGameData;
        currentSpeed = PlayerDataManager.Instance.inGameData.Speed;
        //currentSpeed = playerData.Speed;
        stamina = playerData.Stamina;
        //
        
        
    }
    private void OnDestroy()
    {
       
    }

    private void Update()
    {
        HandleMovement();
        HandleStamina();

        if (playerData.HP <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ) * currentSpeed * Time.deltaTime;
        transform.Translate(move);
    }

    private void HandleStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            stamina -= Time.deltaTime * 10;
        }
        else
        {
            stamina = Mathf.Min(stamina + Time.deltaTime * 5, playerData.Stamina);
        }

    }

    private void HandleDeath()
    {
        Debug.Log("플레이어 사망");
        GameManager.Instance.GameOver();
    }
    private void UpdatePlayerData()
    {
        // 데이터가 변경되었을 때 속성 재설정
        playerData = PlayerDataManager.Instance.inGameData;
        currentSpeed = playerData.Speed;
        stamina = Mathf.Min(stamina, playerData.Stamina); // 스태미너를 현재 최대치에 맞춤
    }
}
