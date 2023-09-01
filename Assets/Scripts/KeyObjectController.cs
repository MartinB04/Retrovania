using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyObject { 
    relic1, relic2, relic3, finalRelic
}


public class KeyObjectController : MonoBehaviour
{
    [SerializeField] KeyObject typeKeyObjetct;

    private SpriteRenderer spr;
    private bool statusKeyObject;
    private int keyObject;

    private void Awake()
    {
        this.spr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        this.keyObject = GetTypeKeyObject();
        if (this.keyObject != -1)
        {
            this.statusKeyObject = DataStorage.sharedInstance.GetKeyObjects(this.keyObject);
            if(this.statusKeyObject)
                this.spr.color = Color.white;
            else
                this.spr.color = Color.red;
        }
    }

    int GetTypeKeyObject()
    {
        if (this.typeKeyObjetct == KeyObject.relic1)
            return this.keyObject = 1;
        else if (this.typeKeyObjetct == KeyObject.relic2)
            return this.keyObject = 2;
        else if (this.typeKeyObjetct == KeyObject.relic3)
            return this.keyObject = 3;
        else if (this.typeKeyObjetct == KeyObject.finalRelic)
            return this.keyObject = 0;
        return this.keyObject = -1;
    }

    void SetStatusKeyObject(bool status)
    {
        this.statusKeyObject = status;
    }

    bool GetStatusKeyObject()
    {
        return this.statusKeyObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (this.statusKeyObject)
            {
                SetStatusKeyObject(!DataStorage.sharedInstance.GetKeyObjects(this.keyObject));   
                this.spr.color = Color.red;
                DataStorage.sharedInstance.SetKeyObjects(this.keyObject, GetStatusKeyObject());  
                if (this.typeKeyObjetct == KeyObject.finalRelic)
                    GameManager.sharedInstance.Win();
            }
            else
            {
                this.spr.color = Color.white;
                SetStatusKeyObject(!DataStorage.sharedInstance.GetKeyObjects(this.keyObject));
                DataStorage.sharedInstance.SetKeyObjects(this.keyObject, GetStatusKeyObject());
            }
        }
    }
}
