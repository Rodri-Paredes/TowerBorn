using UnityEngine;
using UnityEngine.UI; // Para manipular texto en la interfaz

public class PrefabMenuController2D : MonoBehaviour
{
    public GameObject menuPanel; // Panel del men� (asociar en el inspector)
    public Vector2 offset = new Vector2(0, 1.5f); // Desplazamiento del men� respecto al prefab
    public AudioClip clickSound; // Opcional: sonido para el clic
    public Text coinText; // Referencia al texto de las monedas
    public GameObject tower; // Torre asociada para destruir al vender

    private int coins = 10; // Contador inicial de monedas
    private AudioSource audioSource;

    private void Start()
    {
        // Asegurarse de que el men� est� oculto al iniciar
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

        // Asegurarse de que el texto de monedas est� inicializado
        UpdateCoinText();
    }

    // M�todo llamado al hacer clic en el prefab
    private void OnMouseDown()
    {
        if (menuPanel != null)
        {
            // Reproducir sonido opcional
            if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }

            // Activar el men� y posicionarlo cerca del prefab
            menuPanel.SetActive(true);
            menuPanel.transform.position = (Vector2)transform.position + offset;
        }
        else
        {
            Debug.LogWarning("menuPanel no est� asignado en el inspector.");
        }
    }

    // M�todo para cerrar el men�
    public void CloseMenu()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }
    }

    // M�todos para los botones
    public void LeftOption()
    {
        Debug.Log("Opci�n izquierda seleccionada");
        CloseMenu(); // Cierra el men� despu�s de seleccionar
    }

    public void RightOption()
    {
        Debug.Log("Opci�n derecha seleccionada");
        CloseMenu(); // Cierra el men� despu�s de seleccionar
    }

    // M�todo para manejar la opci�n de vender
    public void SellOption()
    {
        Debug.Log("Opci�n de vender seleccionada");

        // A�adir monedas
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

        CloseMenu(); // Cierra el men� despu�s de vender
    }

    // M�todo para a�adir monedas
    private void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinText(); // Actualizar el texto de monedas
    }

    // M�todo para actualizar el texto de monedas
    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = coins.ToString(); // Mostrar solo el n�mero de monedas
        }
        else
        {
            Debug.LogWarning("coinText no est� asignado en el inspector.");
        }
    }
}
