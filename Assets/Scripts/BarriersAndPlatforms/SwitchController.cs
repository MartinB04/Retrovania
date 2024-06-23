using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum Switch
{
    switch1, switch2, switch3
}*/

public class SwitchController : MonoBehaviour
{
    
    //[SerializeField] Switch switchs;
    [SerializeField] GameObject target;

    private SpriteRenderer spr;

    private void Awake()
    {
        this.spr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && this.target != null)
        {
            bool newState;
            if (this.spr.color == Color.white)
            {
                this.spr.color = Color.red;
                newState = this.target.activeSelf;
                this.target.SetActive(!newState);
            }
            else
            {
                this.spr.color = Color.white;
                newState = this.target.activeSelf;
                this.target.SetActive(!newState);
            }

        }
        
    }
}
