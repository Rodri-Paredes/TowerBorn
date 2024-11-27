using UnityEngine;
using UnityEngine.UI; // Para manipular texto en la interfaz

public class PrefabMenuController2D : MonoBehaviour
{
    public GameObject menuPanel; // Panel del menú (asociar en el inspector)
    public Vector2 offset = new Vector2(0, 1.5f); // Desplazamiento del menú respecto al prefab
    public AudioClip clickSound; // Opcional: sonido para el clic
    public Text coinText; // Referencia al texto de las monedas
    public GameObject tower; // Torre asociada para destruir al vender

    private int coins = 10; // Contador inicial de monedas
    private AudioSource audioSource;

    private void Start()
    {
        // Asegurarse de que el menú esté oculto al iniciar
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }

        // Configurar el audio
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && clickSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Asegurarse de que el texto de monedas esté inicializado
        UpdateCoinText();
    }

    // Método llamado al hacer clic en el prefab
    private void OnMouseDown()
    {
        if (menuPanel != null)
        {
            // Reproducir sonido opcional
            if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }

            // Activar el menú y posicionarlo cerca del prefab
            menuPanel.SetActive(true);
            menuPanel.transform.position = (Vector2)transform.position + offset;
        }
        else
        {
            Debug.LogWarning("menuPanel no está asignado en el inspector.");
        }
    }

    // Método para cerrar el menú
    public void CloseMenu()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }
    }

    // Métodos para los botones
    public void LeftOption()
    {
        Debug.Log("Opción izquierda seleccionada");
        CloseMenu(); // Cierra el menú después de seleccionar
    }

    public void RightOption()
    {
        Debug.Log("Opción derecha seleccionada");
        CloseMenu(); // Cierra el menú después de seleccionar
    }

    // Método para manejar la opción de vender
    public void SellOption()
    {
        Debug.Log("Opción de vender seleccionada");

        // Añadir monedas
        AddCoins(5);

        // Destruir la torre asociada
        if (tower != null)
        {
            Destroy(tower);
        }
        else
        {
            Debug.LogWarning("No hay torre asociada para destruir.");
        }

        CloseMenu(); // Cierra el menú después de vender
    }

    // Método para añadir monedas
    private void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinText(); // Actualizar el texto de monedas
    }

    // Método para actualizar el texto de monedas
    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = coins.ToString(); // Mostrar solo el número de monedas
        }
        else
        {
            Debug.LogWarning("coinText no está asignado en el inspector.");
        }
    }
}
