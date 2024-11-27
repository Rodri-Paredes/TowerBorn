using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // M�todo para reiniciar la escena actual desde cero
    public void ReloadScene()
    {
        // Reinicia la escena actual desde el estado inicial
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // M�todo para volver a la primera escena del �ndice 0
    public void GotoIndex0()
    {
        SceneManager.LoadScene(1); // Carga la escena con �ndice 0
    }
}
