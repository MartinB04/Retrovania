using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController sharedInstance;

    //public string textValue;
    public Text textLife;
    public Text textScene;
    public Text textTime;

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
