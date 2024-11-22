using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public void PlaySound(GameObject effectSound)
    {
        if (effectSound != null)
        {
            AudioSource audioSource = effectSound.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
