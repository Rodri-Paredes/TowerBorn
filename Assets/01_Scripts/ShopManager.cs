using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject towerPrefab;      // Prefab de la torre
    public GameObject shopPanel;        // Panel de la tienda
    private GameObject currentButton;   // Referencia al botón desde donde se abre la tienda

    // Esta función se llama al abrir la tienda desde un botón específico
    public void OpenShop(GameObject button)
    {
        // Guardar el botón actual para usar su posición al crear la torre
        currentButton = button;

        // Mostrar el panel de la tienda
        shopPanel.SetActive(true);
    }

    // Esta función se llama al hacer clic en el botón de la torre dentro del panel
    public void CreateTower()
    {
        if (currentButton != null)
        {
            // Obtener la posición del botón
            Vector3 buttonPosition = currentButton.transform.position;

            // Instanciar la torre en la posición del botón
            Instantiate(towerPrefab, buttonPosition, Quaternion.identity);

            // Destruir el botón de abrir tienda
            Destroy(currentButton);

            // Ocultar el panel de la tienda
            shopPanel.SetActive(false);
        }
    }
}
