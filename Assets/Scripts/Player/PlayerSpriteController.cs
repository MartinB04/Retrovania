using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    // Referencia al script PlayerController del padre
    private PlayerController playerController;

    private void Start()
    {
        // Obtener la referencia al script PlayerController en el padre
        playerController = GetComponentInParent<PlayerController>();
    }

    // M�todo p�blico para habilitar o deshabilitar el collider
    public void EnableAttackCollider()
    {
        // Llamar al m�todo del script PlayerController en el padre
        playerController.AttackCollider(1);
        //Debug.Log("Enable");
    }
    public void DisableAttackCollider()
    {
        playerController.AttackCollider(0);
        //Debug.Log("Disable");
    }

    public void EnableAttackAnimation()
    {
        playerController.AttackAnimationStatus(true);
    }
    public void DisableAttackAnimationStatus()
    {
        playerController.AttackAnimationStatus(false);
    }

}