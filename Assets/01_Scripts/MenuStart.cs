using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuStart : MonoBehaviour
{
    public GameObject EffectSound; 

    public void StartGame()
    {
        if (EffectSound != null)
        {
            AudioSource audioSource = EffectSound.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
                StartCoroutine(WaitForSoundToFinish(audioSource));
            }
        }
    }

    private System.Collections.IEnumerator WaitForSoundToFinish(AudioSource audioSource)
    {

        yield return new WaitForSeconds(audioSource.clip.length);

        SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        if (EffectSound != null)
        {

            AudioSource audioSource = EffectSound.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                // Reproduce el sonido desde el AudioSource del GameObject
                audioSource.Play();
            }

        }
    }

    public void Exit()
    {
        Application.Quit();
        if (EffectSound != null)
        {

            AudioSource audioSource = EffectSound.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                // Reproduce el sonido desde el AudioSource del GameObject
                audioSource.Play();
            }

        }

    }

}
