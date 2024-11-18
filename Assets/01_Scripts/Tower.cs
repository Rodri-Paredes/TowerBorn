using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 2.0f;
    private float nextFireTime = 0.0f;

    // Distancia hacia la derecha desde la posici�n de la torre
    public float offsetDistance = 1.0f; // Ajusta este valor seg�n sea necesario

    void Start()
    {
        // Puedes iniciar cualquier configuraci�n aqu� si es necesario
    }

    void Update()
    {
        // Verificar si es el momento de disparar
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    // M�todo para lanzar el disparo
    void Fire()
    {
        if (projectilePrefab != null)
        {
            // Calcular la posici�n hacia la derecha de la torre
            Vector3 offsetPosition = transform.position + transform.right * offsetDistance;
            Instantiate(projectilePrefab, offsetPosition, transform.rotation);
        }
    }
}
