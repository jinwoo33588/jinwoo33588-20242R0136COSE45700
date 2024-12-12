using UnityEngine;

public class Gem : MonoBehaviour
{
    public int expValue = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerData = PlayerDataManager.Instance.inGameData;
            playerData.Exp += expValue;

            int requiredExp = playerData.Level * 100;
            if (playerData.Exp >= requiredExp)
            {
                playerData.Exp -= requiredExp;
                playerData.Level++;
                Debug.Log("레벨업! 새로운 레벨: " + playerData.Level);

                UpgradeStat();
            }

            Destroy(gameObject);
        }
    }

    private void UpgradeStat()
    {
        PlayerDataManager.Instance.inGameData.HP += 10;
        Debug.Log("스탯 업그레이드 완료: HP 증가");
    }
}
