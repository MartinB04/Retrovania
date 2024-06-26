using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] int initialDamage;
    [SerializeField] int currentDamage = 10;

    public static LevelSystem sharedInstance;

    private int playerLevel = 1;
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
        this.playerTotalExp = DataStorage.sharedInstance.GetPlayerTotalExp();
        this.playerLevel = DataStorage.sharedInstance.GetPlayerLevel();
        this.playerRemainingExp = DataStorage.sharedInstance.GetPlayerRemainingExp();
        this.nextLevel = DataStorage.sharedInstance.GetNextLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayerTotalExp(int exp)
    {
        this.playerTotalExp += exp;
    }

    public int GetPlayerTotalExp()
    {
        return this.playerTotalExp;
    }

    public void SetPlayerLevel()
    {
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
        //this.nextLevel =  (level * 10) + (level - 1) * 10;
        this.nextLevel += 10;
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

    public void SetCurrentPlayerDamage(int newDamage){

    }

    public int GetCurrentPlayerDamage(){
        return this.currentDamage;
    }
}
