using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerColliderController : MonoBehaviour
{
    // Start is called before the first frame update

    // PLAYER HIT
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EnemyController enemyController = gameObject.GetComponentInParent<EnemyController>();
            if (enemyController.isAttacking)
            {
                enemyController.FinishAttack();
                other.GetComponent<PlayerController>().HitByEnemy(
                    enemyController.target.normalized * enemyController.attackSpeed,
                    enemyController.damage);
            }
        }
    }

    // ENEMY HIT
    // Called from weapon when it intersects collider
    public void HitByPlayer(Vector3 knockbackDir, int damage)
    {
        gameObject.GetComponentInParent<EnemyController>().HitByPlayer(knockbackDir, damage);
    }
}
