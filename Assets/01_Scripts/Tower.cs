using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float attackRange = 10f; // Radio de ataque
    [SerializeField] private float fireRate = 1f; // Tasa de disparo (proyectiles por segundo)
    [SerializeField] private GameObject projectilePrefab; // Prefab del proyectil
    [SerializeField] private float projectileSpeed = 15f; // Velocidad del proyectil

    private float fireCountdown = 0f; // Temporizador para controlar el disparo
    private Transform target; // Referencia al enemigo objetivo

    void Update()
    {
        // Busca al enemigo más cercano dentro del rango
        FindClosestEnemy();

        if (target != null)
        {
            // Si el enemigo está dentro del rango, dispara
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate; // Reinicia el temporizador basado en la tasa de disparo
            }
        }
        else
        {
            // Si no hay objetivo, no disparamos
            fireCountdown = 0f;
        }

        fireCountdown -= Time.deltaTime; // Reduce el temporizador cada frame
    }

    private void FindClosestEnemy()
    {
        // Obtener todos los enemigos en el rango de la torre
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= attackRange)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        // Si encontramos un enemigo dentro del rango, asignamos el objetivo
        if (closestEnemy != null)
        {
            target = closestEnemy.transform;
        }
        else
        {
            // Si no hay enemigos dentro del rango, dejamos de disparar
            target = null;
        }
    }

    private void Shoot()
    {
        if (projectilePrefab == null || target == null) return;

        // Instancia el proyectil en la posición de la torre
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Obtiene el script del proyectil y le asigna el enemigo objetivo
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.target = target; // Asigna el enemigo como objetivo del proyectil
            projectileScript.speed = projectileSpeed; // Asigna la velocidad del proyectil
            projectileScript.attackRange = attackRange; // Asigna el rango de la torre al proyectil
            projectileScript.onDestroy += DestroyProjectile; // Suscribimos al evento de destrucción del proyectil
        }
    }

    private void DestroyProjectile(GameObject projectile)
    {
        // Destruye el proyectil cuando se alcanza el rango máximo o se sale del alcance
        Destroy(projectile);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el rango de ataque en la escena para visualizarlo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
