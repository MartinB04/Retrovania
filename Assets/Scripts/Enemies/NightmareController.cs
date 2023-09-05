using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareController : MonoBehaviour
{
    public float distance,speed = 1f;
    public bool horizontal;
    public Animator animator;

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
            


            if (rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f)
            {
                speed = -speed;
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            }

            if (speed < 0)
                transform.localScale = new Vector3(1f, 1f, 1f);
            if (speed > 0)
                transform.localScale = new Vector3(-1f, 1f, 1f);

        }



        else
            rb2d.velocity = new Vector2(0, 0);
    }


    
}
