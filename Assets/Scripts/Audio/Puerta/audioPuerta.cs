using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPuerta : SistemaAudio
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
        reproducirAudio(audioAbrirPuerta, velocidadAudioAbrirPuerta);
    }

    public void reproduceAudioCerrarPuerta()
    {
        reproducirAudio(audioCerrarPuerta, velocidadAudioCerrarPuerta);
    }

    public void reproduceAudioRomperPuerta()
    {
        reproducirAudio(audioRomperPuerta, velocidadAudioRomperPuerta);
    }

}
