using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rgbd;
    private CapsuleCollider2D capsuleCollider;
    private Animator animator;
    private SpriteRenderer spr;

    [SerializeField] private BoxCollider2D attackCollider;

    [SerializeField] float jumpForce = 5;
    [SerializeField] float runningSpeed = 1.5f;
    [SerializeField] float lifePoints = 100;
    [SerializeField] LayerMask groundLayer;

    private bool bandAnimation; //Ayuda a determinar la correccion de posicion del player
    private bool canMove = true;

    private bool isHurt = false;

    private bool isAttacking = false;

    [SerializeField] float enemyImpulse = 2f;

    [SerializeField] LayerChecker footA;
    [SerializeField] LayerChecker footB;
    

    //Singleton de playercontroller
    public static PlayerController sharedInstance;

    
    private Vector3 startPosition;
    //private Vector3 localScale;

    private void Awake()
    {
        this.rgbd = GetComponent<Rigidbody2D>();
        this.capsuleCollider = GetComponent<CapsuleCollider2D>();
        this.animator = GetComponentInChildren<Animator>();
        this.spr = GetComponentInChildren<SpriteRenderer>();
        
        sharedInstance = this;   
        startPosition = this.transform.position; //Toma el valor de la posicion del player en el inspector
    }

   
    public void Start()
    {
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isFalling", false);
        animator.SetBool("isHurt", false);
        bandAnimation = false;

        int numScene = ChangeScene.sharedInstance.GetNumberCurrentScene();
        Vector2 playerPosition = DataStorage.sharedInstance.GetPlayerPosition(numScene);

        if (playerPosition == Vector2.zero) //si la posicion del player en DataStorage es 0, toma la posicion del inspector.
            this.transform.position = this.startPosition;
        else
            this.transform.position = playerPosition;

        this.LoadLife(DataStorage.sharedInstance.LoadPlayerPointsLife());
        this.attackCollider.enabled = false;
    }

    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            // solo poder saltar si esta en modo juego
            if (IsTouchingTheGround() && InputManager.sharedInstance.GetJumpButton())
                Jump();
            SetAnimations();
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
            if (canMove && IsAlive())
                Movement();
    }

    //Gestiona las animaciones
    private void SetAnimations()
    {
        animator.SetBool("isAlive", IsAlive());
        animator.SetBool("isGrounded", IsTouchingTheGround());
        animator.SetBool("isMoving", IsMoving());
        
        animator.SetBool("isFalling", IsFalling());
        animator.SetBool("isHurt", IsHurt());
        animator.SetBool("test", AnimationTest());

        animator.SetBool("isAttacking", this.isAttacking);
    }

    private bool AnimationTest()
    {
        return Input.GetKey(KeyCode.F);
    }
    private bool IsFalling()
    {
        return !IsTouchingTheGround() && rgbd.velocity.y <= 0;
    }
    
    public bool IsAlive()
    { 
        return animator.GetBool("isAlive");
    }


    //corrige la pocision cuando se invierte la animacion
    private void FixAnimationMirror()
    {
        if ((bandAnimation == true) && Input.GetKey(KeyCode.D))
        {
            rgbd.transform.localPosition = new Vector3((rgbd.transform.localPosition.x +1), rgbd.transform.localPosition.y, rgbd.transform.localPosition.z);
            bandAnimation = false;
        }
         else if ((bandAnimation == false) && Input.GetKey(KeyCode.A))
        {
            rgbd.transform.localPosition = new Vector3((rgbd.transform.localPosition.x - 1), rgbd.transform.localPosition.y, rgbd.transform.localPosition.z);
            bandAnimation = true;
        }
    }
    

    public void Attack()
    {
        if (InputManager.sharedInstance.GetAttackButton())
        {
            this.isAttacking = true;
            Invoke("RealiseAttack", 1f);
        }
    }

    public void RealiseAttack()
    {
        this.isAttacking = false;
    }

    //suma o resta los pv que recibe como parametro al jugador
    public void SetLife(float life)
    {
        this.lifePoints += life;
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

    bool IsMoving()
    {
        return rgbd.velocity.x != 0 && canMove == true;
    }



    void Movement()
    {
        if (this.isAttacking) // Verifica si el jugador está atacando
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
                moveX = runningSpeed;
                FlipAnimation(true); // Voltea sprite a la derecha.
            }
            else if (direction== -1)
            {
                // Si se presiona la tecla A, establece la velocidad a -runningSpeed en el eje X
                moveX = -runningSpeed;
                FlipAnimation(false);
            }
            rgbd.velocity = new Vector2(moveX, rgbd.velocity.y); // Establece la velocidad en el eje X
        }
    }

    private void FlipAnimation(bool flip)
    {
        if(flip)
            rgbd.transform.localScale = new Vector3(1, 1, 1);
        else
            rgbd.transform.localScale = new Vector3(-1, 1, 1);
        FixAnimationMirror();
    }


    void Jump()
    {
        // F = m*a ====> a = F/m
        if (IsTouchingTheGround())
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x, 0f); // Eliminar la velocidad vertical actual antes de aplicar el salto
            rgbd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Aplicar el impulso de salto
        }
    }

    public bool IsTouchingTheGround()
    {
        //Verifica si almenos uno de los elementos hijos pie toca el suelo
        return footA.isTouching || footB.isTouching;
    }

    public void Kill()
    {
        this.isHurt = false;
        spr.color = Color.white;
        this.animator.SetBool("isAlive", false);

        Debug.Log("Jugador muerto");
        Invoke("GameOver", 3f);
    }

    public void EnemyKnockBack(float enemyPosX)
    {
        canMove = false;
        this.isHurt = true;
        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rgbd.AddForce(enemyImpulse * side * Vector2.left, ForceMode2D.Impulse);
        Invoke("EnableMovement", 0.9f);

        if (this.lifePoints <= 0)
            Kill();
        else
            Invoke("EnableMovement", 0.9f);

        spr.color = Color.red;

    }
    private bool IsHurt()
    {
        return this.isHurt;
    }
    void EnableMovement()
    {
        canMove = true;
        this.isHurt = false;
        spr.color = Color.white;
        if (lifePoints <= 0)
            Kill();
        spr.color = Color.white;
    }
    void GameOver()
    {
        GameManager.sharedInstance.GameOver();
        Debug.Log("Juego terminado");
    }
  

    // Agrega un nuevo método en tu script PlayerController para habilitar o deshabilitar el Collider2D
    public void AttackCollider(int act)
    {
        attackCollider.enabled = act == 1 ? true : false;
    }

    public bool GetAttackCollider()
    {
        return attackCollider.enabled;
    }
    //<----Termina el animationEvent para el attack collider---->


    public float GetCurrentPlayerPosition()
    {
        return this.transform.position.y;
    }
    
    public bool GetIsAttacking()
    {
        return this.isAttacking;
    }

}
