using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    public static CollisionHandler sharedInstance;

    [SerializeField] float damage = 10.0f;
    [SerializeField] float enemyLife = 100;
    [SerializeField] int enemyExp;

    private int enemyMaxLife = 100;

    private void Awake()
    {
        sharedInstance = this;
    }
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

    public float GetEnemyMaxLife()
    {
        return this.enemyMaxLife;
    }


}
