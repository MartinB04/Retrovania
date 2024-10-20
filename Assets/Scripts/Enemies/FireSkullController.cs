using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkullController : MonoBehaviour
{
    public float distance, speed = 1f;
    public bool flipRight;

    [SerializeField] Transform TargetA;
    [SerializeField] Transform TargetB;

    bool AxisY;

    Vector2 initialPosition;

    Vector2 midpoint;

    Vector2 initialPositionTargetA;
    Vector2 initialPositionTargetB;

    bool isHurt;

    private Rigidbody2D rb2d;
    private Animator animator;

    private void Awake()
    {
        this.animator = GetComponentInChildren<Animator>();
        this.rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        

        this.midpoint = (TargetA.position + TargetB.position) / 2;

        rb2d.position = this.midpoint;

        initialPositionTargetA = TargetA.position;
        initialPositionTargetB = TargetB.position;

        this.isHurt = false;

        this.animator.SetBool("isAlive", true);
        this.animator.SetBool("isHurt", this.isHurt);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if(!this.isHurt)
                Movement();
            else
                rb2d.linearVelocity = Vector2.zero;

        }
        else
        {
            rb2d.linearVelocity = Vector2.zero;
        }
    }

    

    void FlipAnimation()
    {
        if (this.flipRight)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Movement()
    {
        // Convertir las posiciones a Vector2
        Vector2 currentPosition = rb2d.position;
        Vector2 targetPositionA = initialPositionTargetA;
        Vector2 targetPositionB = initialPositionTargetB;

        // Calcula la direcci�n del movimiento.
        Vector2 direction = this.flipRight ? (targetPositionB - currentPosition) : (targetPositionA - currentPosition);

        direction.Normalize(); // Normaliza la direcci�n para mantener una velocidad constante.

        // Aplica velocidad en ambos ejes.
        rb2d.linearVelocity = direction * speed;

        // Cambia la direcci�n cuando llega a los l�mites.
        if ((this.flipRight && currentPosition.x >= initialPositionTargetB.x) ||
            (!this.flipRight && currentPosition.x <= initialPositionTargetA.x))
        {
            this.flipRight = !this.flipRight;
            FlipAnimation();
        }
    }

    public void SetHurt()
    {
        this.isHurt = true;
        this.animator.SetBool("isHurt", true);
        StartCoroutine(ResetHurt());
    }

    IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(1);
        this.isHurt = false;
        this.animator.SetBool("isHurt", false);

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Calculamos la direcci�n del rayo.
        Vector2 targetAPosition = TargetA.position;
        Vector2 targetBPosition = TargetB.position;
        Vector2 direction = targetBPosition - targetAPosition;

        // Dibuja un rayo desde TargetA hacia TargetB en el Editor de Unity.
        Gizmos.color = (targetAPosition.y == targetBPosition.y || targetAPosition.x == targetBPosition.x) ? Color.green : Color.red;
        Gizmos.DrawLine(targetAPosition, targetBPosition);
    }
#endif
}
