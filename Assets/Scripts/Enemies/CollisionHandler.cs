using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float damage = 10.0f;
    [SerializeField] float enemyLife = 100;
    [SerializeField] int enemyExp;

    [SerializeField] int money;
    [SerializeField] GameObject coinPrefab;

    private FireSkullController fireSkullController;
    private NightmareController nightmareController;

    private int enemyMaxLife = 100;

    private void Awake()
    {
        if (gameObject.name == "FireSkull")
            this.fireSkullController = GetComponentInParent<FireSkullController>();
        else if (gameObject.name == "Nightmare")
            this.nightmareController = GetComponentInParent<NightmareController>();

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerAttack") && PlayerController.sharedInstance.GetAttackCollider())
        {
            SetEnemyLife(LevelSystem.sharedInstance.GetCurrentPlayerDamage());

            if (this.enemyLife <= 0)
            {
                LevelSystem.sharedInstance.calculateExp(this.enemyExp);

                GameObject coin =Instantiate(this.coinPrefab, transform.position, Quaternion.identity);
                coinController coinScript = coin.GetComponent<coinController>();
                if (coinScript != null)
                    coinScript.SetCoinValue(this.money);
                else
                    Debug.LogWarning("CoinScript nulo");

                Destroy(gameObject);
            }
            else
            {
                if(gameObject.name == "FireSkull") {
                    Debug.Log(gameObject.name);
                   fireSkullController.SetHurt();
                }

            }
        }


        if (col.gameObject.CompareTag("NoPlayerAttack") && PlayerController.sharedInstance.GetIsAttacking() == false
            && PlayerController.sharedInstance.GetIsAlive())
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
