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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCoinValue(int coin)
    {
        this.coinValue = coin;
        Debug.Log($"Valor moneda = {this.coinValue}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log($"OnTriggerEnter = {this.coinValue}");
            LevelSystem.sharedInstance.SetPlayerMoney(this.coinValue);
            Destroy(gameObject);
        }

    }
}
