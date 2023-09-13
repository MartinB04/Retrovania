using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static InputManager sharedInstance;

    private bool jumpButtonDown;
    private bool pauseButtonDown;
    private bool attackButtonDown;


    private void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.jumpButtonDown = Input.GetButtonDown("Jump");
        this.pauseButtonDown = Input.GetButtonDown("Pause");
        this.attackButtonDown = Input.GetButtonDown("Attack");
    }

    private void FixedUpdate()
    {
        
    }

    public bool GetJumpButton()
    {
        return this.jumpButtonDown;
    }

    public bool GetPauseButton()
    {
        return this.pauseButtonDown;
    } 
    public bool GetAttackButton()
    {
        return this.attackButtonDown;
    }
}
