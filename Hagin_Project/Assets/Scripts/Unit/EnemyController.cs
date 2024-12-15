using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : UnitBase
{
    public Transform TargetPlayer;
    public float enemySpeed = 5.0f;
    

    public void ChasePlayer(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * enemySpeed * Time.deltaTime;
        transform.forward = direction;
    }

    public override void Update()
    {
        base.Update();
        if (TargetPlayer != null)
        {
            ChasePlayer(TargetPlayer);
        }
    }
}


