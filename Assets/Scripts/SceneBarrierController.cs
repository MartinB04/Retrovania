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

    private void Awake()
    {
        
    }

    private void Start()
    {
        this.numBarrier = GetTypeObjectActivation();
        Debug.Log("numBarrier" + this.numBarrier);
        if (this.numBarrier != -1)
        {
            //isInitiallyActive = gameObject.activeSelf;
            //Debug.Log("isInitiallyActive " + isInitiallyActive);

            bool statusKeyObject = DataStorage.sharedInstance.GetKeyObjects(this.numBarrier);
            //Debug.Log("statusKeyObject " + statusKeyObject);

            // Si el estado del objeto clave es verdadero, activa la barrera si no estaba inicialmente activa.
            // Si el estado del objeto clave es falso, desactiva la barrera si no estaba inicialmente desactiva.
            /*if (statusKeyObject && !isInitiallyActive)
            {
                gameObject.SetActive(true);
            }
            else if (!statusKeyObject && isInitiallyActive)
            {
                gameObject.SetActive(false);
            }*/
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

/*if (this.numBarrier != -1)
        {
            isInitiallyActive = gameObject.activeSelf;
            Debug.Log("isInitiallyActive " + isInitiallyActive);

            bool statusKeyObject = DataStorage.sharedInstance.GetKeyObjects(this.numBarrier);
            Debug.Log("statusKeyObject " + statusKeyObject);
            if (statusKeyObject && !isInitiallyActive)
            {
                gameObject.SetActive(true);
                Debug.Log("Barrier activated: " + this.numBarrier);
            }
            else if (!statusKeyObject && isInitiallyActive)
            {
                gameObject.SetActive(false);
                Debug.Log("Barrier deactivated: " + this.numBarrier);
            }
            else if (!statusKeyObject && !isInitiallyActive)
            {
                gameObject.SetActive(true);
            }
            else if (!statusKeyObject && !isInitiallyActive)
            {
                gameObject.SetActive(false);
            }
              
        }*/