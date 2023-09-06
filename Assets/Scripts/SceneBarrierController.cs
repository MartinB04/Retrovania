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
        
        if (this.numBarrier != -1)
        {
            bool statusKeyObject = DataStorage.sharedInstance.GetKeyObjects(this.numBarrier);

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