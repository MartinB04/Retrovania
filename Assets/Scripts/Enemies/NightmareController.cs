using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareController : MonoBehaviour
{
    public float distance,speed = 1f;
    public bool horizontal;
    public Animator animator;

    Vector3 initialPosition;

    //float umbral = 0.1f;

    [SerializeField] LayerChecker footA;
    [SerializeField] LayerChecker footB;

    [SerializeField] float fixPosition;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        
        initialPosition = rb2d.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            //this.rb2d.AddForce(Vector2.right * this.speed);

            //this.rb2d.velocity = new Vector2(-this.speed, this.rb2d.velocity.y);
            this.rb2d.linearVelocity = new Vector2(this.speed, this.rb2d.linearVelocity.y);
            if (this.speed > 0 && !this.footA.isTouching)
            {
                this.speed = -Mathf.Abs(this.speed);
                FlipRigidBody();
            }
            else if(this.speed < 0 && !this.footA.isTouching)
            {
                this.speed = Mathf.Abs(this.speed);
                FlipRigidBody();
            }
        }
    }

    public void FlipRigidBody()
    {
        if (this.speed > 0)
        {
            this.rb2d.transform.localScale = new Vector3(-1, 1, 1);
            rb2d.transform.localPosition = new Vector3((rb2d.transform.localPosition.x - this.fixPosition), rb2d.transform.localPosition.y, rb2d.transform.localPosition.z);
        }
        else
        {
            this.rb2d.transform.localScale = new Vector3(1, 1, 1);
            rb2d.transform.localPosition = new Vector3((rb2d.transform.localPosition.x + this.fixPosition), rb2d.transform.localPosition.y, rb2d.transform.localPosition.z);
        }
    }
}
