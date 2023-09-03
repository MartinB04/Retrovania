using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Text npcNameText;
    [SerializeField] Text dialogueText; // Referencia al Texto UI donde se mostrar� el di�logo.
    private Queue<string> sentences; // Cola para almacenar las l�neas de di�logo.
    private string npcName;
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(string[] dialogue, string name)
    {
        this.canvas.enabled = true;

        sentences.Clear();

        // Agrega todas las l�neas de di�logo a la cola.
        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }
        this.npcName = name;

        // Muestra la primera l�nea de di�logo.
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        npcNameText.text = this.npcName; //muestra el nombre del npc
        dialogueText.text = sentence; // Muestra la l�nea de di�logo en la ventana de texto.
        Invoke("DisplayNextSentence", 5f);
    }

    void EndDialogue()
    {
        // Puedes agregar aqu� la l�gica para cerrar la ventana de di�logo o realizar otras acciones.
        Debug.Log("Fin del di�logo");
        this.canvas.enabled = false;
    }
}
