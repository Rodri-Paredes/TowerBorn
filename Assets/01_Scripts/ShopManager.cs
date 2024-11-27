using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject towerPrefab;      // Prefab de la torre
    public GameObject shopPanel;        // Panel de la tienda
    private GameObject currentButton;   // Referencia al bot�n desde donde se abre la tienda

    // Esta funci�n se llama al abrir la tienda desde un bot�n espec�fico
    public void OpenShop(GameObject button)
    {
        // Guardar el bot�n actual para usar su posici�n al crear la torre
        currentButton = button;

        // Mostrar el panel de la tienda
        shopPanel.SetActive(true);
    }

    // Esta funci�n se llama al hacer clic en el bot�n de la torre dentro del panel
    public void CreateTower()
    {
        if (currentButton != null)
        {
            // Obtener la posici�n del bot�n
            Vector3 buttonPosition = currentButton.transform.position;

            // Instanciar la torre en la posici�n del bot�n
            Instantiate(towerPrefab, buttonPosition, Quaternion.identity);

            // Destruir el bot�n de abrir tienda
            Destroy(currentButton);

            // Ocultar el panel de la tienda
            shopPanel.SetActive(false);
        }
    }
}
