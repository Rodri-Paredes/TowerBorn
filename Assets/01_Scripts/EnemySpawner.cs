using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs; // Prefabs de enemigos
    [SerializeField] private Text lifeText; // Referencia al texto de la UI que muestra las vidas.
    [SerializeField] private GameObject gameOverPanel; // Panel de Game Over.
    [SerializeField] private Text waveText; // Texto de la oleada.

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8; // Número base de enemigos por ola.
    [SerializeField] private float enemiesPerSecond = 0.5f; // Tasa de aparición de enemigos.
    [SerializeField] private float timeBetweenWaves = 5f; // Tiempo entre oleadas.
    [SerializeField] private float difficultyScalingFactor = 0.75f; // Factor de escalado de dificultad.
    [SerializeField] private int playerLives = 10; // Vidas iniciales del jugador.

    private int currentWave = 1; // Oleada actual
    private float timeSinceLastSpawn; // Tiempo transcurrido desde el último spawn.
    private int enemiesAlive; // Enemigos vivos en la escena.
    private int enemiesLeftToSpawn; // Enemigos restantes por spawnear.
    private bool isSpawning = false; // ¿Está actualmente spawneando?

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent(); // Evento de destrucción de enemigo.

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void Start()
    {
        UpdateLifeUI(); // Inicializa las vidas en la UI.
        gameOverPanel.SetActive(false); // Asegúrate de que el panel de Game Over esté oculto.
        StartCoroutine(StartWave());
        UpdateWaveUI();
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
            SpawnEnemy();
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
        UpdateWaveUI();
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void SpawnEnemy()
    {
        // Asegúrate de que hay al menos 3 elementos en el arreglo
        if (enemyPrefabs.Length < 3)
        {
            Debug.LogError("El arreglo enemyPrefabs debe tener al menos 3 elementos.");
            return;
        }

        // Selecciona aleatoriamente un prefab entre los índices 0 y 2
        int randomIndex = Random.Range(0, 3);
        GameObject prefabToSpawn = enemyPrefabs[randomIndex];

        // Instancia el enemigo
        GameObject spawnedEnemy = Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);

        // Asigna la lógica del EndPoint al enemigo
        spawnedEnemy.GetComponent<EnemyMovement>().OnReachEndPoint = ReduceLife;
    }

    public void ReduceLife()
    {
        playerLives--;
        UpdateLifeUI(); // Actualiza la vida en la UI.

        if (playerLives <= 0)
        {
            GameOver();
        }
    }

    private void UpdateLifeUI()
    {
        lifeText.text = playerLives.ToString(); // Actualiza el texto de la vida en la UI.
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true); // Muestra el panel de Game Over.
        Time.timeScale = 0f; // Detiene el juego.
    }

    private void UpdateWaveUI()
    {
        waveText.text = $"Wave: {currentWave}"; // Actualiza el texto de la oleada en la UI.
    }
}
