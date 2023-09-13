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

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        this.midpoint = (TargetA.transform.position + TargetB.transform.position) / 2;

        rb2d.position = this.midpoint;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            Movement();
        }
        else
            rb2d.velocity = new Vector2(0, 0);

    }

    void FlipAnimation()
    {
        if (this.flipRight)
            gameObject.transform.localScale = new Vector3(-1,1,1);
        else
            gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
    void Movement()
    {
        // Calcula la dirección del movimiento.
        Vector2 direction = this.flipRight ? (TargetB.position - rb2d.transform.position) : (TargetA.position - rb2d.transform.position);
        direction.Normalize(); // Normaliza la dirección para mantener una velocidad constante.

        // Aplica velocidad en ambos ejes.
        rb2d.velocity = direction * speed;

        // Cambia la dirección cuando llega a los límites.
        if ((this.flipRight && rb2d.transform.position.x >= TargetB.position.x) ||
            (!this.flipRight && rb2d.transform.position.x <= TargetA.position.x))
        {
            this.flipRight = !this.flipRight;
            FlipAnimation();
        }
    }



    // Este método se ejecutará en el Editor de Unity.



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Calculamos la dirección del rayo.
        Vector2 targetAPosition = TargetA.position;
        Vector2 targetBPosition = TargetB.position;
        Vector2 direction = targetBPosition - targetAPosition;

        // Dibuja un rayo desde TargetA hacia TargetB en el Editor de Unity.

        if (targetAPosition.y == targetBPosition.y || targetAPosition.x == targetBPosition.x)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;
        Gizmos.DrawLine(targetAPosition, targetBPosition);
    }

#endif

}
