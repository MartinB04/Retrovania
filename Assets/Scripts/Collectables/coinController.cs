using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinController : MonoBehaviour
{

    private CircleCollider2D circleCollider;

    private int coinValue = 0;

    private bool isCollected = false;

   

    private void Awake()
    {
        this.circleCollider = GetComponent<CircleCollider2D>();
    }

    public void SetCoinValue(int coin)
    {
        Debug.Log($"Valor moneda antes set = {this.coinValue}");
        this.coinValue = coin;
        Debug.Log($"Valor moneda  depues set = {this.coinValue}");
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this.isCollected && collision.gameObject.CompareTag("Player")) {
            this.isCollected = true;
            Debug.Log($"OnTriggerEnter = {this.coinValue}");
            LevelSystem.sharedInstance.SetPlayerMoney(this.coinValue);
            //this.audioSource.PlayOneShot(this.coinAudio);
            AudioManager.sharedInstance.PlayCoin();
            Destroy(gameObject);
        }

    }
}
