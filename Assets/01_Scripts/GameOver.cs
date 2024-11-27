using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Método para reiniciar la escena actual desde cero
    public void ReloadScene()
    {
        // Reinicia la escena actual desde el estado inicial
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Método para volver a la primera escena del índice 0
    public void GotoIndex0()
    {
        SceneManager.LoadScene(1); // Carga la escena con índice 0
    }
}
