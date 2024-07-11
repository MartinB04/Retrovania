using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;

    private Rigidbody2D rgbd;
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer spr;

    private bool canMove = true;
    private bool isHurt = false;
    private bool isAttacking = false;
    private bool isAlive = true;
    private bool attackAnimationStatus;

    private bool isMoving;

    private bool afterAttack;
    private bool afterJump;

    private bool blockedFlip;


    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float runningSpeed = 1.5f;
    [SerializeField] float enemyImpulse = 2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerChecker footA;
    [SerializeField] LayerChecker footB;
    [SerializeField] float fixFlip;

    private Vector3 startPosition;

    private void Awake()
    {
        this.rgbd = GetComponent<Rigidbody2D>();
        this.capsuleCollider = GetComponent<CapsuleCollider2D>();
        this.spr = GetComponentInChildren<SpriteRenderer>();

        sharedInstance = this;
        startPosition = this.transform.position;
    }

    public void Start()
    {
        if (PlayerAnimationController.sharedInstance.GetMirrorAnimation())
            FlipRigidbody(false, this.fixFlip);
        else
            FlipRigidbody(true, 0f);

        int numScene = ChangeScene.sharedInstance.GetNumberCurrentScene();
        Vector2 playerPosition = DataStorage.sharedInstance.GetPlayerPosition(numScene);
        
        if (playerPosition == Vector2.zero)
            this.transform.position = this.startPosition;
        else
            this.transform.position = playerPosition;

        

        if (PlayerAnimationController.sharedInstance.GetMirrorAnimation())
        {
            rgbd.transform.localScale = new Vector3(-1, 1, 1);
            //El .4f representa un ajuste para que el player este centrado en la puerta.
            rgbd.transform.localPosition = new Vector3((rgbd.transform.localPosition.x - (this.fixFlip + .4f)), rgbd.transform.localPosition.y, rgbd.transform.localPosition.z);
        }

        

        this.attackCollider.enabled = false;

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
            this.isAlive = true;

        attackAnimationStatus = false;
        this.afterAttack = false;
        this.afterJump = false;
        this.blockedFlip = false;
    }

    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (GetIsTouchingTheGround() && InputManager.sharedInstance.GetJumpButton() && !this.attackAnimationStatus)
                Jump();

            if (InputManager.sharedInstance.GetAttackButton())
                Attack();

            if (GetIsAttacking() && !GetIsTouchingTheGround())
                this.blockedFlip = true;
            else
                this.blockedFlip = false;

            //Debug.Log($"BlockedFlip {this.blockedFlip}");
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame && this.canMove && GetIsAlive())
            Movement();
    }

    public bool GetIsFalling()
    {
        return !GetIsTouchingTheGround() && rgbd.velocity.y <= 0;
    }

    public bool GetIsMoving()
    {
        return this.isMoving;
    }

    public bool GetIsTouchingTheGround()
    {
        return footA.isTouching || footB.isTouching;
    }

    public bool GetIsAttacking()
    {
        return this.isAttacking;
    }

    public bool GetIsAlive()
    {
        return this.isAlive;
    }

    public bool GetIsHurt()
    {
        return this.isHurt;
    }

    public void Attack()
    {
        this.isAttacking = true;
    }

    void Movement()
    {
        if (this.attackAnimationStatus && this.GetIsTouchingTheGround())
            rgbd.velocity = new Vector2(0f, rgbd.velocity.y);
        else
        {
            Vector2 moveInput = InputManager.sharedInstance.GetMovement();
            float direction = moveInput.x;
            float moveX = 0f;

            if (direction > 0)
            {
                if (PlayerAnimationController.sharedInstance.GetMirrorAnimation())
                {
                    PlayerAnimationController.sharedInstance.SetMirrorAnimation(false);                              
                        FlipRigidbody(true, this.fixFlip);
                }
                moveX = runningSpeed;
            }
            else if (direction < 0)
            {
                if (!PlayerAnimationController.sharedInstance.GetMirrorAnimation())
                {
                    PlayerAnimationController.sharedInstance.SetMirrorAnimation(true);                
                        FlipRigidbody(false, this.fixFlip);
                }
                moveX = -runningSpeed;
            }

            //StartCoroutine(FixFlip(direction));

            this.isMoving = moveX != 0 ? true : false;

            rgbd.velocity = new Vector2(moveX, rgbd.velocity.y);
        }
    }

    

    private void FlipRigidbody(bool flip, float value)
    {
        if (!this.blockedFlip) 
        {
            if (flip)
            {
                rgbd.transform.localScale = new Vector3(1, 1, 1);
                rgbd.transform.localPosition = new Vector3((rgbd.transform.localPosition.x + value), rgbd.transform.localPosition.y, rgbd.transform.localPosition.z);
            }
            else
            {
                rgbd.transform.localScale = new Vector3(-1, 1, 1);
                rgbd.transform.localPosition = new Vector3((rgbd.transform.localPosition.x - value), rgbd.transform.localPosition.y, rgbd.transform.localPosition.z);
            }
        }
    }

    public void Jump()
    {
        rgbd.velocity = new Vector2(rgbd.velocity.x, 0f);
        rgbd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        SetAfterJump(true);
    }

    public IEnumerator Kill()
    {
        yield return new WaitForSeconds(1);
        this.isHurt = false;
        this.isAlive = false;
        spr.color = Color.white;
        StartCoroutine(GameOver());
    }

    public void EnemyKnockBack(float enemyPosX, float damage)
    {
        canMove = false;
        this.isHurt = true;
        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rgbd.AddForce(enemyImpulse * side * Vector2.left, ForceMode2D.Impulse);
        spr.color = Color.red;

        LevelSystem.sharedInstance.DecreaseLife(damage);

        if (LevelSystem.sharedInstance.GetLife() <= 0)
            StartCoroutine(Kill());
        else
            StartCoroutine(EnableMovement());
    }

    IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(1);
        this.canMove = true;
        this.isHurt = false;
        this.spr.color = Color.white;
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        GameManager.sharedInstance.GameOver();
    }

    public void AttackCollider(int act)
    {
        attackCollider.enabled = act == 1;
    }

    public bool GetAttackCollider()
    {
        return attackCollider.enabled;
    }

    public void AttackAnimationStatus(bool status)
    {
        if (!status) { 
            this.isAttacking = false;
            this.afterAttack = true;
            StartCoroutine(SetAfterAttack());
        }
        this.attackAnimationStatus = status;
        
    }
    

    public bool GetAfterAttack()
    {
        return this.afterAttack;
    }

    IEnumerator SetAfterAttack()
    {
        yield return new WaitForFixedUpdate();
        this.afterAttack = false;

        //FixFlip();

    }

    /*
    void FixFlip()
    {
        bool mirror = PlayerAnimationController.sharedInstance.GetMirrorAnimation();
        Vector2 moveInput = InputManager.sharedInstance.GetMovement();
        float direction = moveInput.x;

        //si mirror true player mira izquierda

        //dir falso player va izquierda, true va derecha

        bool dir = direction > 0 ? true : false;

        if(mirror && !dir )
            FlipRigidbody(true, this.fixFlip);
        else if(!mirror && dir)
            FlipRigidbody(false, this.fixFlip);

 
    }
    */

    public float GetCurrentPlayerPosition()
    {
        return this.transform.position.y;
    }

    public bool GetAfterJump()
    {
        return this.afterJump;
    }

    public void SetAfterJump(bool status)
    {
        this.afterJump = status;
    }
}
