using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraversablePlatform : MonoBehaviour
{

    private bool isDropping = false;
    private bool isTouchingThePlatform = false;
    Collider2D collisionPlayer;
    BoxCollider2D boxColliderPlayer;
    CapsuleCollider2D capsuleColliderPlayer;

    PlatformEffector2D platformEffector2D;

    [SerializeField] BoxCollider2D triggerCollider2D;

    private void Awake()
    {
        this.platformEffector2D = GetComponent<PlatformEffector2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isTouchingThePlatform)
        {
            Debug.Log("Tocando plataforma");
           
            if (InputManager.sharedInstance.GetMovementY().y < 0 && InputManager.sharedInstance.GetJumpButton())
            {
                Debug.Log("Entro al if de desactivar");
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), boxColliderPlayer, true);
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), capsuleColliderPlayer, true);
                this.isDropping = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jugador entrando");
            this.isTouchingThePlatform = true;
            this.collisionPlayer = collision;

            boxColliderPlayer = collisionPlayer.gameObject.GetComponent<BoxCollider2D>();
            capsuleColliderPlayer = collisionPlayer.gameObject.GetComponent<CapsuleCollider2D>();

            //if  (PlayerController.sharedInstance.GetIsAttacking() == true || InputManager.sharedInstance.GetAttackButton() == true || (Input.GetKeyDown(KeyCode.DownArrow) ))
            if(this.isDropping)
                StartCoroutine(bajarPlataforma());
            

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jugador saliendo");
            this.isTouchingThePlatform = false;

        }
    }

    IEnumerator bajarPlataforma()
    {
        yield return new WaitForSeconds(0.5f);
        this.isDropping = false;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), boxColliderPlayer, false);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), capsuleColliderPlayer, false);
    }

}
