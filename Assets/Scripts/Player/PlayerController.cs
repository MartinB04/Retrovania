using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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


    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float runningSpeed = 1.5f;
    
    [SerializeField] float enemyImpulse = 2f;
    
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerChecker footA;
    [SerializeField] LayerChecker footB;
    [SerializeField] float fixFlip;

    public static PlayerController sharedInstance;

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

        
        this.attackCollider.enabled = false;

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
            this.isAlive = true;

        attackAnimationStatus = false;
        this.afterAttack = false;
    }

    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (GetIsTouchingTheGround() && InputManager.sharedInstance.GetJumpButton() && !this.attackAnimationStatus)
                Jump();

            if (InputManager.sharedInstance.GetAttackButton())
                Attack();

            Debug.Log($"Attack {this.isAttacking}");
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
        //return rgbd.velocity.x != 0 && canMove;
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
        //AttackAnimationStatus(true);
    }

    void Movement()
    {
        if (this.attackAnimationStatus && this.GetIsTouchingTheGround())
        {
            rgbd.velocity = new Vector2(0f, rgbd.velocity.y);
        }
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

            this.isMoving = moveX != 0 ? true : false;

            rgbd.velocity = new Vector2(moveX, rgbd.velocity.y);
        }
    }

    private void FlipRigidbody(bool flip, float value)
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

    void Jump()
    {
        rgbd.velocity = new Vector2(rgbd.velocity.x, 0f);
        rgbd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
        //yield return new WaitForSeconds(0.1f);
        yield return new WaitForFixedUpdate();
        this.afterAttack = false;
    }

    public float GetCurrentPlayerPosition()
    {
        return this.transform.position.y;
    }


}
