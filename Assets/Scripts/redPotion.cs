using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redPotion : MonoBehaviour
{

    [SerializeField] float recoveryLife;
    private float remainingPoints;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("recoverylife " + this.recoveryLife);

            this.remainingPoints = PlayerController.sharedInstance.IncreaseLife(this.recoveryLife);
            //Debug.Log("recoverylife2 " + this.recoveryLife);


            if (this.remainingPoints <= 0)
                Destroy(gameObject);
            else
                this.recoveryLife = this.remainingPoints;
        }
    }
}
