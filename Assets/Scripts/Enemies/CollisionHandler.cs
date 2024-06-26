using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float damage = 10.0f;
    [SerializeField] float enemyLife = 100;
    [SerializeField] int enemyExp;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerAttack") && PlayerController.sharedInstance.GetAttackCollider())
        {
            SetEnemyLife(LevelSystem.sharedInstance.GetCurrentPlayerDamage());

            if (this.enemyLife <= 0)
            {
                LevelSystem.sharedInstance.calculateExp(this.enemyExp);
                Destroy(gameObject);
            }
            else
            Debug.Log($"VidaRestante {this.enemyLife}");
        }


        if (col.gameObject.CompareTag("NoPlayerAttack") && PlayerController.sharedInstance.GetIsAttacking() == false
            && PlayerController.sharedInstance.IsAlive())
            PlayerController.sharedInstance.EnemyKnockBack(transform.position.x, damage);
    }

    public float GetEnemyLife()
    {
        return this.enemyLife;
    }

    public void SetEnemyLife(float damage)
    {
        this.enemyLife -= damage;
    }
}
