using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] Transform enemyTarget;
    //[SerializeField] TextMeshProUGUI enemyLife;
    [SerializeField] Slider enemyLife;
    private Canvas canvasEnemyUI;
    private Transform enemyTransform;


    [SerializeField] Vector2 offsetLife;

    private Camera mainCamera;




    private void Awake()
    {
        this.canvasEnemyUI = GetComponentInChildren<Canvas>();
        this.enemyTransform = GetComponent<Transform>();
        this.mainCamera = Camera.main;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (enemyLife == null)
            Debug.LogWarning("Nulo");
        if (this.mainCamera == null)
            Debug.LogWarning("MainCamera no asignada.");
        else
            this.canvasEnemyUI.worldCamera = this.mainCamera;

        FixLocalScale();

        enemyLife.maxValue = CollisionHandler.sharedInstance.GetEnemyMaxLife();
        enemyLife.value = CollisionHandler.sharedInstance.GetEnemyLife();

        enemyLife.transform.position = new Vector2(this.enemyTarget.position.x + this.offsetLife.x, this.enemyTarget.position.y + this.offsetLife.y);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"{CollisionHandler.sharedInstance.GetEnemyLife()}");
        Debug.Log($"position fireskull {this.enemyTarget.position}");
       
        enemyLife.value = CollisionHandler.sharedInstance.GetEnemyLife();

        enemyLife.transform.position = new Vector2(this.enemyTarget.position.x + this.offsetLife.x, this.enemyTarget.position.y + this.offsetLife.y);
        //enemyLife.transform.position = this.enemyTarget.position;
        //enemyLife.transform.localScale = new Vector3(1, 1, 1);

       


        FixLocalScale();
        


        
    }

    private void FixLocalScale()
    {
        if (this.enemyTransform.localScale.x == 1)
            this.canvasEnemyUI.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        else if (this.enemyTransform.localScale.x == -1)
            this.canvasEnemyUI.transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
    }
}
