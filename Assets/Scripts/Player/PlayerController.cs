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

    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float runningSpeed = 1.5f;
    [SerializeField] float lifePoints = 100;
    [SerializeField] float enemyImpulse = 2f;

    [SerializeField] float MAXLIFE;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerChecker footA;
    [SerializeField] LayerChecker footB;
    [SerializeField] float fixFlip;

    //Singleton de playercontroller
    public static PlayerController sharedInstance;


    private Vector3 startPosition;

    private void Awake()
    {
        this.rgbd = GetComponent<Rigidbody2D>();
        this.capsuleCollider = GetComponent<CapsuleCollider2D>();

        this.spr = GetComponentInChildren<SpriteRenderer>();

        sharedInstance = this;
        startPosition = this.transform.position; //Toma el valor de la posicion del player en el inspector


    }


    public void Start()
    {


        //Gira
        if (PlayerAnimationController.sharedInstance.GetMirrorAnimation())
            FlipRigidbody(false, this.fixFlip);
        else
            FlipRigidbody(true, 0f);

        int numScene = ChangeScene.sharedInstance.GetNumberCurrentScene();
        Vector2 playerPosition = DataStorage.sharedInstance.GetPlayerPosition(numScene);

        if (playerPosition == Vector2.zero) //si la posicion del player en DataStorage es 0, toma la posicion del inspector.
            this.transform.position = this.startPosition;
        else
            this.transform.position = playerPosition;

        this.LoadLife(DataStorage.sharedInstance.LoadPlayerPointsLife());
        this.attackCollider.enabled = false;

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
            this.isAlive = true;

        attackAnimationStatus = false;
    }

    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            // solo poder saltar si esta en modo juego
            if (IsTouchingTheGround() && InputManager.sharedInstance.GetJumpButton() && !this.attackAnimationStatus)
                Jump();

            Attack();
        }
        Debug.Log("CanMOve" + this.canMove);
        Debug.Log("isAttack " + this.isAttacking);
        Debug.Log("this.attackAnimationStatus" + this.attackAnimationStatus);
    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
            if (canMove && IsAlive())
                Movement();
    }



    public bool AnimationTest()
    {
        return Input.GetKey(KeyCode.F);
    }
    public bool IsFalling()
    {
        return !IsTouchingTheGround() && rgbd.velocity.y <= 0;
    }
    public bool IsMoving()
    {
        return rgbd.velocity.x != 0 && canMove == true;
    }
    public bool IsTouchingTheGround()
    {
        //Verifica si almenos uno de los elementos hijos pie toca el suelo
        return footA.isTouching || footB.isTouching;
    }
    public bool GetIsAttacking()
    {
        return this.isAttacking;
    }
    public bool IsAlive()
    {
        return this.isAlive;
    }
    public bool IsHurt()
    {
        return this.isHurt;
    }


    public void Attack()
    {
        if (InputManager.sharedInstance.GetAttackButton())
        {
            this.isAttacking = true;
            AttackAnimationStatus(true);
            //Invoke("RealiseAttack", 1f);
        }
    }

    //libera para detener la animacion de ataque
    /* public void RealiseAttack()
    {
        this.isAttacking = false;
        Debug.Log("false");
    } */

    //suma o resta los pv que recibe como parametro al jugador
    public void SetLife(float life)
    {
        if (life > 0)
        {
            if (GetLife() < this.MAXLIFE)
                this.lifePoints += life;
            if (this.lifePoints > this.MAXLIFE)
                this.lifePoints = this.MAXLIFE;
        }
        else
        {
            this.lifePoints += life;
            if (this.lifePoints < 0)
                this.lifePoints = 0;
        }

        DataStorage.sharedInstance.SavePlayerPointsLife(this.lifePoints);

    }

    public float GetLife()
    {
        return this.lifePoints;
    }

    //Establece los puntos de vida almacenados en DataStorage
    public void LoadLife(float life)
    {
        this.lifePoints = life;
    }

    void Movement()
    {
        if (this.attackAnimationStatus && this.IsTouchingTheGround()) // Verifica si el jugador est� atacando
        {

            rgbd.velocity = new Vector2(0f, rgbd.velocity.y); // Detiene el movimiento en el eje X mientras ataca
        }
        else
        {
            float direction = InputManager.sharedInstance.GetMovement();
            float moveX = 0f; // Inicialmente, la velocidad en el eje X es 0
                              //float direction = Input.GetAxis("Horizontal");
                              //if (direction > 0)  // Si se presiona la tecla D, establece la velocidad a runningSpeed en el eje X
            if (direction == 1)  // Si se presiona la tecla D, establece la velocidad a runningSpeed en el eje X
            {
                if (PlayerAnimationController.sharedInstance.GetMirrorAnimation())
                {
                    PlayerAnimationController.sharedInstance.SetMirrorAnimation(false);
                    //Debug.Log(PlayerAnimationController.sharedInstance.GetMirrorAnimation() + "se supone que false Bv");
                    FlipRigidbody(true, this.fixFlip);
                }

                moveX = runningSpeed;


                // Voltea sprite a la derecha.
            }
            else if (direction == -1)
            {
                if (!PlayerAnimationController.sharedInstance.GetMirrorAnimation())
                {
                    PlayerAnimationController.sharedInstance.SetMirrorAnimation(true);

                    FlipRigidbody(false, this.fixFlip);
                }
                // Si se presiona la tecla A, establece la velocidad a -runningSpeed en el eje X
                moveX = -runningSpeed;


            }
            rgbd.velocity = new Vector2(moveX, rgbd.velocity.y); // Establece la velocidad en el eje X
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
        // F = m*a ====> a = F/m

        rgbd.velocity = new Vector2(rgbd.velocity.x, 0f); // Eliminar la velocidad vertical actual antes de aplicar el salto
        rgbd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Aplicar el impulso de salto

    }



    public void Kill()
    {
        this.isHurt = false;
        this.isAlive = false;
        spr.color = Color.white;
        Invoke("GameOver", 3f);
    }

    public void EnemyKnockBack(float enemyPosX, float damage)
    {
        canMove = false;
        this.isHurt = true;
        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rgbd.AddForce(enemyImpulse * side * Vector2.left, ForceMode2D.Impulse);
        spr.color = Color.red;

        this.SetLife(damage);

        if (this.lifePoints <= 0)
            Invoke("Kill", 0.9f);
        else
            Invoke("EnableMovement", 0.9f);

    }

    void EnableMovement()
    {
        canMove = true;
        this.isHurt = false;
        spr.color = Color.white;
    }
    void GameOver()
    {
        GameManager.sharedInstance.GameOver();
    }


    // Agrega un nuevo m�todo en tu script PlayerController para habilitar o deshabilitar el Collider2D
    public void AttackCollider(int act)
    {
        attackCollider.enabled = act == 1 ? true : false;
    }

    public bool GetAttackCollider()
    {
        return attackCollider.enabled;
    }
    //<----Termina el animationEvent para el attack collider---->

    public void AttackAnimationStatus(bool status)
    {
        this.attackAnimationStatus = status;

        //this.isAttacking = status;
        if (!status)
            this.isAttacking = false;
    }

    public float GetCurrentPlayerPosition()
    {
        return this.transform.position.y;
    }
}
