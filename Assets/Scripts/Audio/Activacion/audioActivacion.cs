using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioActivacion : SistemaAudio
{

    [Header("Audio al pulsar interruptor")]
    [SerializeField] private AudioSource audioActiva;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioActiva;

    public void reproduceAudioActivacion() 
    {
        reproducirAudio(audioActiva, velocidadAudioActiva);
    }

}
