using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinController : MonoBehaviour
{

    private CircleCollider2D circleCollider;

    private int coinValue = 0;

   

    private void Awake()
    {
        this.circleCollider = GetComponent<CircleCollider2D>();
    }

    public void SetCoinValue(int coin)
    {
        this.coinValue = coin;
        //Debug.Log($"Valor moneda = {this.coinValue}");
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            //Debug.Log($"OnTriggerEnter = {this.coinValue}");
            LevelSystem.sharedInstance.SetPlayerMoney(this.coinValue);
            //this.audioSource.PlayOneShot(this.coinAudio);
            AudioManager.sharedInstance.PlayCoin();
            Destroy(gameObject);
        }

    }
}
