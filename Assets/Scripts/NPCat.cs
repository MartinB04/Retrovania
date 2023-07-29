using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCat : MonoBehaviour
{
    public Animator animator;
    public Canvas canvas;
    public Text text;
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("NoPlayerAttack"))
        {
            Debug.Log("sesupone que jala Bv");
            text.text = "Moverse: A y D \nSaltar: J\nAtacar: K";
            canvas.enabled = true;
            Invoke("ChangeText", 5f);   
        }
    }

    private void ChangeText()
    {
        text.text = "Busca las 3 reliquias\nEntra al nivel 3\nY recoge la reliquia final";
        Invoke("DisableCanvas", 5f);
    }
    private void DisableCanvas()
    {
        canvas.enabled = false;
    }
}
