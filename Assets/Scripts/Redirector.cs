using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirector : MonoBehaviour

    
{
    // Either drag in via Inspector
    [SerializeField] private GameObject _scriptOnOtherObject;

    // or get at runtime if you are always sure about the hierachy
    private void Awake()
    {
        _scriptOnOtherObject = transform.parent.GetComponent<GameObject>();
    }

    // and now call this from the AnimationEvent
    public void DoIt()
    {
        
    }

}
