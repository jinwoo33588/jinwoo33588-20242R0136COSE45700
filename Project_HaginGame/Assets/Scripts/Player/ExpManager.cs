using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using GameSave;


public class ExpManager : MonoBehaviour
{
    [Header("경험치 바")]
    [SerializeField] private Image mExpCurrentBar;

    /// <summary>
    /// 플레이어의 현재 경험치
    /// </summary>
    public float ExpCurrent { private set; get; }

    /// <summary>
    /// 플레이어의 현재 최대 경험치
    /// </summary>
    public float ExpMax { private set; get; } = 100;

    private Coroutine? mCoUpdateExpBarFill;

    public void AddExp(float amount)
    {
        float expPrev = ExpCurrent;
        ExpCurrent += amount;

        if (mCoUpdateExpBarFill is not null)
            StopCoroutine(mCoUpdateExpBarFill);
        mCoUpdateExpBarFill = StartCoroutine(CoUpdateExpBarFill(expPrev));
    }

    /// <summary>
    /// 세이브파일로부터 로드된경우 현재EXP와 최대EXP를 설정
    /// </summary>
    public void LoadFromData(PlayerStat playerStat)
    {
        this.ExpMax = playerStat.expMax;
        this.ExpCurrent = playerStat.expCurrent;

        mExpCurrentBar.fillAmount = this.ExpCurrent / this.ExpMax;
    }

    private IEnumerator CoUpdateExpBarFill(float expPrev)
    {
        float process = 0f;

        while (process < 1f)
        {
            process += Time.deltaTime;

            expPrev = Mathf.Lerp(expPrev, ExpCurrent, process);
            mExpCurrentBar.fillAmount = expPrev / ExpMax;

            if (expPrev / ExpMax > 1f)
            {
                expPrev = 0f;
                process = 0f;
                ExpCurrent -= ExpMax;
                ExpMax *= 2.0f;

                //StatManager.Instance.LevelUp(); //스탯매니저 레벨업 호출
            }

            yield return null;
        }
    }
}
