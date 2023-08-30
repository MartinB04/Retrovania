using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rgbd;
    private CapsuleCollider2D capsuleCollider;

    [SerializeField] private BoxCollider2D attackCollider;

    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spr;

    [SerializeField] float jumpForce = 5;
    [SerializeField] float runningSpeed = 1.5f;
    [SerializeField] float pointsLife = 100;
    //sirva para detectar capa de suelo
    [SerializeField] LayerMask groundLayer;

    private bool bandAnimation;
    private bool movement = true;
    [SerializeField] float enemyImpulse = 2f;

    [SerializeField] LayerChecker footA;
    [SerializeField] LayerChecker footB;
    

    //Singleton de playercontroller
    public static PlayerController sharedInstance;

    
    private Vector3 startPosition;
    //private Vector3 localScale;

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        
        sharedInstance = this;
        //Toma el valor de donde empieza personaje
        startPosition = this.transform.position;
      
        //localScale = this.transform.localScale;
    }

   
    public void Start()
    {
        //antes public void StartGame()
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        animator.SetBool("isMoving", false);
        animator.SetBool("rightAnimation", true);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isFalling", false);
        animator.SetBool("isHurting", false);
        bandAnimation = false;
        //Cada vez que reiniciamos colocamos al personaje en la posicion inicial
        this.transform.position = this.startPosition;

        this.SaveLife(ChangeScene.sharedInstance.LoadPlayerPointsLife());
    
        this.attackCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            // solo poder saltar si esta en modo juego
            if (Input.GetButtonDown("Jump") && IsTouchingTheGround())
                Jump();
            SetAnimations();

        }

    }

    //Gestiona las animaciones
    private void SetAnimations()
    {
        animator.SetBool("isGrounded", IsTouchingTheGround());
        animator.SetBool("isMoving", IsMoving());
        animator.SetBool("rightAnimation", MirrorAnimation());
        animator.SetBool("isAttacking", IsAttacking());
        animator.SetBool("isFalling", IsFalling());
        animator.SetBool("isHurting", IsHurting());
        animator.SetBool("test", AnimationTest());
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

    private void FixedUpdate()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame) {
            if (movement && IsAlive())
                Movement();
            MirrorAnimation();

            if (Input.GetButtonDown("Pause"))
                GameManager.sharedInstance.Pause();
        }
    }

    //corrige la pocision cuando se invierte la animacion
    private void FixAnimationMirror()
    {
        if ((bandAnimation == true) && Input.GetKey(KeyCode.D))
        {
            rgbd.transform.localPosition = new Vector3((rgbd.transform.localPosition.x +1), rgbd.transform.localPosition.y, rgbd.transform.localPosition.z);
            bandAnimation = false;
        }

        if ((bandAnimation == false) &&Input.GetKey(KeyCode.A))
        {
            rgbd.transform.localPosition = new Vector3((rgbd.transform.localPosition.x - 1), rgbd.transform.localPosition.y, rgbd.transform.localPosition.z);
            bandAnimation = true;
        }
    }
    

    public bool IsAttacking()
    {
        return Input.GetButton("Attack");
    }

    //suma o resta los pv que recibe como parametro al jugador
    public void SetLife(float life)
    {
        this.pointsLife += life;
        ChangeScene.sharedInstance.SavePlayerPointsLife(this.pointsLife);
    }

    public float GetLife()
    {
        return this.pointsLife;
    }

    //Establece los puntos de vida almacenados en PlayerPrefs
    public void SaveLife(float life)
    {
        this.pointsLife = life;
        Debug.Log("PLAYER CONTROLLER save data this.pointslife " + this.pointsLife);
    }

    bool IsMoving()
    {
        return rgbd.velocity.x != 0 && movement == true;
    }

    bool MirrorAnimation()
    {
        return Input.GetKey(KeyCode.D);
    }

    void Movement()
    {
        //solo poder moverse si estar en modo juego
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (rgbd.velocity.x < runningSpeed)
                    rgbd.velocity = new Vector2(runningSpeed, rgbd.velocity.y);
                //rgbd.transform.localScale = localScale;
                rgbd.transform.localScale = new Vector3(1, 1, 1);
                //rgbd.transform.localEulerAngles = new Vector3(0, 0, 0);
                FixAnimationMirror();
            }
            

            if (Input.GetKey(KeyCode.A))
            {
                if (rgbd.velocity.x > -runningSpeed)
                    rgbd.velocity = new Vector2(-runningSpeed, rgbd.velocity.y);

                //rgbd.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
                rgbd.transform.localScale = new Vector3(-1, 1, 1);
                //rgbd.transform.localEulerAngles = new Vector3(0, 180, 0);
                FixAnimationMirror();
            }
            
        }
    }

    void Jump()
    {
        if (IsTouchingTheGround())
        {
            // Eliminar la velocidad vertical actual antes de aplicar el salto
            rgbd.velocity = new Vector2(rgbd.velocity.x, 0f);

            // Aplicar el impulso de salto
            // F = m*a ====> a = F/m
            rgbd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsTouchingTheGround()
    {
        //Verifica si almenos uno de los elementos hijos pie toca el suelo
        return footA.isTouching || footB.isTouching;
    }

    public void Kill()
    {
        this.animator.SetBool("isAlive", false);
      
        Debug.Log("Jugador muerto");
        Invoke("GameOver", 3f);
    }

    public void EnemyKnockBack(float enemyPosX)
    {
        movement = false;
        //IsHurting();
        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rgbd.AddForce(enemyImpulse * side * Vector2.left, ForceMode2D.Impulse);
        Invoke("EnableMovement", 0.9f);
        

        spr.color = Color.red;

    }
    private bool IsHurting()
    {
        return !movement;
    }
    void EnableMovement()
    {
        movement = true;
        spr.color = Color.white;
        if (pointsLife <= 0)
            Kill();
        spr.color = Color.white;
    }
    void GameOver()
    {
        GameManager.sharedInstance.GameOver();
        Debug.Log("Juego terminado");
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

}
