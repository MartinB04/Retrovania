using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    public float damage = 10.0f; 
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerAttack") && PlayerController.sharedInstance.GetAttackCollider())
            Destroy(gameObject);

        if (col.gameObject.CompareTag("NoPlayerAttack") && PlayerController.sharedInstance.IsAttacking() == false
            && PlayerController.sharedInstance.IsAlive())
        {
            PlayerController.sharedInstance.EnemyKnockBack(transform.position.x);
            PlayerController.sharedInstance.SetLife(damage);
        }
    }
}
