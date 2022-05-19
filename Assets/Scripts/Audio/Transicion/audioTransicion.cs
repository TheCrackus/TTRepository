using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioTransicion : sistemaAudio
{

    [Header("Audio de la transicion")]
    [SerializeField] private AudioSource audioTransiciona;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioTransiciona;

    public void reproduceAudioTransicion() 
    {
        reproduceAudio(audioTransiciona, velocidadAudioTransiciona);
    }

}
