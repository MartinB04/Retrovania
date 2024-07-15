using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public static ScoreController sharedInstance;

    //public string textValue;
    [SerializeField] Slider sliderLife;
    [SerializeField] Slider sliderRemainingExp;
    [SerializeField] TextMeshProUGUI textScene;
    [SerializeField] TextMeshProUGUI textTime;
    [SerializeField] TextMeshProUGUI textPlayerLevel;
    [SerializeField] TextMeshProUGUI textPlayerTotalExp;
    [SerializeField] TextMeshProUGUI textNextLevel;
    //public Text textRemainingExp;

    [SerializeField] TextMeshProUGUI textMoney;

    //[SerializeField] TextMeshProUGUI textRemainingExp;

    private Image lifeFillImage;
    private Image remainingExpFillImage;

    //string timeS = "tiempo ";
    //float time=0;

    private void Awake()
    {
        sharedInstance = this;
        this.lifeFillImage = sliderLife.fillRect.GetComponent<Image>();
        this.remainingExpFillImage = sliderRemainingExp.fillRect.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        this.sliderLife.maxValue = LevelSystem.sharedInstance.GetMaxPlayerLife();
        this.sliderLife.value = LevelSystem.sharedInstance.GetLife();
        

        this.sliderRemainingExp.maxValue = LevelSystem.sharedInstance.GetNextLevel();
        this.sliderRemainingExp.value = LevelSystem.sharedInstance.GetPlayerRemainingExp();


        //se desactiva el relleno rojo de vida cuando el jugador muere
        //debido a que sino, queda un resto rojo en la barra al morir
        if (this.sliderLife.value <= 0)
            this.lifeFillImage.enabled = false;
        else
            this.lifeFillImage.enabled = true;

        if (this.sliderRemainingExp.value <= 0)
            this.remainingExpFillImage.enabled = false;
        else
            this.remainingExpFillImage.enabled = true;

        textScene.text = this.UpdateNameScene();
        //time -= Time.deltaTime;
        //textTime.text = time + timeS.ToString("f0");

        if (LevelSystem.sharedInstance != null)
        {
            textPlayerLevel.text = "Lvl " + LevelSystem.sharedInstance.GetPlayerLevel();
            textPlayerTotalExp.text = "Exp " + LevelSystem.sharedInstance.GetPlayerTotalExp();
            textNextLevel.text = "Next " + LevelSystem.sharedInstance.GetNextLevel();
            textMoney.text = "Money " + LevelSystem.sharedInstance.GetPlayerMoney();


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
