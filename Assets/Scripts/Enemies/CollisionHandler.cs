using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float damage = 10.0f;
    //[SerializeField] float enemyLife;
    [SerializeField] int enemyExp;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerAttack") && PlayerController.sharedInstance.GetAttackCollider())
        {
            //LevelSystem.sharedInstance.SetPlayerExp(this.enemyExp);
            //LevelSystem.sharedInstance.SetPlayerTotalExp(this.enemyExp);
            LevelSystem.sharedInstance.calculateExp(this.enemyExp);
            Destroy(gameObject);
        }


        if (col.gameObject.CompareTag("NoPlayerAttack") && PlayerController.sharedInstance.GetIsAttacking() == false
            && PlayerController.sharedInstance.IsAlive())
            PlayerController.sharedInstance.EnemyKnockBack(transform.position.x, damage);
    }
}
