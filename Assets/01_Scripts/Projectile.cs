using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad del proyectil

    void Update()
    {
        // Mover el proyectil hacia la derecha
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
