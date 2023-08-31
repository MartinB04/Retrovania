using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Relic { 
    relic1, relic2, relic3, finalRelic
}


public class RelicController : MonoBehaviour
{
    [SerializeField] Relic typeRelic;
    [SerializeField] ObjectActivation target;

    private SpriteRenderer spr;

    private void Awake()
    {
        this.spr = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int relic = 0;

            if (this.typeRelic == Relic.relic1)
                relic = 1;
            else if (this.typeRelic == Relic.relic2)
                relic = 2;
            else if (this.typeRelic == Relic.relic3)
                relic = 3;

            if (this.spr.color == Color.white)
            {
                this.spr.color = Color.red;
                if(this.typeRelic != Relic.finalRelic)
                    this.target.Activation(relic, false);
                else
                    GameManager.sharedInstance.Win();
            }
            else
            {
                this.spr.color = Color.white;
                if (this.typeRelic != Relic.finalRelic)
                    this.target.Activation(relic, true);
            }
        }

    }

}
