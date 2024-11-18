using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Audio;
public class MenuOptions : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public void FullScreen(bool CompleteScene)
    {
        Screen.fullScreen = CompleteScene;
    }
    public void ChangeVolume(float audio)
    {
        audioMixer.SetFloat("Volumen", audio);
    }
}
