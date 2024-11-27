using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f; // Daño que hace el proyectil
    public float speed = 15f;  // Velocidad del proyectil
    public Transform target;   // El enemigo al que persigue el proyectil
    public float attackRange = 10f; // Radio de la torre desde donde se dispara el proyectil
    public float maxDistance = 100f; // Máxima distancia para la destrucción del proyectil

    public delegate void DestroyEvent(GameObject projectile); // Evento de destrucción
    public event DestroyEvent onDestroy; // Evento que dispara la destrucción

    private Vector3 towerPosition; // Posición de la torre desde donde se dispara el proyectil
    private float distanceTraveled = 0f; // Distancia recorrida por el proyectil

    void Start()
    {
        // Al iniciar, guardamos la posición de la torre (puedes asignarla desde el script de la torre)
        towerPosition = transform.position;
    }

    void Update()
    {
        if (target != null)
        {
            // Calcula la distancia actual al objetivo
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // Calculamos la dirección hacia el objetivo
            Vector3 direction = (target.position - transform.position).normalized;

            // Calculamos la rotación hacia el objetivo (quiere que el proyectil rote hacia el objetivo)
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

            // Mueve el proyectil hacia el enemigo
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Verifica si el proyectil ha llegado al enemigo
            if (transform.position == target.position)
            {
                // Aquí puedes agregar lógica para aplicar el daño al enemigo (si es necesario)
                onDestroy?.Invoke(gameObject);  // Llama al evento de destrucción
            }

            // Verifica si el proyectil ha salido del radio de la torre
            if (Vector3.Distance(towerPosition, transform.position) > attackRange || distanceTraveled > maxDistance)
            {
                onDestroy?.Invoke(gameObject);  // Llama al evento de destrucción
            }
        }
        else
        {
            onDestroy?.Invoke(gameObject);  // Llama al evento de destrucción si no tiene target
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Si el proyectil toca un enemigo, destrúyelo
        if (other.CompareTag("Enemy"))
        {
            // Aquí puedes agregar la lógica para hacer daño al enemigo
            onDestroy?.Invoke(gameObject);  // Llama al evento de destrucción
        }
    }
}
