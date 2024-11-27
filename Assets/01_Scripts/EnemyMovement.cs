using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0;

    private Transform target;
    private int pathIndex = 0;

    public System.Action OnReachEndPoint; // Acción a ejecutar al llegar al EndPoint.

    private void Start()
    {
        target = LevelManager.main.path[0];
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            if (pathIndex == LevelManager.main.path.Length) // Al alcanzar el EndPoint
            {
                OnReachEndPoint?.Invoke(); // Reduce las vidas del jugador.
                EnemySpawner.onEnemyDestroy.Invoke(); // Notifica al spawner.
                Destroy(gameObject); // Destruye el enemigo.
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
        FlipSprite(direction);
    }

    private void FlipSprite(Vector2 direction)
    {
        if (direction.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
