using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Text npcNameText;
    [SerializeField] Text dialogueText; // Referencia al Texto UI donde se mostrará el diálogo.
    private Queue<string> sentences; // Cola para almacenar las líneas de diálogo.
    private string npcName;
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(string[] dialogue, string name)
    {
        this.canvas.enabled = true;

        sentences.Clear();

        // Agrega todas las líneas de diálogo a la cola.
        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }
        this.npcName = name;

        // Muestra la primera línea de diálogo.
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
        dialogueText.text = sentence; // Muestra la línea de diálogo en la ventana de texto.
        Invoke("DisplayNextSentence", 5f);
    }

    void EndDialogue()
    {
        // Puedes agregar aquí la lógica para cerrar la ventana de diálogo o realizar otras acciones.
        Debug.Log("Fin del diálogo");
        this.canvas.enabled = false;
    }
}
