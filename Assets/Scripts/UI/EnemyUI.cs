using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] Transform enemyTarget;
    
    [SerializeField] Slider sliderEnemyLife;
    private Canvas canvasEnemyUI;
    private Transform enemyTransform;

    [SerializeField] int enemyMaxLife = 100;

    private Image enemyLifeFillImage;

    private Image enemyLifebackgroundImage;

    [SerializeField] Vector2 offsetLife;

    private Camera mainCamera;




    private void Awake()
    {
        this.canvasEnemyUI = GetComponentInChildren<Canvas>();
        this.enemyTransform = GetComponent<Transform>();
        this.mainCamera = Camera.main;

        this.enemyLifeFillImage = this.sliderEnemyLife.fillRect.GetComponent<Image>();
        
    }


    // Start is called before the first frame update
    void Start()
    {
        if (this.sliderEnemyLife == null)
            Debug.LogWarning("Nulo");
        if (this.mainCamera == null)
            Debug.LogWarning("MainCamera no asignada.");
        else
            this.canvasEnemyUI.worldCamera = this.mainCamera;

        FixLocalScale();

        sliderEnemyLife.maxValue = CollisionHandler.sharedInstance.GetEnemyMaxLife();
        sliderEnemyLife.value = CollisionHandler.sharedInstance.GetEnemyLife();

        sliderEnemyLife.transform.position = new Vector2(this.enemyTarget.position.x + this.offsetLife.x, this.enemyTarget.position.y + this.offsetLife.y);

        enemyLifebackgroundImage = GetComponentsInChildren<Image>(true).FirstOrDefault();

        ShowEnemyLifeBar();
    }

    // Update is called once per frame
    void Update()
    {
        sliderEnemyLife.value = CollisionHandler.sharedInstance.GetEnemyLife();
        sliderEnemyLife.transform.position = new Vector2(this.enemyTarget.position.x + this.offsetLife.x, this.enemyTarget.position.y + this.offsetLife.y);
        
        ShowEnemyLifeBar();
        FixLocalScale();
    }

    private void FixLocalScale()
    {
        if (this.enemyTransform.localScale.x == 1)
            this.canvasEnemyUI.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        else if (this.enemyTransform.localScale.x == -1)
            this.canvasEnemyUI.transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
    }

    public void ShowEnemyLifeBar()
    {
        //si la vida del enemy esta llena no muestra la barra de vida
        if (this.sliderEnemyLife.value >= this.enemyMaxLife) 
        { 
            this.enemyLifeFillImage.enabled = false;
            enemyLifebackgroundImage.enabled = false;
        }
        else 
        { 
            this.enemyLifeFillImage.enabled = true;
            enemyLifebackgroundImage.enabled = true;
        }
    }
}
