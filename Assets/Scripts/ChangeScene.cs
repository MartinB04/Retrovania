using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene sharedInstance;

    int valor = 0;
    string valorPreferName = "valor";
    float life;
    string lifePreferName;
    int relic1;
    string relic1PreferName;

    private void Awake()
    {
        sharedInstance = this;
        LoadData();
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
        Scene esena = SceneManager.GetActiveScene();
        if (col.gameObject.CompareTag("Player") && (esena.name == "Level1"))
        {
            valor += 1;
            Debug.Log("Valor = " + valor);
            SceneManager.LoadScene("Level2");
        }

        else if (col.gameObject.CompareTag("Player") && (esena.name == "Level2"))
        {
            valor += 1;
            Debug.Log("Valor = " + valor);
            SceneManager.LoadScene("Level3");
        }

    }
    public void SaveData()
    {
        //si es gameover va a regrear todos los valores almacenados a defecto
        if (GameManager.sharedInstance.currentGameState == GameState.gameOver ||
            GameManager.sharedInstance.currentGameState == GameState.win)
        {
            SetValue();
            PlayerPrefs.SetInt(valorPreferName, valor);
            //Debug.Log("Setea valor = " + GetValue());
        }
        //Sino, va a seguir almacenando datos
        else
        {
            PlayerPrefs.SetInt(valorPreferName, valor);
            PlayerPrefs.SetFloat(lifePreferName, PlayerController.sharedInstance.GetLife());
            Debug.Log("Get life " + PlayerController.sharedInstance.GetLife());
            //PlayerPrefs.SetInt(relic1PreferName, Relic1.sharedInstance.GetRelic());
            //Debug.Log("reliquia 1 = " + PlayerPrefs.GetInt(relic1PreferName));

        }

    }
    //carga los valores almacenados directo a sus objetos
    void LoadData()
    {
        valor = PlayerPrefs.GetInt(valorPreferName, 0);
        life = PlayerPrefs.GetFloat(lifePreferName, 100);
        Debug.Log("Life + cambio escena => " + life);
        //PlayerController.sharedInstance.SaveLife(life);
        //relic1 = PlayerPrefs.GetInt(relic1PreferName);
        //Debug.Log("Relic1 + cambio escena => " + relic1);
        //RelicController.sharedInstance.SetRelics(relic1);
        //Debug.Log("reliquia 1 local " + RelicController.sharedInstance.GetRelic1());

    }
    //guarda datos al destruirse por el cambio de escena
    private void OnDestroy()
    {
        SaveData();
    }
    //retorna el contador de cambio de escena
    public int GetValue()
    {
        return this.valor;
    }

    //setea los datos, es importante poner ponerlos por defecto, ya que los setea directamente a sus objetos
    //solo se ejecutara en estado gameover
    public void SetValue()
    {
        this.valor = 0;
        PlayerController.sharedInstance.SetLife(0);
        Debug.Log("Esta es la vida que tiene setValue" + PlayerController.sharedInstance.GetLife());
        //RelicController.sharedInstance.SetRelics(1);
    }

    public int GetRelic1()
    {
        return this.relic1;
    }

    public float GetLife()
    {
        Debug.Log("es la vida que devuelve el CS " + this.life);
        return this.life;
    }

}