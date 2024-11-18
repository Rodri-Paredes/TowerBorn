using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 2.0f;
    private float nextFireTime = 0.0f;

    // Distancia hacia la derecha desde la posición de la torre
    public float offsetDistance = 1.0f; // Ajusta este valor según sea necesario

    void Start()
    {
        // Puedes iniciar cualquier configuración aquí si es necesario
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

    // Método para lanzar el disparo
    void Fire()
    {
        if (projectilePrefab != null)
        {
            // Calcular la posición hacia la derecha de la torre
            Vector3 offsetPosition = transform.position + transform.right * offsetDistance;
            Instantiate(projectilePrefab, offsetPosition, transform.rotation);
        }
    }
}
