using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Barrier
{
    bridge, flyPlatform, wallDoor, example
}

public class SceneBarrierController : MonoBehaviour
{
    public static SceneBarrierController sharedInstance;

    [SerializeField] Barrier obj;
    [SerializeField] bool reverseActivation;
    [SerializeField] bool sameScene = false;

    private bool statusKeyObject;
    private int numBarrier;
    private bool isInitiallyActive;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        this.numBarrier = GetTypeObjectActivation(); 
        if (this.numBarrier != -1 && this.sameScene == false) //si es una barrera valida ejecuta el codigo de dentro.
        {
            bool statusKeyObject = DataStorage.sharedInstance.GetKeyObjects(this.numBarrier); //Consulta el estado del keyObject asociado a la barrera
            if (statusKeyObject)
            {
                if (this.reverseActivation)
                    gameObject.SetActive(false);
                else
                    gameObject.SetActive(true);
            }
            else
            {
                if (this.reverseActivation)
                    gameObject.SetActive(true);
                else
                    gameObject.SetActive(false);
            }
        }
    }

    public void SetBarrier()
    {
        if (this.numBarrier != -1 && this.sameScene == true)
        {
            bool statusKeyObject = DataStorage.sharedInstance.GetKeyObjects(this.numBarrier); //Consulta el estado del keyObject asociado a la barrera
            if (statusKeyObject)
            {
                if (this.reverseActivation)
                    gameObject.SetActive(false);
                else
                    gameObject.SetActive(true);
            }
            else
            {
                if (this.reverseActivation)
                    gameObject.SetActive(true);
                else
                    gameObject.SetActive(false);
            }
        }
    }


    //determina que barrera es el objeto.
    int GetTypeObjectActivation()
    {
        if (this.obj == Barrier.bridge)
            return this.numBarrier = 1;
        else if (this.obj == Barrier.flyPlatform)
            return this.numBarrier = 2;
        else if (this.obj == Barrier.wallDoor)
            return this.numBarrier = 3;
        else if (this.obj == Barrier.example)
            return this.numBarrier = 4;
        return this.numBarrier = -1;
    }
}