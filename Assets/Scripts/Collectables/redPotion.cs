using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redPotion : MonoBehaviour
{

    [SerializeField] float recoveryLife;
    private float remainingPoints;

    private Animator animator;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!this.animator)
            Debug.LogWarning("Animator no asignado.");

        this.remainingPoints = this.recoveryLife;
        this.animator.SetBool("full", true);
        this.animator.SetBool("medium", false);
        this.animator.SetBool("almostEmpty", false);
        this.animator.SetBool("empty", false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            this.remainingPoints = LevelSystem.sharedInstance.IncreaseLife(this.remainingPoints);
            /*if (this.remainingPoints <= 0)
                Destroy(gameObject);
            else
                this.recoveryLife = this.remainingPoints;
            */

            if(this.remainingPoints < this.recoveryLife)
                Potion();

            if (this.remainingPoints <= 0)
                StartCoroutine(DestroyPotion());
        }
    }


    private void Potion()
    {

        if(this.remainingPoints >= this.recoveryLife * 0.5f) 
        {
            this.animator.SetBool("full", false);
            this.animator.SetBool("medium", true);
            this.animator.SetBool("almostEmpty", false);
            this.animator.SetBool("empty", false);
        }
        else if((this.remainingPoints < this.recoveryLife * 0.5f) && this.remainingPoints > 0)
        {
            this.animator.SetBool("full", false);
            this.animator.SetBool("medium", false);
            this.animator.SetBool("almostEmpty", true);
            this.animator.SetBool("empty", false);
        }
        else
        {
            this.animator.SetBool("full", false);
            this.animator.SetBool("medium", false);
            this.animator.SetBool("almostEmpty", false);
            this.animator.SetBool("empty", true);
            
        }

        
    }

    IEnumerator DestroyPotion()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
