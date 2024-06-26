using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public static ScoreController sharedInstance;

    //public string textValue;
    [SerializeField] TextMeshProUGUI textLife;
    [SerializeField] TextMeshProUGUI textScene;
    [SerializeField] TextMeshProUGUI textTime;
    [SerializeField] TextMeshProUGUI textPlayerLevel;
    [SerializeField] TextMeshProUGUI textPlayerTotalExp;
    [SerializeField] TextMeshProUGUI textNextLevel;
    //public Text textRemainingExp;

    [SerializeField] TextMeshProUGUI textRemainingExp;

    //string timeS = "tiempo ";
    //float time=0;

    private void Awake()
    {
        sharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        textLife.text = "Vida " + PlayerController.sharedInstance.GetLife();
        textScene.text = this.UpdateNameScene();
        //time -= Time.deltaTime;
        //textTime.text = time + timeS.ToString("f0");

        if (LevelSystem.sharedInstance != null)
        {
            textPlayerLevel.text = "Lvl " + LevelSystem.sharedInstance.GetPlayerLevel();
            textPlayerTotalExp.text = "Exp " + LevelSystem.sharedInstance.GetPlayerTotalExp();
            textNextLevel.text = "Next " + LevelSystem.sharedInstance.GetNextLevel();
            //textRemainingExp.text = "ReExp " + LevelSystem.sharedInstance.GetPlayerRemainingExp();

            textRemainingExp.text = $"ReExp {LevelSystem.sharedInstance.GetPlayerRemainingExp()} ";

        }
        else
        {
            Debug.LogError("LevelSystem.sharedInstance is null");
        }

    }

    string UpdateNameScene()
    {
        string nameScene = "";
        switch (ChangeScene.sharedInstance.GetCurrentScene())
        {
            case "Level1":
                nameScene = "Nivel 1";
                return nameScene;
            case "Level2":
                nameScene = "Nivel 2";
                return nameScene;
            case "Level3":
                nameScene = "Nivel 3";
                return nameScene;
            case "Level4":
                nameScene = "Nivel 4";
                return nameScene;
            case "FinalLevel":
                nameScene = "Nivel Final";
                return nameScene;
        }
        return "null";
    }
}
