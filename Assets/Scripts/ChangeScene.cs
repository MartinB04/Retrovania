using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    level1, level2, level3, level4, level5, finalLevel
}
public class ChangeScene : MonoBehaviour
{
    public static ChangeScene sharedInstance;

    [SerializeField] Scenes tarjetScene;

    private bool knockingDoor;

    private void Awake()
    {
        sharedInstance = this;
    }

    

    private void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (this.knockingDoor && Input.GetKeyDown(KeyCode.E))
                ChangeSceneNow();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            this.knockingDoor = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            this.knockingDoor = false;
    }

    //se va a ejecutar desde el manager, solo si es game over, volvera a cargar lvl1
    public void RefreshScene()
    {

        Debug.Log("Va a refrescar lvl1");

        SceneManager.LoadScene("Level1");
        Debug.Log("Refresco lvl1");

    }

    public string GetCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.name;
    }

    


    void ChangeSceneNow()
    {
        if (this.tarjetScene == Scenes.level1)
            SceneManager.LoadScene("Level1");
        else if (this.tarjetScene == Scenes.level2)
            SceneManager.LoadScene("Level2");
        else if (this.tarjetScene == Scenes.level3)
            SceneManager.LoadScene("Level3");
        else if (this.tarjetScene == Scenes.level4)
            SceneManager.LoadScene("Level4");
        else if (this.tarjetScene == Scenes.level5)
            SceneManager.LoadScene("Level5");
        else if (this.tarjetScene == Scenes.finalLevel)
            SceneManager.LoadScene("FinalLevel");

    }




}