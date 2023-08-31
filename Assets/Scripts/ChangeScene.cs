using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    level1, level2, level3
}
public class ChangeScene : MonoBehaviour
{
    public static ChangeScene sharedInstance;

    [SerializeField] Scenes tarjetScene;

    private void Awake()
    {
        sharedInstance = this;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (this.tarjetScene == Scenes.level1)
                SceneManager.LoadScene("Level1");
            else if (this.tarjetScene == Scenes.level2)
                SceneManager.LoadScene("Level2");
            else if (this.tarjetScene == Scenes.level3)
                SceneManager.LoadScene("Level3");
        }
    
    }
    
  
    

   
}