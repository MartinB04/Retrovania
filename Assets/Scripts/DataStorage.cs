using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{

    public static DataStorage sharedInstance;


    bool enableMainMenu = true;
    float playerPointsLife = 100;

    bool relic1 = true;
    bool relic2 = true;
    bool relic3 = true;
    

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

    public void SetRelics(int relic, bool status)
    {
        switch (relic)
        {
            case 1:
                this.relic1 = status;
                break;
            case 2:
                this.relic2 = status;
                break;
            case 3:
                this.relic3 = status;
                break;
        }
    }

    public bool GetRelics(int relic, bool status)
    {
        switch (relic)
        {
            case 1:
                return this.relic1;
                
            case 2:
                return this.relic2;
          
            case 3:
                return this.relic3;
              
        }
        return false;
    }
}
