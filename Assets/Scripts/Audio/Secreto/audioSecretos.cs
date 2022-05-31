using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSecretos : SistemaAudio
{

    [Header("Audio al abrir el cofre")]
    [SerializeField] private AudioSource audioAbrirCofre;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioAbrirCofre;

    public void reproducirAudioSecreto() 
    {
        reproducirAudio(audioAbrirCofre, velocidadAudioAbrirCofre);
    }

}
