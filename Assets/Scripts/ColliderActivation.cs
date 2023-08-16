using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActivation : MonoBehaviour
{
    // Referencia al Collider que deseas habilitar
    public Collider2D targetCollider;

    // Método para habilitar el collider
    public void EnableCollider()
    {
        targetCollider.enabled = true;
    }

    // Método para deshabilitar el collider
    public void DisableCollider()
    {
        targetCollider.enabled = false;
    }
}