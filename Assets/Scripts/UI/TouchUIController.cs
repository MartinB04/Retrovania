using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchUIController : MonoBehaviour
{
    public static TouchUIController sharedInstance;

    [SerializeField] Image actionButton;
    //[SerializeField] Image pauseButton;

    private void Awake()
    {
        sharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.actionButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    public void SetActionButton(bool status)
    {
        this.actionButton.enabled = status;
    }
}
