using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class manejadorMixer : MonoBehaviour
{

    [Header("Mixer a controlar")]
    [SerializeField] private AudioMixer masterMixer;

    [Header("Nombre del volumen a modificar")]
    [SerializeField] private string nombreMasterVolume;

    public void enviaVolumen(float volumen) 
    {
        masterMixer.SetFloat(nombreMasterVolume, Mathf.Log10(volumen) * 20);
    }

}
