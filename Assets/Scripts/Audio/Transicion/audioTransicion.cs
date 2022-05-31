using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTransicion : SistemaAudio
{

    [Header("Audio de la transicion")]
    [SerializeField] private AudioSource audioTransiciona;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioTransiciona;

    public void reproducirAudioTransicion() 
    {
        reproducirAudio(audioTransiciona, velocidadAudioTransiciona);
    }

}
