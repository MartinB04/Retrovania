using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{

    public static DataStorage sharedInstance;


    bool enableMainMenu = true;
    float playerPointsLife = 100;
    

    private void Awake()
    {
        if(sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        sharedInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }




    public void SavePlayerPointsLife(float life)
    {
        this.playerPointsLife = life;
    }

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
