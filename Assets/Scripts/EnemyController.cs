using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float distance, damage, speed = 1f;
    public bool horizontal;

    Vector3 initialPosition;

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        initialPosition = rb2d.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            rb2d.AddForce(Vector2.right * speed);
            //float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
            //rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);


            if (horizontal == true && rb2d.transform.position.x < initialPosition.x + distance)
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            else
                horizontal = false;

            if (horizontal == false && rb2d.transform.position.x > initialPosition.x - distance)
                rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            else
                horizontal = true;

            if (horizontal == false)
                transform.localScale = new Vector3(1f, 1f, 1f);
            else
                transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
            rb2d.velocity = new Vector2(0, 0);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerAttack") && PlayerController.sharedInstance.IsAttacking() == true)
            Destroy(gameObject);

        if (col.gameObject.CompareTag("NoPlayerAttack") && PlayerController.sharedInstance.IsAttacking() == false
            && PlayerController.sharedInstance.IsAlive())
        {
            PlayerController.sharedInstance.EnemyKnockBack(transform.position.x);
            PlayerController.sharedInstance.SetLife(damage);
        }
    }

}