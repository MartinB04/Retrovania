using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Object
{
    bridge, flyPlatform, wallDoor
}

public class ObjectActivation : MonoBehaviour
{
    [SerializeField] Object obj;


    public void Activation(int relic, bool status)
    {
        if (relic == 1 && this.obj == Object.bridge)
            gameObject.SetActive(status);
        else if (relic == 2 && this.obj == Object.flyPlatform)
            gameObject.SetActive(!status);
        else if (relic == 3 && this.obj == Object.wallDoor)
            gameObject.SetActive(status);

    }
}
