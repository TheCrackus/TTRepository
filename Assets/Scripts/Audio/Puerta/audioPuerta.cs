using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPuerta : sistemaAudio
{
    [Header("Audio al abrir puerta")]
    [SerializeField] private AudioSource audioAbrirPuerta;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioAbrirPuerta;

    [Header("Audio al cerrar puerta")]
    [SerializeField] private AudioSource audioCerrarPuerta;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioCerrarPuerta;

    [Header("Audio al romper puerta")]
    [SerializeField] private AudioSource audioRomperPuerta;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioRomperPuerta;

    public void reproduceAudioAbrirPuerta() 
    {
        reproduceAudio(audioAbrirPuerta, velocidadAudioAbrirPuerta);
    }

    public void reproduceAudioCerrarPuerta()
    {
        reproduceAudio(audioCerrarPuerta, velocidadAudioCerrarPuerta);
    }

    public void reproduceAudioRomperPuerta()
    {
        reproduceAudio(audioRomperPuerta, velocidadAudioRomperPuerta);
    }

}
