using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChecker : MonoBehaviour
{
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask alternativeTargetMask;
    [SerializeField] Vector2 direction;
    [SerializeField] float distance;


    public bool isTouching;

    // Update is called once per frame
    void Update()
    {
        isTouching = (Physics2D.Raycast(this.transform.position, direction, distance, targetMask) || Physics2D.Raycast(this.transform.position, direction, distance, alternativeTargetMask));

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (isTouching)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, direction * distance);
    }

#endif
}
