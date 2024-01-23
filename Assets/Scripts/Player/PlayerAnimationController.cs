using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public static PlayerAnimationController sharedInstance;
    private Animator animator;

    private bool mirrorAnimation = false;

    private void Awake()
    {
        sharedInstance = this;
        
        this.animator = GetComponent<Animator>();
        if (this.animator == null)
            Debug.LogWarning("Error, PlayerAnimationController no tiene animator asignado");
    }

    void Start()
    {
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isFalling", false);
        animator.SetBool("isHurt", false);


        this.mirrorAnimation = DataStorage.sharedInstance.GetDirectionPlayer();

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
            SetAnimations();
    }

    private void SetAnimations()
    {
        animator.SetBool("isAlive", PlayerController.sharedInstance.IsAlive());
        animator.SetBool("isGrounded", PlayerController.sharedInstance.IsTouchingTheGround());
        animator.SetBool("isMoving", PlayerController.sharedInstance.IsMoving());

        animator.SetBool("isFalling", PlayerController.sharedInstance.IsFalling());
        animator.SetBool("isHurt", PlayerController.sharedInstance.IsHurt());
        animator.SetBool("test", PlayerController.sharedInstance.AnimationTest());

        animator.SetBool("isAttacking", PlayerController.sharedInstance.GetIsAttacking());
    }

    public void SetMirrorAnimation(bool status)
    {
        this.mirrorAnimation = status;
    }

    public bool GetMirrorAnimation()
    {
        return this.mirrorAnimation;
    }

    

}
