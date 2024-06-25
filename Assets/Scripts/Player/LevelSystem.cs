using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] int initialDamage;
    [SerializeField] int currentDamage;

    public static LevelSystem sharedInstance;
    //public static DataStorage sharedInstance;

    private int playerLevel = 1;

    //private int playerExp = 0;
    private int playerRemainingExp = 0;
    private int playerTotalExp = 0;

    private int nextLevel;

    private PlayerController playerController;

    private void Awake()
    {
        sharedInstance = this;

        this.playerController = GetComponent<PlayerController>();

    }

    void Start()
    {
        //this.playerExp = DataStorage.sharedInstance.GetPlayerExp();
        this.playerTotalExp = DataStorage.sharedInstance.GetPlayerTotalExp();
        this.playerLevel = DataStorage.sharedInstance.GetPlayerLevel();
        this.playerRemainingExp = DataStorage.sharedInstance.GetPlayerRemainingExp();

        //if (this.playerLevel == 1)
        //this.nextLevel = 10;
        //else
        this.nextLevel = DataStorage.sharedInstance.GetNextLevel();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayerTotalExp(int exp)
    {
        if (this.playerTotalExp == 0)
            this.playerTotalExp = exp;
        //this.playerRemainingExp = exp;

        else
            this.playerTotalExp += exp;

    }

    /* public int GetPlayerExp()
    {
        return this.playerExp;
    } */

    public int GetPlayerTotalExp()
    {
        return this.playerTotalExp;
    }

    public void SetPlayerLevel()
    {
        Debug.Log("Set Player Level ENTRO");
        this.playerLevel++;
    }

    public int GetPlayerLevel()
    {
        return this.playerLevel;
    }

    public int GetNextLevel()
    {
        return this.nextLevel;
    }

    public int GetPlayerRemainingExp()
    {
        return this.playerRemainingExp;
    }

    public void IncreaseLevel()
    {
        if (this.playerLevel > 1)
        {
            //this.nextLevel = (this.playerLevel * 10) + this.nextLevel;
            this.nextLevel += 10;
        }
        SetPlayerLevel();


    }

    public void calculateExp(int pointsExp)
    {
        this.playerRemainingExp += pointsExp;

        while (this.playerRemainingExp > 0)
        {
            if (this.playerRemainingExp >= this.nextLevel)
            {
                this.playerRemainingExp -= this.nextLevel;
                IncreaseLevel();
            }
            else
                break; // Sal del bucle si playerRemainingExp es menor que nextLevel

        }

        SetPlayerTotalExp(pointsExp);
    }

}
