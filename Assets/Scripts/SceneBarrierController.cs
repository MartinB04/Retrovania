using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Barrier
{
    bridge, flyPlatform, wallDoor
}

public class SceneBarrierController : MonoBehaviour
{
    [SerializeField] Barrier obj;
    [SerializeField] bool reverseActivation;

    private bool statusKeyObject;
    private int numBarrier;
    private bool isInitiallyActive;

    private void Start()
    {
        this.numBarrier = GetTypeObjectActivation(); 
        if (this.numBarrier != -1) //si es una barrera valida ejecuta el codigo de dentro.
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
        return this.numBarrier = -1;
    }
}