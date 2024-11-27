using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Este método se llamará cuando el enemigo colisione con el proyectil
    private void OnCollisionEnter(Collision collision)
    {
        // Verificamos si la colisión fue con un proyectil
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Destruimos el enemigo al colisionar con el proyectil
            Destroy(gameObject);
        }
    }
}
