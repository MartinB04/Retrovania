using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCat : MonoBehaviour
{
    
    //[SerializeField] Dialogue dialogue;
    [SerializeField] DialogueManager dialogueManager;

    private Animator animator;
    [SerializeField] string[] dialogueLines; // Líneas de diálogo del NPC.
    private string npcName;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    private void Start()
    {
        this.npcName = gameObject.name;
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //solo se inicia cuando el jugador lo toca y no hay dialogo mostrandose antes.
        if (collision.gameObject.CompareTag("NoPlayerAttack") && DialogueManager.sharedInstance.GetDialogueStatus() == false)
        {
            // Cuando el jugador interactúa con el NPC, inicia el diálogo.
            dialogueManager.StartDialogue(dialogueLines, this.npcName);
        }
    }


    /*void OnMouseDown()
    {
        dialogueManager.StartDialogue(dialogue);
    }*/


    /*void OnTriggerEnter2D(Collider2D col)
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

    }*/
}
