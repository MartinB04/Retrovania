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

    bool enableMainMenu = true;
    //string valorPreferName = "valor";
    float playerPointsLife = 100;
    //string lifePreferName;

    [SerializeField] Scenes tarjetScene;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject); // Garantiza que solo haya una instancia de ChangeScene.
            return;
        }
        sharedInstance = this;
        DontDestroyOnLoad(gameObject); // Mantén este objeto en las escenas cargadas.

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
    public void SavePlayerPointsLife(float life)
    {
        this.playerPointsLife = life;
    }
    //carga los valores almacenados directo a sus objetos
    

    public void ResetData()
    {
        this.playerPointsLife = 100;

        SetEnableMainMenu(true);
        SavePlayerPointsLife(this.playerPointsLife);
    }

    public bool GetEnableMainMenu()
    {
        return this.enableMainMenu;
    }

    public void SetEnableMainMenu(bool mainMenu)
    {
        this.enableMainMenu = mainMenu;
    }


    public float LoadPlayerPointsLife()
    {
        return this.playerPointsLife;
    }
}