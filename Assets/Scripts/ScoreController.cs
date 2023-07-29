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
        textLife.text = "PV " + PlayerController.sharedInstance.GetLife();
        textScene.text = ChangeScene.sharedInstance.GetCurrentScene();
        //time -= Time.deltaTime;
        //textTime.text = time + timeS.ToString("f0");
    }
}
