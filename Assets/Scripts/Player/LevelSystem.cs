using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] int initialDamage;
    [SerializeField] int currentPlayerDamage = 10;

    public static LevelSystem sharedInstance;

    private int playerLevel = 1;
    private int playerRemainingExp = 0;
    private int playerTotalExp = 0;
    private int nextLevel;

    private int playerMoney;


    [SerializeField] float lifePoints = 100;

    private int maxPlayerLife;

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


        this.maxPlayerLife = DataStorage.sharedInstance.GetMaxPlayerLife();
        this.LoadPlayerLife(DataStorage.sharedInstance.LoadPlayerPointsLife());
        this.currentPlayerDamage = DataStorage.sharedInstance.GetPlayerDamage();

        this.playerMoney = DataStorage.sharedInstance.GetPlayerMoney();
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
        this.maxPlayerLife += 10;
        //Debug.Log($"MaxLIfe {this.maxPlayerLife}");
        //Debug.Log($"LIfe {this.lifePoints}");
        SetCurrentPlayerDamage();
        SetPlayerLevel();
    }

    public void calculateExp(int pointsExp)
    {
        this.playerRemainingExp += pointsExp;

        while (this.playerRemainingExp > 0)
        {
            if (this.playerRemainingExp >= this.nextLevel)
            {
                this.playerRemainingExp = this.playerRemainingExp - this.nextLevel;
                IncreaseLevel();
            }
            else
                break; // Sal del bucle si playerRemainingExp es menor que nextLevel

        }
        SetPlayerTotalExp(pointsExp);
    }

    public void SetCurrentPlayerDamage(){
        this.currentPlayerDamage += 10;
    }

    public int GetCurrentPlayerDamage(){
        return this.currentPlayerDamage;
    }


    public float IncreaseLife(float points)
    {
        float remainingPoints = points;

        // Solo añadir vida si el jugador no está al máximo de vida
        if (GetLife() < this.maxPlayerLife)
        {
            this.lifePoints += points;
            if (this.lifePoints > this.maxPlayerLife)
            {
                remainingPoints = this.lifePoints - this.maxPlayerLife;
                this.lifePoints = this.maxPlayerLife;
            }
            else
            {
                remainingPoints = 0;  // Todo el "points" se ha utilizado
            }
        }

        return remainingPoints;
    }

    public void DecreaseLife(float points)
    {
        this.lifePoints -= points;
        if (this.lifePoints < 0)
            this.lifePoints = 0;

    }

    public float GetLife()
    {
        return this.lifePoints;
    }

    public void LoadPlayerLife(float life)
    {
        this.lifePoints = life;
    }

    public int GetMaxPlayerLife()
    {
        return this.maxPlayerLife;
    }

    public void SetPlayerMoney(int money)
    {
        Debug.Log($"SetPlayerMoney called with money = {money}");
        Debug.Log($"PlayerMoney before update = {this.playerMoney}");

        this.playerMoney += money;

        Debug.Log($"PlayerMoney after update = {this.playerMoney}");
    }

    public int GetPlayerMoney()
    {
        return this.playerMoney;
    }
}
