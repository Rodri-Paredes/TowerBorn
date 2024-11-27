using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f; // Da�o que hace el proyectil
    public float speed = 15f;  // Velocidad del proyectil
    public Transform target;   // El enemigo al que persigue el proyectil
    public float attackRange = 10f; // Radio de la torre desde donde se dispara el proyectil
    public float maxDistance = 100f; // M�xima distancia para la destrucci�n del proyectil

    public delegate void DestroyEvent(GameObject projectile); // Evento de destrucci�n
    public event DestroyEvent onDestroy; // Evento que dispara la destrucci�n

    private Vector3 towerPosition; // Posici�n de la torre desde donde se dispara el proyectil
    private float distanceTraveled = 0f; // Distancia recorrida por el proyectil

    void Start()
    {
        // Al iniciar, guardamos la posici�n de la torre (puedes asignarla desde el script de la torre)
        towerPosition = transform.position;
    }

    void Update()
    {
        if (target != null)
        {
            // Calcula la distancia actual al objetivo
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // Calculamos la direcci�n hacia el objetivo
            Vector3 direction = (target.position - transform.position).normalized;

            // Calculamos la rotaci�n hacia el objetivo (quiere que el proyectil rote hacia el objetivo)
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

            // Mueve el proyectil hacia el enemigo
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Verifica si el proyectil ha llegado al enemigo
            if (transform.position == target.position)
            {
                // Aqu� puedes agregar l�gica para aplicar el da�o al enemigo (si es necesario)
                onDestroy?.Invoke(gameObject);  // Llama al evento de destrucci�n
            }

            // Verifica si el proyectil ha salido del radio de la torre
            if (Vector3.Distance(towerPosition, transform.position) > attackRange || distanceTraveled > maxDistance)
            {
                onDestroy?.Invoke(gameObject);  // Llama al evento de destrucci�n
            }
        }
        else
        {
            onDestroy?.Invoke(gameObject);  // Llama al evento de destrucci�n si no tiene target
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Si el proyectil toca un enemigo, destr�yelo
        if (other.CompareTag("Enemy"))
        {
            // Aqu� puedes agregar la l�gica para hacer da�o al enemigo
            onDestroy?.Invoke(gameObject);  // Llama al evento de destrucci�n
        }
    }
}
